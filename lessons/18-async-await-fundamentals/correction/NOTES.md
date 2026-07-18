# Correction Notes — Lesson 18 — async/await Fundamentals

## Answer

**Common mistakes to watch for:**
- Calling `.Result` or `.Wait()` on a `Task` instead of `await`ing it — this blocks a thread and can deadlock (Lesson 19).
- Marking a method `async` but never actually `await`-ing anything inside it (the compiler will warn — such a method runs synchronously despite the `async` keyword).
- Assuming `await` means "runs in parallel" — it means "don't block while waiting"; true concurrency across multiple operations needs `Task.WhenAll` (Lesson 19).
