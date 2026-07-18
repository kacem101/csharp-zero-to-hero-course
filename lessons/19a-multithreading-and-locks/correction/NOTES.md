# Correction Notes — Lesson 19a — Multithreading & Locks

## Answer

**Common mistakes to watch for:**
- Wrapping I/O-bound work (a network call, a file read) in `Task.Run` just to "make it async" — it doesn't help; `await` the I/O call directly instead (Lesson 18).
- Any shared mutable state accessed from multiple threads without synchronization — even something as innocent-looking as `counter++` isn't atomic.
- Inconsistent lock acquisition order across different methods — the single most common cause of real deadlocks in multithreaded code.
- Reaching for `lock` when `Interlocked` would do — for simple counters, `Interlocked` is faster and just as correct.
