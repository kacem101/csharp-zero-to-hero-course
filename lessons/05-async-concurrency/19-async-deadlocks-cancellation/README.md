# Lesson 19 — Deadlocks, Task Types, Task.WhenAll, Cancellation, and async void

## Why this matters
This lesson covers the five async pitfalls that account for almost every real-world async bug report: the classic deadlock, wasted sequential awaits, unkillable long-running operations, and unhandled crashes from `async void`.

## The concept

### The deadlock trap
```csharp
public void ButtonClick() // synchronous caller
{
    var result = DownloadAsync(url).Result; // DEADLOCK in a UI app or classic (non-Core) ASP.NET!
}
public async Task<string> DownloadAsync(string url)
{
    using var client = new HttpClient();
    return await client.GetStringAsync(url);
    // by default, `await` tries to resume on the original context (e.g.
    // the UI thread). But that thread is blocked on .Result above — it
    // can never become free to run the continuation. Classic deadlock.
}
```
Fix: **async all the way up** — `await` instead of `.Result` at every level. If you truly must call async code from a sync entry point in library code, use `ConfigureAwait(false)` so the continuation doesn't require the original context.

### Task vs Task<T> vs ValueTask<T>
- `Task` — async operation, no return value (awaitable "void").
- `Task<T>` — async operation producing a `T`.
- `ValueTask<T>` — struct-based, avoids a heap allocation when the result is already available synchronously; a micro-optimization for hot paths, not a default choice.

### Running independent operations concurrently
```csharp
// Bad — three sequential round trips
var user = await GetUserAsync(id);
var orders = await GetOrdersAsync(id);
var reviews = await GetReviewsAsync(id);

// Good — start together, await together
Task<User> userTask = GetUserAsync(id);
Task<List<Order>> ordersTask = GetOrdersAsync(id);
Task<List<Review>> reviewsTask = GetReviewsAsync(id);
await Task.WhenAll(userTask, ordersTask, reviewsTask);
User user = userTask.Result;      // safe — already completed
```
This turns three sequential round-trips into roughly the duration of the *slowest single one*.

### Cancellation
```csharp
public async Task<string> DownloadAsync(string url, CancellationToken ct)
{
    using var client = new HttpClient();
    return await client.GetStringAsync(url, ct);
}
using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
try { var result = await DownloadAsync(url, cts.Token); }
catch (OperationCanceledException) { Console.WriteLine("Timed out or cancelled."); }
```

### async void — avoid it outside event handlers
```csharp
public async void ProcessOrder(Order order) // BAD outside event handlers
{
    await SaveAsync(order); // if this throws, the caller has NO way to catch it —
                              // it crashes the process directly
}
```
Return `Task` instead, so callers can `try { await ProcessOrderAsync(order); } catch { }`. The only legitimate `async void` is a UI event handler, whose signature is dictated by the framework.
