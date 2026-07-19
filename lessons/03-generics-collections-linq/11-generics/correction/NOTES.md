# Correction Notes — Lesson 11 — Generics

## Answer

**Common mistakes to watch for:**
- Comparing a generic `T` to `null` directly without a constraint or `EqualityComparer<T>`.
- Forgetting a constraint and then being surprised you can't call `.CompareTo()`, `.ToString()`-specific overloads, or use `new T()` (needs `where T : new()`).
- Making everything generic "just in case" — if a type will only ever be used with one concrete type, a generic adds complexity for no benefit.
