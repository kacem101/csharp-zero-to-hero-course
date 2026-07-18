# Lesson 20 — HttpClient Done Right

## Why this matters
`HttpClient` is one of the most-misused types in .NET — creating a new one per request looks harmless and is a classic cause of production outages under load (socket exhaustion).

## The concept
`HttpClient` manages an underlying connection pool and is designed to be **reused** for the lifetime of the app (or via `IHttpClientFactory` in ASP.NET), not created per-call.

```csharp
// Bad — new connection pool every call
public async Task<string> GetDataAsync(string url)
{
    using var client = new HttpClient(); // BAD
    return await client.GetStringAsync(url);
}
// under load, connections pile up in TIME_WAIT and new requests start failing —
// this is "socket exhaustion", a well-known .NET production incident cause
```

```csharp
// Good — one shared, long-lived instance
public class ApiClient
{
    private static readonly HttpClient _client = new();
    public async Task<string> GetDataAsync(string url) => await _client.GetStringAsync(url);
}
```
(In ASP.NET Core, prefer `IHttpClientFactory` via DI over even a static field — it manages connection lifetime correctly and avoids DNS-change issues a purely static client can have. For a console app, a static shared instance is fine.)

### Handle non-success responses explicitly
```csharp
public async Task<Product?> GetProductAsync(int id, CancellationToken ct = default)
{
    var response = await _client.GetAsync($"https://api.example.com/products/{id}", ct);
    if (response.StatusCode == HttpStatusCode.NotFound) return null; // expected outcome, not an error
    response.EnsureSuccessStatusCode(); // throws for any other non-2xx
    return await response.Content.ReadFromJsonAsync<Product>(cancellationToken: ct);
}
```
If you skip the status check and just call `ReadFromJsonAsync` on a 404/500 HTML error page, you get a confusing JSON-deserialization exception instead of a clear signal about what actually failed.

### Always set a timeout
```csharp
var client = new HttpClient { Timeout = TimeSpan.FromSeconds(10) }; // default is 100s — often too long
```
