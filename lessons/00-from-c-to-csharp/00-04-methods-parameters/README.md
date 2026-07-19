# Lesson 00.4 — Methods & Parameters: ref/out vs C Pointers

## Why this matters
You've used pointers in C to let a function modify a caller's variable (`void swap(int *a, int *b)`). C# gives you a cleaner tool for exactly that use case — `ref` and `out` — without ever touching a raw memory address.

## The concept

### Methods = C functions, same value-passing rule
Just like C, C# passes arguments **by value** by default — the method gets a copy.
```csharp
static void TryIncrement(int x) { x++; } // only changes the local copy
int n = 5;
TryIncrement(n);
Console.WriteLine(n); // still 5
```

### `ref` — C#'s answer to "pass a pointer so I can modify the caller's variable"
In C you'd write `void swap(int *a, int *b) { int tmp = *a; *a = *b; *b = tmp; }` and call `swap(&x, &y)`. C# does the same job without exposing an address:
```csharp
static void Swap(ref int a, ref int b)
{
    int tmp = a;
    a = b;
    b = tmp;
}
int x = 1, y = 2;
Swap(ref x, ref y); // `ref` required at BOTH the method signature AND the call site — no accidental pass-by-reference
Console.WriteLine($"{x}, {y}"); // 2, 1
```

### `out` — for when a method needs to hand back an extra result
`out` is like `ref`, but the variable doesn't need a value going in — the method is required to assign it before returning. This is exactly the pattern behind `int.TryParse`:
```csharp
static bool TryDivide(int a, int b, out int result)
{
    if (b == 0) { result = 0; return false; }
    result = a / b;
    return true;
}
if (TryDivide(10, 2, out int quotient)) Console.WriteLine(quotient); // 5
```

### Method overloading — no C equivalent
C requires unique function names (`add_int`, `add_double`). C# lets multiple methods share a name if their parameter lists differ:
```csharp
static int Add(int a, int b) => a + b;
static double Add(double a, double b) => a + b;
```
The compiler picks the right one based on the argument types you pass — no name-mangling to think about yourself.

### Default parameter values — no C equivalent
```csharp
static void Greet(string name, string greeting = "Hello") => Console.WriteLine($"{greeting}, {name}!");
Greet("Belkacem");            // "Hello, Belkacem!"
Greet("Belkacem", "Welcome"); // "Welcome, Belkacem!"
```
