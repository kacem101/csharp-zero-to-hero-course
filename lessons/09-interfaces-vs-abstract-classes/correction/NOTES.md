# Correction Notes — Lesson 09 — Interfaces vs Abstract Classes

## Answer

**Common mistakes to watch for:**
- Reaching for an abstract class when types don't actually share meaningful implementation — that forces an artificial single-inheritance hierarchy.
- Reaching for an interface when several implementers would end up copy-pasting the same method bodies — that's a signal for a shared abstract base instead.
- Forgetting a class can implement many interfaces but extend only one class — plan your one inheritance slot carefully.
