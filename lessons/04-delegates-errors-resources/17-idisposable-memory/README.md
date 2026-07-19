# Lesson 17 — IDisposable & Memory Management

## Why this matters
The garbage collector reclaims **memory** — it has no idea your object is holding an open file handle, network socket, or database connection. Those "unmanaged" resources need deterministic, immediate cleanup, which is exactly what `IDisposable` and `using` provide.

## The concept

```csharp
public class FileLogger : IDisposable
{
    private readonly StreamWriter _writer;
    public FileLogger(string path) => _writer = new StreamWriter(path);
    public void Log(string message) => _writer.WriteLine(message);
    public void Dispose() => _writer.Dispose(); // releases the OS file handle
}
```

```csharp
using (var logger = new FileLogger("log.txt"))
{
    logger.Log("Starting up");
} // Dispose() called automatically here, even if an exception is thrown inside

// or, the newer "using declaration" syntax:
using var logger2 = new FileLogger("log.txt");
logger2.Log("Also fine");
// Dispose() called at the end of the enclosing scope
```

The bug this prevents:

```csharp
var logger = new FileLogger("log.txt");
logger.Log("Starting up");
DoSomethingThatMightThrow();
logger.Dispose(); // never reached if the line above throws — the handle leaks
```

**Why the GC alone isn't enough:** the GC decides *when* to run based on memory pressure, not on how many OS handles you've opened. Meanwhile you can exhaust the OS's limit on open file handles or sockets long before a GC cycle happens to clean them up.
