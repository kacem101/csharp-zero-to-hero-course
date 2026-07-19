using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

static class ApiClient
{
    private static readonly HttpClient _client = new() { Timeout = TimeSpan.FromSeconds(10) };

    public static async Task<string?> GetTextOrNullAsync(string url, CancellationToken ct = default)
    {
        var response = await _client.GetAsync(url, ct);
        if (response.StatusCode == HttpStatusCode.NotFound) return null;
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync(ct);
    }

    public static async Task<T?> GetJsonAsync<T>(string url, CancellationToken ct = default)
    {
        var response = await _client.GetAsync(url, ct);
        if (response.StatusCode == HttpStatusCode.NotFound) return default;
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<T>(cancellationToken: ct);
    }
}

class Program
{
    // TODO 4 answer: a new HttpClient (and its own connection pool) is
    // created on EVERY call to Fetch(). Under concurrent load, this means
    // sockets are opened and torn down constantly instead of reused,
    // leaving many connections stuck in the OS's TIME_WAIT state. Once
    // enough accumulate, the OS runs out of available ports/sockets and
    // NEW connections start failing outright — this is "socket
    // exhaustion," and it's the single most common HttpClient production
    // bug in .NET. Fix: use the shared static `_client` above instead of
    // `new HttpClient()` per call.

    static async Task Main()
    {
        try
        {
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
            var body = await ApiClient.GetTextOrNullAsync("https://example.com", cts.Token);
            Console.WriteLine(body is null ? "Not found" : $"Got {body.Length} chars");
        }
        catch (TaskCanceledException)
        {
            Console.WriteLine("Request timed out or was cancelled.");
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Request failed: {ex.Message}");
        }
    }
}
