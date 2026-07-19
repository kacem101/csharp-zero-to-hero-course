# Correction Notes — Lesson 24 — Testing with xUnit

## Answer

**Common mistakes to watch for:**
- Testing private implementation details (via reflection or internal access) instead of public, observable behavior — this makes tests brittle and actively discourages safe refactoring.
- Writing a test that depends on a real database, real network call, or real file system when a fake/interface-based substitute (Lesson 10) would do — slow, flaky tests get skipped or ignored over time.
- Vague test names like `Test1` — a failing test's name should tell you what broke without opening the test body.
