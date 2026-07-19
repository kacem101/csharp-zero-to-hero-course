using System;
using System.Buffers.Binary;

class Program
{
    static unsafe void PointerTraversal(int[] numbers)
    {
        fixed (int* ptr = numbers)
        {
            for (int i = 0; i < numbers.Length; i++)
                Console.Write($"{*(ptr + i)} ");
        }
        Console.WriteLine();
    }

    static void SpanTraversal(int[] numbers)
    {
        Span<int> span = numbers;
        for (int i = 0; i < span.Length; i++)
            Console.Write($"{span[i]} ");
        Console.WriteLine();
    }

    static void Main()
    {
        int[] nums = { 1, 2, 3, 4, 5 };
        PointerTraversal(nums); // requires <AllowUnsafeBlocks>true</AllowUnsafeBlocks> in the .csproj
        SpanTraversal(nums);

        Span<byte> buffer = stackalloc byte[8];
        BinaryPrimitives.WriteInt32BigEndian(buffer.Slice(0, 4), 42);
        BinaryPrimitives.WriteInt32BigEndian(buffer.Slice(4, 4), 1000);

        int first = BinaryPrimitives.ReadInt32BigEndian(buffer.Slice(0, 4));
        int second = BinaryPrimitives.ReadInt32BigEndian(buffer.Slice(4, 4));
        Console.WriteLine($"{first}, {second}"); // 42, 1000 — parsed with zero heap allocations
    }
}

// TODO 4 answer: Span<T> is a `ref struct`, which the compiler forbids
// from being a field of an ordinary (heap-allocated) class. The reason
// is safety: a class instance can outlive the stack frame (or
// stackalloc'd memory) that a Span might be pointing at, which would
// leave a "dangling" Span with no way to detect it — exactly the
// dangling-pointer bug `unsafe`/C has, reintroduced through the back
// door. By banning ref structs from heap-allocated locations entirely,
// the compiler guarantees a Span can never outlive the memory it views.
