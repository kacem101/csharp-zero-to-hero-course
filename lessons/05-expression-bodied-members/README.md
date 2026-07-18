# Lesson 05 — Expression-Bodied Members

## Why this matters
This is a small syntax feature, but knowing *when to stop using it* is a real skill — overusing it produces unreadable one-liners masquerading as "clean code."

## The concept
Expression-bodied members replace `{ return ...; }` with `=> ...;` for genuinely one-expression logic.

```csharp
public double Area => Width * Height;
public override string ToString() => $"{Name} ({Age})";
```

The failure mode is cramming multi-step logic into a single expression to look terse:

```csharp
public double ComputeTotal => Items.Where(i => i.InStock)
    .Select(i => i.Price * (1 - i.Discount))
    .Aggregate(0.0, (acc, p) => acc + p * TaxMultiplier - LoyaltyDiscount(acc));
// unreadable — needs a real method body with named intermediate steps
```

**Rule of thumb:** if you can't read the expression body in one glance and understand what it does, it should be a full method body with named local variables.
