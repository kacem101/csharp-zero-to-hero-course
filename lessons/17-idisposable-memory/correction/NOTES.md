# Correction Notes — Lesson 17 — IDisposable & Memory Management

## Answer

**Common mistakes to watch for:**
- Calling `.Dispose()` manually on a line that can be skipped by an early return or exception — always prefer `using`.
- Writing a class that holds `IDisposable` fields but doesn't itself implement `IDisposable` to clean them up.
- In a composite `Dispose()`, letting one resource's disposal exception prevent another resource's disposal — wrap in `try/finally` as shown.
