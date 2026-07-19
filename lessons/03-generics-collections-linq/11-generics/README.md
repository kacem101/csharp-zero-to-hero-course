# Lesson 11 — Generics

## Why this matters
Generics let you write a type or method once, use it with any type, get full compile-time type safety, and — as you saw in Lesson 03 — avoid boxing entirely. They're the backbone of every modern collection in C#.

## The concept

```csharp
public class Box<T>
{
    private T _value;
    public Box(T value) => _value = value;
    public T Get() => _value;
}
var intBox = new Box<int>(42);   // no boxing, fully type-checked
var strBox = new Box<string>("hi");
```

Compare to the pre-generics style using `object`:
```csharp
public class Box
{
    private object _value;
    public Box(object value) => _value = value;
    public object Get() => _value;
}
var box = new Box(42);
int x = (int)box.Get();      // manual cast everywhere
string s = (string)box.Get(); // compiles even if box holds an int — crashes at runtime
```

### Generic constraints
Constraints let you use members of `T` that a bare unconstrained generic doesn't expose.

```csharp
public T Max<T>(T a, T b) where T : IComparable<T>
    => a.CompareTo(b) > 0 ? a : b; // CompareTo only available because of the constraint
```

Without the constraint, you can't call `CompareTo` at all — the compiler has no idea `T` supports it.
