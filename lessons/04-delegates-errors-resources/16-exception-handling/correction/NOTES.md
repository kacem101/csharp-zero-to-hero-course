# Correction Notes — Lesson 16 — Exception Handling Deep Dive

## Answer

**Common mistakes to watch for:**
- Catching `Exception` broadly "to be safe" — this hides real bugs instead of surfacing them during development.
- `throw ex;` instead of `throw;` when rethrowing — destroys the original stack trace.
- Using try/catch for expected, everyday cases (bad user input, a missing dictionary key you expect sometimes) instead of `TryParse`/`TryGetValue`-style APIs — exceptions are for *exceptional*, not routine, situations.
