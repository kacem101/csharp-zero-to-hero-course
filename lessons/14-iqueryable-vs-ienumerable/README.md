# Lesson 14 — IQueryable vs IEnumerable

## Why this matters
This is a top real-world performance bug in database-backed apps: accidentally pulling an entire table into memory before filtering it, instead of letting the database do the filtering.

## The concept

```csharp
IQueryable<Product> query = db.Products.Where(p => p.Price > 100); // translated to SQL
var results = query.ToList(); // SQL: SELECT * FROM Products WHERE Price > 100
```

`IQueryable<T>` builds an **expression tree** describing the query, which the database provider (e.g. EF Core) translates into SQL and runs on the server — only matching rows come back over the wire.

The trap:

```csharp
List<Product> all = db.Products.ToList();      // SELECT * FROM Products — everything!
var expensive = all.Where(p => p.Price > 100);  // filtering now happens in C#, in memory
```

Calling `.ToList()` (or any `IEnumerable`-only method) too early converts a targeted SQL query into "download the entire table, then filter in memory" — this can mean downloading millions of rows to keep a handful.

**Rule of thumb:** keep the query as `IQueryable` for as long as possible — apply every `.Where()`, `.OrderBy()`, `.Select()` you can *before* calling `.ToList()`/`.ToArray()`/`foreach`.
