# Correction Notes — Lesson 13 — LINQ & Deferred Execution

## Answer

**Common mistakes to watch for:**
- Enumerating the same un-materialized query multiple times, assuming it's "the same data" each time when the underlying source can change.
- Calling `.ToList()` too early inside a chain, forcing full materialization before filters that could have run lazily (relevant for LINQ-to-Objects performance, and *critical* for `IQueryable` — see Lesson 14).
- Forgetting a captured variable in a lambda is evaluated when the lambda *runs*, not when it's written — see the `threshold` example above.
