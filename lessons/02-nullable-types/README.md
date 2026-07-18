# Lesson 02 — Nullable Types

## Why this matters
`NullReferenceException` is historically C#'s most common runtime crash. Nullable value types and nullable reference types are the language's two separate tools for making "this might be absent" an explicit, compiler-checked fact instead of a silent runtime risk.

## The concept
- `int` can **never** be null. `int?` (sugar for `Nullable<int>`) opts in explicitly, and forces you to check `.HasValue` or use `??` before reading `.Value`.
- Reference types (`string`, `class`) are nullable by default at the CLR level — but since C# 8, `string?` vs `string` lets the *compiler* track and warn about null risk, if nullable reference types are enabled in the project.

```csharp
int? maybeAge = null;
if (maybeAge.HasValue) Console.WriteLine(maybeAge.Value);

string? middleName = null;
Console.WriteLine(middleName?.Length ?? 0); // null-conditional + null-coalescing
```

The dangerous pattern is the null-forgiving operator `!`, which tells the compiler "trust me, this isn't null" — and then it is:

```csharp
string middleName = null!;          // lying to the compiler
Console.WriteLine(middleName.Length); // crashes anyway
```

`!` doesn't change runtime behavior at all — it only silences the compiler warning. Using it is a promise you can break.
