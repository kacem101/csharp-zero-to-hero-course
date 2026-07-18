// TODO 1: In an `unsafe` block, take an int[] of 5 elements, get a
// pointer to it with `fixed`, and print each element using pointer
// arithmetic (*(ptr + i)) in a loop instead of array indexing.

// TODO 2: Do the same traversal SAFELY using Span<int> instead — no
// unsafe, no fixed — and confirm you get the same output.

// TODO 3: Use `stackalloc` to create a Span<byte> of 8 bytes. Write a
// 32-bit big-endian integer into the first 4 bytes using
// BinaryPrimitives.WriteInt32BigEndian, and a second one into the next
// 4 bytes. Read both back with ReadInt32BigEndian on appropriate slices
// and print them.

// TODO 4 (bug hunt): why won't this compile, and what does the error
// tell you about Span<T>'s safety guarantees?
class Buffer
{
    public Span<byte> Data; // ???
}
