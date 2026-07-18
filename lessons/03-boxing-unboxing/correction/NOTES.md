# Correction Notes — Lesson 03 — Boxing and Unboxing

## Answer

**Common mistakes to watch for:**
- Using `ArrayList`/`Hashtable` in new code — there's essentially no reason to in modern C#.
- Casting with `(int)item` instead of the safe `item is int n` pattern when the collection's contents aren't guaranteed.
- Not realizing that even something as innocent as `Console.WriteLine(myStruct)` can box the struct if it goes through an `object`-typed overload.
