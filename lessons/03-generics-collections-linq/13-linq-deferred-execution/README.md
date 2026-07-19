# Lesson 13 — LINQ & Deferred Execution

## Why this matters
LINQ's `Where`/`Select`/etc. don't run when you write them — they run when you *enumerate* the result (a `foreach`, `.ToList()`, etc.). Not understanding this causes bugs where a query silently uses stale data, or runs far more times than expected.

## The concept
LINQ methods are extension methods over `IEnumerable<T>`, built on C# iterators (`yield return`), and they're **lazily evaluated**.

```csharp
IEnumerable<int> Numbers()
{
    Console.WriteLine("Generating 1");
    yield return 1;
    Console.WriteLine("Generating 2");
    yield return 2;
}

var query = Numbers().Where(n => n > 0); // nothing printed yet
Console.WriteLine("Query built.");
foreach (var n in query) Console.WriteLine($"Got {n}"); // NOW it runs
```
Output proves it: "Query built." prints *before* "Generating 1" — the query definition and its execution are two separate steps.

**Consequence:** if you enumerate the same query twice (two `foreach` loops over the same `IEnumerable<T>` built from, say, a database call or a file read), the underlying work runs *twice*. If that's not what you want, call `.ToList()` once and reuse the list.
