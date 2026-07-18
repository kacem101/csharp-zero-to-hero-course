# Correction Notes — Lesson 00.2 — Variables, Types & Operators: the C# vs C Cheat Sheet

## Answer

**Common mistakes to watch for:**
- Calling a string method like `.ToUpper()`/`.Trim()` and forgetting to capture the return value — since strings are immutable, the original variable never changes on its own.
- Mixing a `string` and a numeric type with `+` expecting numeric addition — C# silently does string concatenation instead (a real, common source of bugs).
- Assuming `var` means "any type can go here later" — it's resolved to one concrete type at compile time, permanently.
