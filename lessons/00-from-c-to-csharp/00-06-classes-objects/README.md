# Lesson 00.6 — Classes & Objects: From C structs to C# Objects

## Why this matters
You've built linked lists, trees, and stacks in C using `struct` plus manual `malloc`/`free` and raw pointers. That DSA knowledge transfers directly — C# classes are the natural next step: structured data PLUS behavior (methods), automatic heap allocation, and no manual freeing. This lesson is the bridge from "struct + pointer" thinking to "object" thinking, before Lesson 01 goes deep on the value-vs-reference distinction.

## The concept

### The C way you already know
```c
struct Node {
    int data;
    struct Node *next;
};

struct Node *make_node(int value) {
    struct Node *n = malloc(sizeof(struct Node));
    n->data = value;
    n->next = NULL;
    return n;
}
// ...and somewhere later, you MUST remember to free(n) for every node.
```

### The same idea in C#
```csharp
class Node
{
    public int Data;
    public Node? Next; // '?' means this can be null — same idea as NULL in C

    public Node(int data) // a CONSTRUCTOR — runs automatically when you create a Node
    {
        Data = data;
        Next = null;
    }
}

Node n = new Node(5); // `new` allocates on the heap AND calls the constructor in one step
```
Notice what's gone: no `malloc`, no `sizeof`, no manual `NULL` check pattern to set up the struct correctly every time — the constructor guarantees every `Node` starts in a valid state. And crucially: **no `free()`**. The .NET garbage collector automatically reclaims a `Node` once nothing references it anymore. You cannot double-free, and you cannot (accidentally, in safe C#) create a dangling pointer to freed memory.

### Fields, methods, and `this`
A C struct only holds data; any function that operates on it is separate and takes the struct as a parameter (`void print_node(struct Node *n)`). A C# class bundles data (**fields**) and behavior (**methods**) together:
```csharp
class Node
{
    public int Data;
    public Node? Next;

    public Node(int data) { Data = data; }

    public void Print() => Console.WriteLine(Data); // a method — belongs to the object
}
Node n = new Node(5);
n.Print(); // called ON the object, like n->print() if C had that syntax
```
`this` inside a method refers to the current object instance — comparable to how in C you'd pass `struct Node *self` as an explicit first parameter; C# does that implicitly for every method.

### Reference semantics — like C pointers, but memory-safe
A C# class variable holds a reference to the object, exactly like a C pointer holds an address:
```csharp
Node a = new Node(1);
Node b = a;       // b points to the SAME object as a — like `struct Node *b = a;` in C
b.Data = 99;
Console.WriteLine(a.Data); // 99 — both variables see the same object
```
The difference from raw C pointers: you can't do pointer arithmetic on a reference, you can't accidentally read freed memory, and `null` reference usage is caught by the runtime as a clear `NullReferenceException` instead of an undefined-behavior crash. (Lesson 01 covers this reference-semantics topic in much more depth, including when to use `struct` instead of `class`.)

### Putting it together: a linked list, leveraging your DSA background
```csharp
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
}
```
This is the exact same algorithm you already know from DSA in C — traverse with a `current` pointer/reference until it's `null`. The only things that changed are syntax and the fact that you never have to `free` a node yourself.
