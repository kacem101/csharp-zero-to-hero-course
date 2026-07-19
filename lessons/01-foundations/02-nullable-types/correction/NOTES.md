# Correction Notes — Lesson 02 — Nullable Types

## Answer

**Common mistakes to watch for:**
- Reaching for `!` to silence a warning instead of actually handling the null case.
- Using `.Value` on an `int?` without checking `.HasValue` first (throws `InvalidOperationException` if null).
- Forgetting that `?.` short-circuits the *whole* chain: `a?.B.C` still evaluates `.C` unguarded if `B` could be null — you'd need `a?.B?.C`.
