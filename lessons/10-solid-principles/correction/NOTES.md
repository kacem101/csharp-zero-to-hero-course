# Correction Notes — Lesson 10 — SOLID Principles

## Answer

**Common mistakes to watch for:**
- Confusing "one class, one method" (too extreme) with Single Responsibility ("one *reason to change*" — a class can have several methods that all serve the same responsibility).
- Injecting concrete classes into constructors instead of interfaces, which silently reintroduces Dependency Inversion violations.
- Building one giant interface "to be safe" instead of composing several small ones.
