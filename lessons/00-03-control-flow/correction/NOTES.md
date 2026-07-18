# Correction Notes — Lesson 00.3 — Control Flow: if/else, Loops, switch

## Answer

**Common mistakes to watch for:**
- Trying to use `break;` to fall through a switch case like old C habits — C# switch statements don't fall through by default, so an empty case body without `break`/`return`/`goto case` is actually a compile error, not a silent bug.
- Trying to modify a collection's elements through the loop variable in `foreach` — the loop variable is read-only-ish for this purpose; use a regular `for` loop with indexing if you need to mutate elements in place.
- Off-by-one with range syntax — remember the end index in `a..b` is EXCLUSIVE, same convention as slicing in many other languages.
