using System;
using System.Collections.Generic;
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
        // TODO: read non-empty lines from `path` asynchronously
        throw new NotImplementedException();
    }

    static async Task<HealthCheckResult> CheckUrlAsync(string url, CancellationToken ct)
    {
        // TODO: time the request with Stopwatch, GET the url, catch
        // HttpRequestException and TaskCanceledException specifically,
        // return a HealthCheckResult either way (IsUp=false on failure)
        throw new NotImplementedException();
    }

    static async Task<List<HealthCheckResult>> CheckAllAsync(List<string> urls, int maxConcurrency, CancellationToken ct)
    {
        // TODO: use SemaphoreSlim(maxConcurrency) + Task.WhenAll to bound concurrency
        throw new NotImplementedException();
    }

    static void PrintSummary(List<HealthCheckResult> results)
    {
        // TODO: use LINQ GroupBy(r => r.IsUp) + Average(r => r.Duration.TotalMilliseconds)
    }

    static async Task Main(string[] args)
    {
        // TODO: wire Console.CancelKeyPress to a CancellationTokenSource,
        // load urls, call CheckAllAsync, print results + summary
    }
}
