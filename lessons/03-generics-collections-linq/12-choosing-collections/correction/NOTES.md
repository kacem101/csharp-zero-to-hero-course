# Correction Notes — Lesson 12 — Choosing the Right Collection

## Answer

**Common mistakes to watch for:**
- Using `List<T>.Contains()` for membership checks in a loop — reach for `HashSet<T>` instead.
- Using `List<T>` where insertion order at the *front* matters frequently — `List.RemoveAt(0)`/`Insert(0, x)` are O(n); a `Queue<T>` or `LinkedList<T>` fits better.
- Choosing `Dictionary<TKey,TValue>` but forgetting keys must be unique — a second `Add` with a duplicate key throws.
