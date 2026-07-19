# Lesson 12 — Choosing the Right Collection

## Why this matters
Using `List<T>` for everything "works" until it doesn't — an O(n) `.Contains()` scan inside a loop can silently turn a fast program into a slow one as data grows.

## The concept

| Need | Use |
|---|---|
| Ordered, duplicates allowed, indexable | `List<T>` |
| Fast key lookup | `Dictionary<TKey,TValue>` |
| No duplicates | `HashSet<T>` |
| FIFO processing | `Queue<T>` |
| LIFO processing | `Stack<T>` |
| Thread-safe producer/consumer | `System.Collections.Concurrent.*` |

```csharp
// Bad — O(n) scan on every check, inside a loop = O(n^2) overall
List<int> seenIds = new();
foreach (var id in incomingIds)
    if (!seenIds.Contains(id)) seenIds.Add(id);

// Good — Add() on a HashSet is O(1) and naturally dedupes
HashSet<int> seenIds2 = new();
foreach (var id in incomingIds)
    seenIds2.Add(id);
```
