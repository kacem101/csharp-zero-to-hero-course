using System;
using System.Collections.Generic;
using System.Linq;

class Pair<T1, T2>
{
    public T1 First; public T2 Second;
    public Pair(T1 first, T2 second) { First = first; Second = second; }
    public Pair<T2, T1> Swap() => new Pair<T2, T1>(Second, First);
}

class Program
{
    static T FindMax<T>(List<T> items) where T : IComparable<T>
    {
        if (items.Count == 0) throw new InvalidOperationException("Empty list");
        T max = items[0];
        foreach (var item in items.Skip(1))
            if (item.CompareTo(max) > 0) max = item;
        return max;
    }

    static void Main()
    {
        var pair = new Pair<string, int>("age", 25);
        var swapped = pair.Swap();
        Console.WriteLine($"{swapped.First}, {swapped.Second}"); // 25, age

        Console.WriteLine(FindMax(new List<int> { 3, 7, 2, 9, 1 })); // 9

        var repo = new Repository<string>();
        repo.Add("hello"); repo.Add("world");
        Console.WriteLine(repo.Find(s => s.StartsWith("w"))); // world
    }
}

class Repository<T> where T : class
{
    private readonly List<T> _items = new();
    public void Add(T item) => _items.Add(item);
    public IReadOnlyList<T> GetAll() => _items;
    public T? Find(Func<T, bool> predicate) => _items.FirstOrDefault(predicate);
}

// TODO 4 fix: `T == null` fails to compile for unconstrained T because
// value types can never be null, and the compiler can't know at compile
// time whether T will be a value or reference type. Fix with
// EqualityComparer<T>.Default, which works for both:
public class Wrapper<T>
{
    public T? Value;
    public bool IsDefault() => EqualityComparer<T>.Default.Equals(Value, default(T)!);
}
