# Lesson 01a — Records and Immutability

## Why this matters
Lesson 01 taught you that a `class` uses reference semantics — two variables pointing at the same object, and `==` compares references by default, not content. Records give you a third option: reference-type storage (still heap-allocated, still `new`) but **value-based equality and built-in immutability support**. You'll see `record` used constantly in modern C# for DTOs, API payloads, and anywhere "this is just data, and I want two instances with the same values to be considered equal."

## The concept

### The problem records solve
```csharp
class PointClass { public int X, Y; }
var a = new PointClass { X = 1, Y = 2 };
var b = new PointClass { X = 1, Y = 2 };
Console.WriteLine(a == b); // False — different objects, even though the data is identical
```
For a plain `class`, `==` checks reference identity unless you manually override `Equals`/`GetHashCode`/`==` yourself (tedious, error-prone, easy to forget one).

### `record` — value equality, generated for you
```csharp
public record Coordinate(double Lat, double Lng); // "positional record" — one line

var p1 = new Coordinate(36.75, 3.06);
var p2 = new Coordinate(36.75, 3.06);
Console.WriteLine(p1 == p2);              // True — compares VALUES, not references
Console.WriteLine(ReferenceEquals(p1, p2)); // False — still two distinct heap objects
Console.WriteLine(p1);                     // Coordinate { Lat = 36.75, Lng = 3.06 } — ToString() generated too
```
That one line generates: a constructor, public `init`-only properties, `Equals`/`GetHashCode` based on the values, a readable `ToString()`, and deconstruction support — all things you'd otherwise hand-write on a class.

### Immutability via `init`
```csharp
public record Employee(string Name, string Department, decimal Salary);

var emp = new Employee("Amel", "Engineering", 3000);
// emp.Salary = 3500; // COMPILE ERROR — `init` properties can only be set during construction
```
`init` (instead of `set`) means the property can be assigned in the object initializer / constructor, but never again after that — the object is locked once built.

### `with`-expressions — "modified copies," not mutation
Since you can't mutate a record's properties after creation, you create a **new** record based on an old one, changing only what you need:
```csharp
var raised = emp with { Salary = 3500 }; // copies Name/Department, overrides Salary
Console.WriteLine(emp.Salary);    // 3000 — original untouched
Console.WriteLine(raised.Salary); // 3500 — a brand new object
```
This mirrors the "strings are immutable, operations return new strings" pattern from Lesson 00.5 — same idea, now for your own types.

### When to use `record` vs `class`
- **`record`** — data that represents a value: DTOs, API request/response shapes, event payloads, anything where "same values = same thing" is the right notion of equality, and where you don't want it mutated after creation.
- **`class`** — an entity with identity and behavior that changes over time (Lesson 06's `BankAccount`, Lesson 07's `Employee` with `CalculatePay()`) — here, two accounts with the same balance are still two *different* accounts, so reference equality is correct, and mutable state through controlled methods (not `with`-copies) is the point.
