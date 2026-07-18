# Correction Notes — Lesson 00.7 — Intro to Collections: List<T> as a Safer, Growable Array

## Answer

**Common mistakes to watch for:**
- Removing from the front of a `List<T>` in a loop (e.g. treating it like a queue with `RemoveAt(0)`) — this is O(n) per removal, O(n²) overall; use `Queue<T>` (Lesson 12) for FIFO needs instead.
- Forgetting `List<T>.Contains()` already exists and hand-rolling it in production code — the manual version in this lesson is for understanding, not something you'd normally rewrite yourself.
- Indexing past `list.Count - 1` — just like a C# array, `List<T>` throws `ArgumentOutOfRangeException` rather than silently reading garbage memory.
