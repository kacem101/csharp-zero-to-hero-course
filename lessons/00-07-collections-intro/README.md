# Lesson 00.7 — Intro to Collections: List<T> as a Safer, Growable Array

## Why this matters
In C, a "dynamic array" means hand-rolling `realloc`-based growth logic yourself (you've probably implemented this in a DSA assignment). C# gives you this as a built-in, battle-tested type: `List<T>`. This lesson connects your DSA knowledge directly to the tool you'll reach for constantly in real C# code — full depth on choosing between collections comes later in Lesson 12.

## The concept

### The C way you already know
```c
int *arr = malloc(capacity * sizeof(int));
int count = 0;
// ... when count == capacity:
capacity *= 2;
arr = realloc(arr, capacity * sizeof(int));
arr[count++] = value;
// ...and you must remember to free(arr) when done.
```

### `List<T>` does all of that for you
```csharp
List<int> list = new List<int>(); // starts empty, grows automatically
list.Add(10);
list.Add(20);
list.Add(30);
Console.WriteLine(list.Count);     // 3 — like your `count` variable, but always accurate and read-only from outside
Console.WriteLine(list[1]);        // 20 — indexing works just like an array
list.RemoveAt(0);                  // removes the element at index 0, shifts everything else down — you'd hand-roll this in C
Console.WriteLine(list.Contains(30)); // true — a linear search you'd otherwise write yourself
```
`<int>` is the generic type parameter (Lesson 11 covers generics in depth) — it just means "this List holds `int`s specifically," so you get full type safety and no casting, unlike a C `void*`-based generic container.

### foreach works on List<T> exactly like arrays
```csharp
foreach (int x in list) Console.WriteLine(x);
```

### Growth is automatic and amortized — same algorithmic idea as your `realloc` doubling
Internally, `List<T>` doubles its backing array's capacity when it runs out of room, exactly like the C pattern above — you get the same amortized O(1) `Add` performance, without writing the resizing logic yourself.

### Building DSA structures on top of List<T>
You can implement a stack (LIFO) using `List<T>` directly, using `Add`/`RemoveAt(Count - 1)` for push/pop — this is a great way to solidify both your DSA understanding and C# syntax at once. (C# also ships a dedicated, more efficient `Stack<T>` and `Queue<T>` — covered properly in Lesson 12 — but building one yourself first is the point of this exercise.)
