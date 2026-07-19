# Lesson 03 — Boxing and Unboxing

## Why this matters
Boxing is invisible in the syntax — there's no keyword that says "heap allocation happening here." That's exactly why it quietly wrecks performance in hot loops written by people who don't know it's happening.

## The concept
**Boxing**: wrapping a value type in an `object` on the heap. **Unboxing**: extracting it back out.

```csharp
int number = 42;
object boxed = number;     // boxing: heap allocation
int unboxed = (int)boxed;  // unboxing: cast back
```

The classic trap is old, pre-generic APIs (`ArrayList`, `Hashtable`) that store `object`:

```csharp
ArrayList list = new ArrayList();
for (int i = 0; i < 1_000_000; i++)
    list.Add(i);  // boxes every single int — 1,000,000 heap allocations
```

Generics fix this entirely, because `List<int>` stores actual `int`s, not boxed `object`s:

```csharp
List<int> list = new List<int>();
for (int i = 0; i < 1_000_000; i++)
    list.Add(i);  // no boxing
```

**Rule of thumb:** if you see `object`, `ArrayList`, or a non-generic collection holding value types, boxing is happening. In modern C#, you should almost never need `ArrayList` at all.
