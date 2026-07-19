# Correction Notes — Lesson 14 — IQueryable vs IEnumerable

## Answer

**Common mistakes to watch for:**
- Calling `.ToList()`, `.ToArray()`, or looping with `foreach` before you've applied all the filters you need — anything after that point runs in memory, not in the database.
- Using a C#-only method (a custom method, a complex lambda EF can't translate) inside a `.Where()` on an `IQueryable` — this can force EF Core to silently fall back to client-side evaluation, which is another way to accidentally pull too much data.
- Assuming `IQueryable` and `IEnumerable` behave identically just because `IQueryable<T>` extends `IEnumerable<T>` — the *execution location* (database vs memory) is the whole point of the distinction.
