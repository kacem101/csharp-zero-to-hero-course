# Lesson 01 — Value Types vs Reference Types

## Why this matters
This is the single most important mental model in C#. Get it wrong and you'll hit "impossible" bugs — a method that "doesn't update" an object, or two variables that mysteriously change together. Everything else (structs vs classes, `ref`, boxing, mutability bugs) is a consequence of this one distinction.

## The concept
C# splits every type into two families:

- **Value types** (`int`, `double`, `bool`, `struct`, `enum`) — the variable directly *contains* the data. Stack-allocated in most cases. Assignment **copies** the whole value.
- **Reference types** (`class`, `string`, arrays, `List<T>`, delegates) — the variable holds a *pointer* to an object on the heap. Assignment copies the pointer, not the object — so two variables can end up pointing at the same thing.

```csharp
struct Point { public int X, Y; }
Point a = new Point { X = 1, Y = 1 };
Point b = a;        // full copy
b.X = 99;
Console.WriteLine(a.X); // 1 — untouched

class PointClass { public int X, Y; }
PointClass c = new PointClass { X = 1, Y = 1 };
PointClass d = c;   // same object, two names
d.X = 99;
Console.WriteLine(c.X); // 99 — changed
```

**Rule of thumb:** use `struct` only for small, immutable, short-lived data (a color, a 2D point). The moment a type is large, mutable, or represents an "entity" (an `Employee`, an `Order`), it should be a `class`. A mutable struct is a bug generator — methods that look like they mutate the object are silently mutating a copy.
