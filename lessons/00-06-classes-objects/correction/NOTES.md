# Correction Notes — Lesson 00.6 — Classes & Objects: From C structs to C# Objects

## Answer

**Common mistakes to watch for:**
- Forgetting `new` when creating an object — unlike C where a `struct Node n;` on the stack is valid, in C# a `class` variable that's never assigned with `new` is `null`/uninitialized and using it throws `NullReferenceException`.
- Confusing a `class`'s reference semantics with a `struct`'s value semantics (Lesson 01 goes deep on this) — assigning one class variable to another does NOT create an independent copy.
- Trying to bring C habits like manual null-terminated sentinels or manual `free()`-style cleanup into C# — the object model and garbage collector make that unnecessary and, if attempted, usually just dead code.
