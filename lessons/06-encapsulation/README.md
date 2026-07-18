# Lesson 06 — Encapsulation

## Why this matters
Encapsulation isn't "make fields private for style points." It's about protecting **invariants** — facts that must always be true about an object. If any outside code can put the object into an invalid state, encapsulation has failed, no matter how many fields are marked `private`.

## The concept

```csharp
public class BankAccount
{
    public decimal Balance { get; private set; }

    public void Deposit(decimal amount)
    {
        if (amount <= 0) throw new ArgumentException("Deposit must be positive.");
        Balance += amount;
    }

    public void Withdraw(decimal amount)
    {
        if (amount > Balance) throw new InvalidOperationException("Insufficient funds.");
        Balance -= amount;
    }
}
```

The invariant: "Balance can never go negative." Nothing outside this class can violate it, because `Balance` has no public setter — the only doors in are `Deposit` and `Withdraw`, both of which enforce the rule.

The most common failure is exposing a mutable collection directly:

```csharp
public class Order
{
    public List<OrderItem> Items = new(); // BAD
}
order.Items.Clear();     // anyone can wipe the order
order.Items.Add(null!);  // or insert garbage
```

Fix: expose read-only access, mutate only through methods that enforce rules.

```csharp
public class Order
{
    private readonly List<OrderItem> _items = new();
    public IReadOnlyList<OrderItem> Items => _items;

    public void AddItem(OrderItem item)
    {
        if (item.Quantity <= 0) throw new ArgumentException("Quantity must be positive.");
        _items.Add(item);
    }
}
```
