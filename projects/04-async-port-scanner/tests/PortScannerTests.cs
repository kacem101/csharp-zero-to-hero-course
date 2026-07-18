using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

// These tests run entirely against localhost — no external network
// access, and nothing here scans anything you don't own (your own test
// process). Safe to run anywhere.
public class PortScannerTests
{
    [Fact]
    public async Task ScanAsync_KnownOpenLocalPort_IsReportedOpen()
    {
        var listener = new TcpListener(IPAddress.Loopback, 0); // port 0 = OS assigns a free port
        listener.Start();
        int openPort = ((IPEndPoint)listener.LocalEndpoint).Port;

        // Accept (and immediately drop) any incoming connection so the scan's connect succeeds.
        var acceptTask = Task.Run(async () =>
        {
            using var client = await listener.AcceptTcpClientAsync();
        });

        var scanner = new PortScanner();
        var results = await scanner.ScanAsync("127.0.0.1", openPort, openPort, maxConcurrency: 1, CancellationToken.None);

        listener.Stop();

        Assert.Single(results);
        Assert.True(results[0].IsOpen);
    }

    [Fact]
    public async Task ScanAsync_PortWithNoListener_IsReportedClosed()
    {
        // Grab a free port, then immediately release it by stopping the
        // listener before scanning — near-certain to be closed for the
        // scan itself.
        var listener = new TcpListener(IPAddress.Loopback, 0);
        listener.Start();
        int freePort = ((IPEndPoint)listener.LocalEndpoint).Port;
        listener.Stop();

        var scanner = new PortScanner();
        var results = await scanner.ScanAsync("127.0.0.1", freePort, freePort, maxConcurrency: 1, CancellationToken.None);

        Assert.Single(results);
        Assert.False(results[0].IsOpen);
    }

    [Fact]
    public async Task ScanAsync_PortRange_ReturnsOneResultPerPort()
    {
        var scanner = new PortScanner();
        var results = await scanner.ScanAsync("127.0.0.1", 50000, 50004, maxConcurrency: 5, CancellationToken.None);

        Assert.Equal(5, results.Count);
    }
}
