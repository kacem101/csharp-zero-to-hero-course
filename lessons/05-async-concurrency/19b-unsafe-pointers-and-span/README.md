# Lesson 19b — unsafe Pointers & Span<T>

## Why this matters
You already think in pointers from C. This lesson closes the loop: C# *does* support raw pointers (in an `unsafe` context) — and then shows you why modern C# almost never uses them, because `Span<T>` gives you the same zero-copy performance with actual safety. This matters directly for Lesson 21 (raw sockets), where you'll be slicing byte buffers constantly.

## The concept

### `unsafe` and raw pointers — C-style, on purpose
C# lets you drop into C-like pointer arithmetic when you explicitly opt in:
```csharp
unsafe
{
    int[] numbers = { 10, 20, 30 };
    fixed (int* ptr = numbers) // "pin" the array so the GC won't move it while you hold a raw pointer to it
    {
        Console.WriteLine(*ptr);       // 10 — dereference, exactly like C
        Console.WriteLine(*(ptr + 1)); // 20 — pointer arithmetic, exactly like C
        *(ptr + 2) = 99;                // write through the pointer
    }
}
```
This requires `<AllowUnsafeBlocks>true</AllowUnsafeBlocks>` in the `.csproj`. `fixed` exists because .NET's garbage collector can *move* objects in memory during a collection — a raw pointer with no `fixed` pin could end up pointing at garbage after a GC cycle. This is exactly the class of bug ("dangling pointer," but caused by a moving GC instead of a `free()`) that safe C# normally protects you from — `unsafe` code opts back into that risk.

**Rule of thumb:** you will almost never need `unsafe` in application code. It exists for interop with native libraries and very specific performance-critical paths.

### `Span<T>` — the safe, modern alternative
`Span<T>` is a **view** over a contiguous block of memory (an array, a slice of an array, or even stack-allocated memory) — no copying, but fully bounds-checked and GC-safe.
```csharp
int[] numbers = { 10, 20, 30, 40, 50 };
Span<int> span = numbers;             // no copy — span just "views" the same memory
Span<int> slice = span.Slice(1, 3);   // {20, 30, 40} — still no copy, just a narrower view
slice[0] = 99;
Console.WriteLine(numbers[1]);        // 99 — the slice writes through to the original array
```
Compare to `array[1..4]` from Lesson 00.3 — that range syntax on a plain array actually **copies** into a new array. `Span<T>.Slice` does not copy — that's the performance win, with none of `unsafe`'s risk.

### `stackalloc` — stack-based buffers via Span
```csharp
Span<byte> buffer = stackalloc byte[16]; // allocated on the STACK, not the heap — no GC involved at all,
                                           // similar to C's alloca(), but bounds-checked through Span
buffer[0] = 0xFF;
```

### Parsing binary data without allocating
```csharp
using System.Buffers.Binary;

Span<byte> header = stackalloc byte[4];
header[0] = 0x00; header[1] = 0x00; header[2] = 0x00; header[3] = 0x2A; // big-endian 42

int length = BinaryPrimitives.ReadInt32BigEndian(header); // 42 — reads directly from the span, zero allocation
```
This is exactly the kind of code you'll write for the length-prefix framing in Lesson 21 — reading a message length out of a raw byte buffer without allocating a new object for every parse.

### Why `Span<T>` can't be a class field
```csharp
class Bad
{
    Span<int> _span; // COMPILE ERROR
}
```
`Span<T>` is a `ref struct` — a special kind of struct the compiler restricts to living only on the stack (it can be a local variable or a method parameter, never a heap-allocated field, never boxed, never captured in a lambda or async method). This restriction is what lets the runtime guarantee a `Span<T>` never outlives the memory it points to, even when that memory is stack-allocated via `stackalloc`.
