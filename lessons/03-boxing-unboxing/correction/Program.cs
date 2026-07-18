using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

class Program
{
    static long SumWithArrayList()
    {
        var list = new ArrayList();
        for (int i = 0; i < 1_000_000; i++) list.Add(i); // boxes every int
        long sum = 0;
        foreach (var item in list) sum += (int)item; // unboxes every int
        return sum;
    }

    static long SumWithGenericList()
    {
        var list = new List<int>();
        for (int i = 0; i < 1_000_000; i++) list.Add(i); // no boxing
        long sum = 0;
        foreach (var item in list) sum += item;
        return sum;
    }

    static void Main()
    {
        var sw = Stopwatch.StartNew();
        SumWithArrayList();
        sw.Stop();
        Console.WriteLine($"ArrayList: {sw.ElapsedMilliseconds} ms");

        sw.Restart();
        SumWithGenericList();
        sw.Stop();
        Console.WriteLine($"List<int>: {sw.ElapsedMilliseconds} ms");
        // ArrayList is reliably slower — every Add/read boxes/unboxes.
    }
}

// TODO 2 answer: List<object> is generic over `object`, so any struct you
// add is implicitly converted to `object` — that conversion IS boxing.
// List<Vector2> is generic over the concrete struct type, so the struct
// is stored inline in the list's backing array, never converted to
// `object`. The whole point of generics is avoiding this conversion.

// TODO 3 fix: don't mix types in an object[] if you plan to treat them
// uniformly. Either use a strongly-typed collection per type, or check
// the type before casting:
static void SafeSum(object[] items)
{
    int sum = 0;
    foreach (var item in items)
        if (item is int n) sum += n; // safe pattern-matching cast, skips non-ints
    Console.WriteLine(sum);
}
