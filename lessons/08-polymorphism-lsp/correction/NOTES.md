# Correction Notes — Lesson 08 — Polymorphism & the Liskov Substitution Principle

## Answer

**Common mistakes to watch for:**
- Overriding a method to throw `NotSupportedException` — that's almost always a sign the base abstraction is wrong for that subtype.
- Writing `is X` / `is Y` chains against types you fully control instead of using virtual dispatch.
- Treating LSP as "does it compile" instead of "does it behave the way callers of the base type are entitled to assume."
