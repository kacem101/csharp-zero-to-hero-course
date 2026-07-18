using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public record ScanResult(int Port, bool IsOpen, string? Banner);

public class PortScanner
{
    private static readonly TimeSpan ConnectTimeout = TimeSpan.FromMilliseconds(500);
    private static readonly TimeSpan BannerTimeout = TimeSpan.FromMilliseconds(300);

    public async Task<List<ScanResult>> ScanAsync(string host, int startPort, int endPort, int maxConcurrency, CancellationToken ct)
    {
        using var semaphore = new SemaphoreSlim(maxConcurrency);
        var results = new List<ScanResult>();
        var resultsLock = new object();

        var tasks = Enumerable.Range(startPort, endPort - startPort + 1).Select(async port =>
        {
            await semaphore.WaitAsync(ct);
            try
            {
                var result = await ScanPortAsync(host, port, ct);
                lock (resultsLock) { results.Add(result); }
            }
            finally
            {
                semaphore.Release();
            }
        });

        try
        {
            await Task.WhenAll(tasks);
        }
        catch (OperationCanceledException)
        {
            // Expected on Ctrl+C — fall through and return whatever we gathered.
        }

        return results.OrderBy(r => r.Port).ToList();
    }

    private async Task<ScanResult> ScanPortAsync(string host, int port, CancellationToken ct)
    {
        using var client = new TcpClient();
        using var connectCts = CancellationTokenSource.CreateLinkedTokenSource(ct);
        connectCts.CancelAfter(ConnectTimeout);

        try
        {
            await client.ConnectAsync(host, port, connectCts.Token);
        }
        catch (SocketException)
        {
            return new ScanResult(port, false, null); // connection refused / unreachable — an ordinary, expected outcome
        }
        catch (OperationCanceledException) when (!ct.IsCancellationRequested)
        {
            return new ScanResult(port, false, null); // per-port connect TIMEOUT, not a full-scan cancellation
        }

        string? banner = await TryReadBannerAsync(client, ct);
        return new ScanResult(port, true, banner);
    }

    private async Task<string?> TryReadBannerAsync(TcpClient client, CancellationToken ct)
    {
        try
        {
            using var bannerCts = CancellationTokenSource.CreateLinkedTokenSource(ct);
            bannerCts.CancelAfter(BannerTimeout);

            var stream = client.GetStream();
            byte[] buffer = new byte[256];
            int bytesRead = await stream.ReadAsync(buffer, bannerCts.Token);
            if (bytesRead == 0) return null;

            return Encoding.ASCII.GetString(buffer, 0, bytesRead).TrimEnd('\r', '\n');
        }
        catch (OperationCanceledException) when (!ct.IsCancellationRequested)
        {
            return null; // many services don't send anything unprompted — that's fine, not an error
        }
        catch (IOException)
        {
            return null;
        }
    }
}
