// TODO 1: Write a class ApiClient with a single static readonly
// HttpClient (Timeout = 10s) and a method
// `async Task<string?> GetTextOrNullAsync(string url)` that returns null
// on 404, throws on other non-success codes, otherwise returns the body
// text.

// TODO 2: Write `async Task<T?> GetJsonAsync<T>(string url)` (generic!)
// using ReadFromJsonAsync<T>, handling 404 as null the same way.

// TODO 3: Add cancellation support: overload GetTextOrNullAsync to accept
// a CancellationToken and pass it through to GetAsync.

// TODO 4 (bug hunt): what's wrong, and what real-world failure mode does
// it cause under load?
public async Task<string> Fetch(string url)
{
    var client = new HttpClient();
    return await client.GetStringAsync(url);
}
