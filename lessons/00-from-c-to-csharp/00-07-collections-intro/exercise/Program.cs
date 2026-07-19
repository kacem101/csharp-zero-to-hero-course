// TODO 1: Create a List<string>, add three names to it with .Add(),
// print its Count, then remove the first one and print the list again
// using string.Join(", ", list).

// TODO 2: Write a method `bool Contains(List<int> list, int target)`
// that does a manual linear search (don't use .Contains() — implement
// the search yourself, like you would in C, to prove you understand
// what's happening underneath).

// TODO 3: Implement a simple stack from scratch using List<int>
// internally:
//   class SimpleStack {
//     private List<int> _items = new();
//     public void Push(int x) => ...
//     public int Pop() => ...       // remove and return the LAST element
//     public bool IsEmpty => ...
//   }
// Push 1, 2, 3 and then Pop twice, printing each popped value (should
// print 3, then 2 — LIFO order).

// TODO 4 (bug hunt): what's wrong with this Pop implementation, and how
// do you fix it in a way that matches proper stack behavior (removing
// from the END, not the START, for O(1) instead of O(n))?
public int Pop()
{
    int first = _items[0];
    _items.RemoveAt(0);
    return first;
}
