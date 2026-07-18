# Correction Notes — Lesson 01 — Value Types vs Reference Types

## Answer

**Common mistakes to watch for:**
- Expecting a struct method to mutate the caller's variable without `ref`.
- Putting a `List<T>` or other reference-type field inside a struct and assuming copies are independent.
- Making an "entity" (something with identity that changes over time) a struct just because it's small right now.
