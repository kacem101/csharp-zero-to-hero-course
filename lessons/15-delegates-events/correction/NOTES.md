# Correction Notes — Lesson 15 — Delegates, Func/Action, and Events

## Answer

**Common mistakes to watch for:**
- Declaring a custom `delegate` type when `Func<>`/`Action<>`/`Predicate<>` already covers the exact signature you need.
- Using a public delegate field instead of `event` — allows accidental (or malicious) overwriting of all subscribers.
- Forgetting the null-conditional `?.Invoke(...)` — invoking an event with zero subscribers throws `NullReferenceException` without it.
