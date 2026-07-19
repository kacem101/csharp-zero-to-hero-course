# Correction Notes — Lesson 22 — File I/O

## Answer

**Common mistakes to watch for:**
- Calling `File.ReadAllText`/`File.ReadAllLines` (sync) inside an `async` method — always use the `Async` suffix variants.
- Loading an entire large file into memory (`ReadAllLines`, `ReadAllText`) when a streaming `StreamReader.ReadLineAsync()` loop would do the job with a fraction of the memory.
- Forgetting `using` on `StreamReader`/`StreamWriter` — these hold OS file handles and need deterministic disposal (Lesson 17).
