# Lesson 00.2 — Variables, Types & Operators: the C# vs C Cheat Sheet

## Why this matters
C# is statically typed, just like C — this is the part of C# that will feel most familiar. The differences are small but matter: no format specifiers, immutable strings, and a `var` keyword that infers types without becoming "loose" typing.

## The concept

### Primitive types — mostly a direct mapping from C
| C | C# | Notes |
|---|---|---|
| `int` | `int` | both typically 32-bit |
| `long` | `long` | 64-bit |
| `float` | `float` | |
| `double` | `double` | |
| `char` | `char` | C# `char` is a 16-bit Unicode code unit, not 1 byte like C |
| `_Bool` / `int` as bool | `bool` | a real boolean type, only `true`/`false` — no "any nonzero int is truthy" |
| `char*` (as a string) | `string` | a real, immutable, built-in type — see below |

```csharp
int count = 10;
double price = 19.99;
bool isActive = true;
char grade = 'A';
string name = "Belkacem"; // NOT a char array — a full type with its own methods
```

### `var` — type inference, not dynamic typing
```csharp
var count = 10;       // compiler infers `int` — count is STILL an int forever, just like `int count = 10;`
var price = 19.99;     // inferred as `double`
// count = "hello";    // COMPILE ERROR — var is not like Python/JS dynamic typing
```
`var` just saves typing when the type is obvious from the right-hand side. The variable's type is fixed at compile time exactly as if you'd written it explicitly.

### String interpolation instead of format specifiers
```csharp
int x = 5, y = 3;
Console.WriteLine($"{x} + {y} = {x + y}"); // no %d, no matching arg count/order by hand
Console.WriteLine($"Price: {price:F2}");    // :F2 = 2 decimal places, closest equivalent to %.2f
```

### Strings are immutable
In C, you mutate a `char[]` in place with `strcpy`, `strcat`, manual indexing. In C#, every string operation produces a **new** string — the original is never changed.
```csharp
string s = "hello";
s = s.ToUpper(); // does NOT mutate the original string object — creates a new one and reassigns s
```
Concatenating in a loop creates a new string object every single iteration — fine for a few iterations, wasteful for thousands (this is what `StringBuilder`, covered in Lesson 00.5, is for).

### Operators — almost identical to C
Arithmetic (`+ - * / %`), comparison (`== != < > <= >=`), logical (`&& || !`), and bitwise (`& | ^ ~ << >>`) all work exactly like C. Two differences worth knowing:
- `==` on `string` compares **content**, not memory address (unlike raw `char*` comparison in C, which compares pointers unless you use `strcmp`).
- Integer division truncates just like C: `7 / 2 == 3`.
