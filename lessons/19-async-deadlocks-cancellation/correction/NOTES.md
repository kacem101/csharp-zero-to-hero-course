# Correction Notes — Lesson 19 — Deadlocks, Task Types, Task.WhenAll, Cancellation, and async void

## Answer

**Common mistakes to watch for:**
- Bridging sync-to-async with `.Result`/`.Wait()` anywhere in a call chain that has a synchronization context (classic ASP.NET, WPF, WinForms) — the classic deadlock.
- Awaiting independent operations sequentially when `Task.WhenAll` would run them concurrently for free.
- Writing long-running async loops with no `CancellationToken` at all — callers have no way to give up.
- Any `async void` outside a genuine UI event handler — exceptions from it can crash the whole process instead of being catchable.
