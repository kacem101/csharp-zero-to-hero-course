# Lesson 18 — async/await Fundamentals

## Why this matters
This is the most misunderstood area of C#. Get the mental model right here and everything downstream (deadlocks, cancellation, networking) makes sense. Get it wrong and you'll write code that *compiles* but silently blocks threads or crashes servers under load.

## The concept
`async`/`await` is **not** primarily about parallelism or extra threads. It's about **not blocking a thread while waiting** for something (I/O, a timer, a network call). The thread is returned to the thread pool during the wait, and picked back up (possibly by a different thread) when the awaited operation completes.

```csharp
public async Task<string> DownloadAsync(string url)
{
    using var client = new HttpClient();
    string result = await client.GetStringAsync(url); // thread is FREED during the network wait
    return result;
}
```

Compare to the blocking equivalent:

```csharp
public string Download(string url)
{
    using var client = new HttpClient();
    return client.GetStringAsync(url).Result; // blocks this thread for the whole round-trip
}
```

In a server handling many requests, `.Result`/`.Wait()` ties up a thread pool thread for the entire I/O duration. Under load, this exhausts the thread pool and the server stops responding — even though the CPU is sitting completely idle waiting on the network.

**Key mental model:** `await` doesn't mean "wait synchronously." It means "pause this method here, free up the thread, and resume this method (maybe on a different thread) once the awaited task finishes."
