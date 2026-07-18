# Lesson 00.3 — Control Flow: if/else, Loops, switch

## Why this matters
This is the part of C# that will feel almost identical to C — good news, one less thing to relearn. The main new tool is `foreach`, which removes an entire category of off-by-one indexing bugs you've probably hit in C.

## The concept

### if/else, while, for — essentially unchanged from C
```csharp
int n = 7;
if (n % 2 == 0) Console.WriteLine("even");
else Console.WriteLine("odd");

for (int i = 0; i < 5; i++) Console.WriteLine(i);

int j = 0;
while (j < 5) { Console.WriteLine(j); j++; }
```

### `foreach` — new, and it removes manual indexing
In C, iterating an array means managing an index yourself:
```c
for (int i = 0; i < n; i++) { printf("%d\n", arr[i]); }
```
C# gives you `foreach` for any collection (arrays, `List<T>`, etc.) — no index variable, no off-by-one risk:
```csharp
int[] arr = { 1, 2, 3, 4, 5 };
foreach (int x in arr) Console.WriteLine(x);
```
Use a regular `for` loop when you actually need the index (or need to iterate backwards, or skip elements); use `foreach` whenever you just need "each element, in order."

### switch — statement and expression forms
```csharp
// switch statement — like C's switch, but NO fallthrough by default
// (each case needs `break;` in C; C# case blocks aren't allowed to
// fall through silently — you must explicitly `goto case` if you want that)
switch (n)
{
    case 0: Console.WriteLine("zero"); break;
    case 1: Console.WriteLine("one"); break;
    default: Console.WriteLine("many"); break;
}

// switch EXPRESSION (C# 8+, no C equivalent) — more on this in Lesson 04
string label = n switch { 0 => "zero", 1 => "one", _ => "many" };
```

### Ranges and indices (no C equivalent)
```csharp
int[] nums = { 10, 20, 30, 40, 50 };
Console.WriteLine(nums[^1]);      // last element — 50 (^1 means "1 from the end")
int[] slice = nums[1..3];         // elements at index 1 and 2 (end index exclusive) — {20, 30}
```
