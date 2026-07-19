using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

record HealthCheckResult(string Url, bool IsUp, int? StatusCode, TimeSpan Duration);

class Program
{
    static readonly HttpClient Client = new() { Timeout = TimeSpan.FromSeconds(5) };

    static async Task<List<string>> LoadUrlsAsync(string path)
    {
        var lines = await File.ReadAllLinesAsync(path);
        return lines.Where(l => !string.IsNullOrWhiteSpace(l)).ToList();
    }

    static async Task<HealthCheckResult> CheckUrlAsync(string url, CancellationToken ct)
    {
        var sw = Stopwatch.StartNew();
        try
        {
            var response = await Client.GetAsync(url, ct);
            sw.Stop();
            return new HealthCheckResult(url, response.IsSuccessStatusCode, (int)response.StatusCode, sw.Elapsed);
        }
        catch (HttpRequestException)
        {
            sw.Stop();
            return new HealthCheckResult(url, false, null, sw.Elapsed); // DNS failure, connection refused, etc.
        }
        catch (TaskCanceledException)
        {
            sw.Stop();
            return new HealthCheckResult(url, false, null, sw.Elapsed); // timeout or user cancellation
        }
    }

    static async Task<List<HealthCheckResult>> CheckAllAsync(List<string> urls, int maxConcurrency, CancellationToken ct)
    {
        using var semaphore = new SemaphoreSlim(maxConcurrency);
        var tasks = urls.Select(async url =>
        {
            await semaphore.WaitAsync(ct);
            try { return await CheckUrlAsync(url, ct); }
            finally { semaphore.Release(); }
        });
        var results = await Task.WhenAll(tasks);
        return results.ToList();
    }

    static void PrintSummary(List<HealthCheckResult> results)
    {
        Console.WriteLine("\n--- Summary ---");
        var groups = results.GroupBy(r => r.IsUp);
        foreach (var g in groups)
        {
            string label = g.Key ? "UP" : "DOWN";
            double avgMs = g.Average(r => r.Duration.TotalMilliseconds);
            Console.WriteLine($"{label}: {g.Count()} sites, avg {avgMs:F0}ms");
        }
    }

    static async Task Main(string[] args)
    {
        string urlsFile = args.Length > 0 ? args[0] : "urls.txt";
        if (!File.Exists(urlsFile))
            await File.WriteAllLinesAsync(urlsFile, new[] { "https://example.com", "https://this-domain-does-not-exist-xyz.com" });

        using var cts = new CancellationTokenSource();
        Console.CancelKeyPress += (_, e) =>
        {
            e.Cancel = true; // don't kill the process immediately — let cancellation propagate
            cts.Cancel();
            Console.WriteLine("Cancelling...");
        };

        var urls = await LoadUrlsAsync(urlsFile);
        Console.WriteLine($"Checking {urls.Count} URLs (Ctrl+C to cancel)...");

        List<HealthCheckResult> results;
        try
        {
            results = await CheckAllAsync(urls, maxConcurrency: 10, cts.Token);
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Cancelled by user.");
            return;
        }

        foreach (var r in results)
            Console.WriteLine($"{(r.IsUp ? "UP  " : "DOWN")} {r.Url} ({r.StatusCode?.ToString() ?? "n/a"}, {r.Duration.TotalMilliseconds:F0}ms)");

        PrintSummary(results);
    }
}
