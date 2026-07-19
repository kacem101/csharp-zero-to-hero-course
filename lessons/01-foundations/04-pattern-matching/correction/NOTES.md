# Correction Notes — Lesson 04 — Pattern Matching

## Answer

**Common mistakes to watch for:**
- Forgetting the discard arm `_` in a switch *expression* (a switch *statement* doesn't require it, but you should still handle the default case deliberately).
- Ordering patterns wrong — patterns are checked top to bottom, so a broad pattern before a specific one will shadow it and the compiler will flag unreachable code.
- Using `when` clauses that overlap in confusing ways — keep the conditions mutually exclusive where possible.
