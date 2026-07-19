# Lesson 08 — Polymorphism & the Liskov Substitution Principle

## Why this matters
Polymorphism is what lets you write code once against a base type and have it correctly handle every subtype, forever, without editing that code again. Violating substitutability quietly destroys that guarantee.

## The concept

```csharp
List<Shape> shapes = new() { new Circle(3), new Rectangle(4, 5) };
foreach (var shape in shapes)
    shape.PrintArea(); // correct Area() runs for each concrete type
```

The anti-pattern is faking polymorphism with type checks:

```csharp
foreach (var shape in shapes)
{
    if (shape is Circle c) Console.WriteLine(Math.PI * c.Radius * c.Radius);
    else if (shape is Rectangle r) Console.WriteLine(r.Width * r.Height);
    // every new shape type means editing THIS method too
}
```

### Liskov Substitution Principle
A subclass must be usable anywhere its base class is expected, without breaking correctness. The textbook violation: Square inheriting from Rectangle.

```csharp
class Rectangle { public virtual int Width { get; set; } public virtual int Height { get; set; } public int Area() => Width * Height; }
class Square : Rectangle
{
    public override int Width { set { base.Width = base.Height = value; } }
    public override int Height { set { base.Width = base.Height = value; } }
}
void Resize(Rectangle r) { r.Width = 5; r.Height = 10; Console.WriteLine(r.Area()); }
Resize(new Rectangle()); // 50, as expected
Resize(new Square());    // 100 — SURPRISE, breaks caller's assumption
```

A `Square` is mathematically a rectangle, but modeling it this way breaks the caller's expectation that setting Width and Height independently works. Fix: don't model it with mutable-property inheritance — use separate types, or an immutable `IShape` interface.
