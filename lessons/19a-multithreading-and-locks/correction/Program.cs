using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static async Task RaceConditionDemo()
    {
        int counter = 0;
        void Increment() { for (int i = 0; i < 100_000; i++) counter++; }

        var tasks = new List<Task>();
        for (int i = 0; i < 4; i++) tasks.Add(Task.Run(Increment));
        await Task.WhenAll(tasks);
        Console.WriteLine($"Unsynchronized: {counter} (expected 400000)"); // usually less
    }

    static readonly object _sync = new();

    static async Task LockDemo()
    {
        int counter = 0;
        void Increment() { for (int i = 0; i < 100_000; i++) { lock (_sync) { counter++; } } }

        var tasks = new List<Task>();
        for (int i = 0; i < 4; i++) tasks.Add(Task.Run(Increment));
        await Task.WhenAll(tasks);
        Console.WriteLine($"With lock: {counter}"); // always 400000
    }

    static async Task InterlockedDemo()
    {
        long counter = 0;
        void Increment() { for (int i = 0; i < 100_000; i++) Interlocked.Increment(ref counter); }

        var tasks = new List<Task>();
        for (int i = 0; i < 4; i++) tasks.Add(Task.Run(Increment));
        await Task.WhenAll(tasks);
        Console.WriteLine($"With Interlocked: {counter}"); // always 400000
    }

    // TODO 4 fix: acquire locks in the SAME order as MethodOne (lockA
    // first, then lockB) — consistent ordering everywhere eliminates the
    // circular-wait condition that causes deadlock.
    static readonly object lockA = new(), lockB = new();
    static void MethodOne() { lock (lockA) { lock (lockB) { /* work */ } } }
    static void MethodTwoFixed() { lock (lockA) { lock (lockB) { /* work */ } } } // same order now

    static async Task Main()
    {
        await RaceConditionDemo();
        await LockDemo();
        await InterlockedDemo();
    }
}
