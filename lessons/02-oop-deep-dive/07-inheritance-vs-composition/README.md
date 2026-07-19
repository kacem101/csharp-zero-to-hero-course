# Lesson 07 — Inheritance vs Composition

## Why this matters
Inheritance is for modeling a real **"is-a"** relationship. Reaching for it just to reuse code — when there's no real is-a relationship — creates classes that leak unwanted behavior and break in surprising ways.

## The concept

```csharp
public abstract class Shape
{
    public abstract double Area();
    public void PrintArea() => Console.WriteLine($"Area: {Area():F2}");
}
public class Circle : Shape { public double Radius; public override double Area() => Math.PI * Radius * Radius; }
public class Rectangle : Shape { public double Width, Height; public override double Area() => Width * Height; }
```

A `Circle` really *is* a `Shape` — this is legitimate inheritance.

The classic misuse: inheriting from a collection just to add two methods.

```csharp
public class Stack : List<int>  // BAD
{
    public void Push(int x) => Add(x);
    public int Pop() { var last = this[^1]; RemoveAt(Count - 1); return last; }
}
// Now Insert(0, x), Sort(), and every other List<int> method is ALSO
// public on Stack — a caller can do stack.Insert(0, 5) and silently
// corrupt the stack's LIFO guarantee.
```

Fix with composition: `Stack` *has-a* `List<int>` privately, and only exposes the methods that make sense.

```csharp
public class Stack
{
    private readonly List<int> _items = new();
    public void Push(int x) => _items.Add(x);
    public int Pop() { var last = _items[^1]; _items.RemoveAt(_items.Count - 1); return last; }
}
```

**Rule of thumb:** ask "can every instance of the subtype be used anywhere the base type is expected, and does it genuinely behave like one?" If not, use composition.
