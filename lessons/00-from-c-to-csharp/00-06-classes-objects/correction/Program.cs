using System;

class Node
{
    public int Data;
    public Node? Next;
    public Node(int data) { Data = data; Next = null; }
}

class LinkedList
{
    private Node? _head;

    public void AddFront(int value)
    {
        var newNode = new Node(value) { Next = _head };
        _head = newNode;
    }

    public void PrintAll()
    {
        Node? current = _head;
        while (current != null)
        {
            Console.Write($"{current.Data} -> ");
            current = current.Next;
        }
        Console.WriteLine("null");
    }

    public int Count()
    {
        int count = 0;
        Node? current = _head;
        while (current != null) { count++; current = current.Next; }
        return count;
    }

    public bool Contains(int value)
    {
        Node? current = _head;
        while (current != null)
        {
            if (current.Data == value) return true;
            current = current.Next;
        }
        return false;
    }
}

class Program
{
    static void Main()
    {
        var list = new LinkedList();
        list.AddFront(3);
        list.AddFront(2);
        list.AddFront(1);
        list.PrintAll();               // 1 -> 2 -> 3 -> null
        Console.WriteLine(list.Count()); // 3
        Console.WriteLine(list.Contains(2)); // True
        Console.WriteLine(list.Contains(99)); // False

        Node a = new Node(1);
        Node b = a;       // same object, two names
        b.Data = 99;
        Console.WriteLine(a.Data); // 99 — proves reference semantics
    }
}

// TODO 5 answer: the .NET garbage collector automatically tracks which
// objects are still reachable from any live reference. Once the LAST
// reference to a Node (or an entire chain of Nodes) goes away — e.g. the
// LinkedList itself goes out of scope — the GC will eventually reclaim
// that memory on its own. You no longer need to write a `free_list`
// function, walk the list calling `free()` on each node, or worry about
// forgetting to free a node and leaking memory. (There's a narrower,
// separate concern — non-memory resources like open files or sockets
// still need explicit cleanup — covered later in Lesson 17.)
