using System;
using System.Collections.Generic;

class SimpleStack
{
    private List<int> _items = new();
    public void Push(int x) => _items.Add(x);
    public int Pop()
    {
        int last = _items[_items.Count - 1];
        _items.RemoveAt(_items.Count - 1); // removing from the END is O(1); removing from the START would be O(n) — shifts every remaining element
        return last;
    }
    public bool IsEmpty => _items.Count == 0;
}

class Program
{
    static bool Contains(List<int> list, int target)
    {
        for (int i = 0; i < list.Count; i++)
            if (list[i] == target) return true;
        return false;
    }

    static void Main()
    {
        var names = new List<string>();
        names.Add("Amel");
        names.Add("Yanis");
        names.Add("Sara");
        Console.WriteLine(names.Count); // 3
        names.RemoveAt(0);
        Console.WriteLine(string.Join(", ", names)); // Yanis, Sara

        var nums = new List<int> { 4, 8, 15, 16, 23, 42 };
        Console.WriteLine(Contains(nums, 15)); // True
        Console.WriteLine(Contains(nums, 99)); // False

        var stack = new SimpleStack();
        stack.Push(1);
        stack.Push(2);
        stack.Push(3);
        Console.WriteLine(stack.Pop()); // 3
        Console.WriteLine(stack.Pop()); // 2
    }
}

// TODO 4 fix is shown directly in SimpleStack.Pop() above: pop from
// _items.Count - 1 (the end), not index 0 (the start). Removing from
// the front of a List<T> is O(n) because every remaining element has to
// shift left by one — exactly the same cost as shifting elements in a
// C array after removing arr[0]. Removing from the end is O(1) because
// nothing else needs to move.
