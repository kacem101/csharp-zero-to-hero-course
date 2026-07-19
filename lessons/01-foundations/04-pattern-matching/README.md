# Lesson 04 — Pattern Matching

## Why this matters
Before C# 7, inspecting "what kind of thing is this" meant manual casting and nested `if`s. Pattern matching lets `switch` and `is` inspect a value's *shape* — type, range, structure — directly and safely, which eliminates a whole category of cast-related bugs.

## The concept

```csharp
static string Describe(object obj) => obj switch
{
    int n when n < 0 => "negative number",
    int n => $"positive number: {n}",
    string s when string.IsNullOrEmpty(s) => "empty string",
    string s => $"string of length {s.Length}",
    null => "nothing at all",
    _ => "something else"
};
```

Compare to the old style:

```csharp
static string Describe(object obj)
{
    if (obj is int)
    {
        int n = (int)obj;   // manual cast, easy to forget or get wrong
        if (n < 0) return "negative number";
        return $"positive number: {n}";
    }
    // repeated boilerplate for every type...
    return "something else";
}
```

The switch expression version reads top-to-bottom as "if it looks like this, do that" — no manual casts, no risk of an `InvalidCastException`, and the compiler can warn you if you miss a case.
