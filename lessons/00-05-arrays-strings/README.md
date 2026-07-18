# Lesson 00.5 — Arrays & Strings: the Safety Nets You Didn't Have in C

## Why this matters
C arrays are just a pointer plus a size you have to remember yourself — go one index past the end and you get undefined behavior, maybe a segfault, maybe silent memory corruption. C# arrays know their own length and check every access. This lesson shows you the new safety net and where the sharp edges moved to.

## The concept

### Arrays — fixed size, bounds-checked, self-describing
```csharp
int[] nums = new int[5];       // like `int nums[5];` in C, but zero-initialized always
int[] literal = { 1, 2, 3 };    // like C's `int literal[] = {1, 2, 3};`
Console.WriteLine(literal.Length); // 3 — the array KNOWS its own size, no sizeof(arr)/sizeof(arr[0]) trick needed

int bad = literal[10]; // throws IndexOutOfRangeException — NOT undefined behavior, NOT a silent segfault
```
This is a real behavioral difference worth internalizing: out-of-bounds access in C# is a loud, catchable, well-defined crash — not memory corruption that might not show up until much later.

### Multi-dimensional and jagged arrays
```csharp
int[,] grid = new int[3, 3];         // true 2D array, like C's int grid[3][3]
int[][] jagged = new int[3][];       // array of arrays — each row can be a different length
jagged[0] = new int[] { 1, 2 };
jagged[1] = new int[] { 1, 2, 3, 4 };
```

### Strings — immutable, no null terminator, no manual buffer management
C strings are `char*` arrays you manage yourself: `strlen`, `strcpy`, `strcat`, watching for the `\0` terminator, worrying about buffer overflows. C# `string` is a real, immutable, bounds-safe type:
```csharp
string s = "hello";
Console.WriteLine(s.Length);        // 5 — no strlen() needed, no terminator to count
string upper = s.ToUpper();         // returns a NEW string; `s` itself never changes (Lesson 00.2)
string combined = s + " world";     // + creates a new string; no manual buffer sizing like strcat
```

### Building strings efficiently — `StringBuilder`
Because strings are immutable, concatenating in a loop creates a new string object every iteration — wasteful for many iterations (this mirrors the cost of repeated `realloc`-based growth you'd hand-roll in C, except here it happens invisibly if you're not careful).
```csharp
// Wasteful for large loops — a new string object every iteration
string result = "";
for (int i = 0; i < 10000; i++) result += i.ToString();

// Efficient — one growable internal buffer, appended in place
var sb = new System.Text.StringBuilder();
for (int i = 0; i < 10000; i++) sb.Append(i);
string result2 = sb.ToString();
```

### Reversing a string — since strings are immutable, you build a new one
```csharp
string original = "hello";
char[] chars = original.ToCharArray(); // strings don't support in-place mutation, so drop to a char array
Array.Reverse(chars);
string reversed = new string(chars);
```
