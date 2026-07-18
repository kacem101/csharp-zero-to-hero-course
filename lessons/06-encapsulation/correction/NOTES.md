# Correction Notes — Lesson 06 — Encapsulation

## Answer

**Common mistakes to watch for:**
- Exposing a `List<T>` field or property directly — always expose `IReadOnlyList<T>`/`IReadOnlyCollection<T>` instead.
- Enforcing an invariant in only *some* of the entry points (e.g. checking on `AddSong` but leaving a public setter that bypasses it).
- Confusing encapsulation with "just make it private" — the real goal is making invalid states unrepresentable.
