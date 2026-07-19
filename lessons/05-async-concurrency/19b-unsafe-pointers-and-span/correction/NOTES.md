# Correction Notes — Lesson 19b — unsafe Pointers & Span<T>

## Answer

**Common mistakes to watch for:**
- Reaching for `unsafe` out of familiarity with C pointers when `Span<T>` would do the same job safely and just as fast.
- Forgetting `fixed` when taking a raw pointer to a managed array — without it, the GC is free to move the array mid-use.
- Trying to store a `Span<T>` somewhere it can outlive its source data (a field, a captured variable in an async method) — the compiler blocks this by design; use `Memory<T>` instead if you genuinely need to hold onto a buffer view across an `await` or store it on the heap.
