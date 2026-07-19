# Lesson 15 — Delegates, Func/Action, and Events

## Why this matters
Delegates are how C# passes behavior around as data (callbacks, strategies, event handlers). Events are delegates with guardrails — understanding the difference prevents a specific, common bug where any outside code can hijack all of an object's subscribers.

## The concept
A delegate is a type-safe function pointer.

```csharp
public delegate int Operation(int a, int b);
int Add(int a, int b) => a + b;
Operation op = Add;
Console.WriteLine(op(3, 4)); // 7
```

Modern C# almost always uses the built-in generic delegates instead of declaring custom ones:

```csharp
Func<int, int, int> add = (a, b) => a + b;   // has a return value
Action<string> log = message => Console.WriteLine($"[LOG] {message}"); // no return value
Predicate<int> isEven = n => n % 2 == 0;      // returns bool, used for filters/matches
```

### Events — the observer pattern, built in
An `event` restricts a delegate field so outside code can only `+=`/`-=` (subscribe/unsubscribe) — it can never invoke it or overwrite other subscribers.

```csharp
public class Order
{
    public event Action<Order>? Shipped;
    public void Ship() { /* ship it */ Shipped?.Invoke(this); }
}
var order = new Order();
order.Shipped += o => Console.WriteLine("Notify customer.");
order.Ship();
```

The bug a plain public delegate field allows:

```csharp
public class Order2 { public Action<Order2>? Shipped; } // BAD — public field, not `event`
var o = new Order2();
o.Shipped += x => Console.WriteLine("Handler A");
o.Shipped = x => Console.WriteLine("Handler B"); // OVERWRITES Handler A entirely!
```
`event` makes this impossible — the `=` assignment operator simply isn't available to outside code.
