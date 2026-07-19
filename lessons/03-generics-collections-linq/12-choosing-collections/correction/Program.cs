using System;
using System.Collections.Generic;

record Ticket(int Id, string Subject);

class Program
{
    // TODO 1 — Dictionary<int,string>: O(1) average lookup by key.
    static string? LookupUsername(Dictionary<int, string> users, int id)
        => users.TryGetValue(id, out var name) ? name : null;

    // TODO 2 — Queue<T>: FIFO matches "oldest first" processing exactly.
    static Ticket ProcessNext(Queue<Ticket> tickets) => tickets.Dequeue();

    // TODO 3 — HashSet<string>: O(1) average membership check + insert,
    // no duplicates, order irrelevant.
    static bool VisitIfNew(HashSet<string> visited, string url) => visited.Add(url); // Add returns false if already present

    // TODO 4 — Stack<T>: LIFO matches "undo the most recent action" exactly.
    static void Main()
    {
        var users = new Dictionary<int, string> { { 1, "belkacem" }, { 2, "amel" } };
        Console.WriteLine(LookupUsername(users, 1)); // belkacem

        var tickets = new Queue<Ticket>();
        tickets.Enqueue(new Ticket(1, "Login issue"));
        tickets.Enqueue(new Ticket(2, "Payment failed"));
        Console.WriteLine(ProcessNext(tickets).Subject); // Login issue — the oldest one

        var visited = new HashSet<string>();
        Console.WriteLine(VisitIfNew(visited, "a.com")); // True
        Console.WriteLine(VisitIfNew(visited, "a.com")); // False — already seen

        var undoStack = new Stack<string>();
        undoStack.Push("state1");
        undoStack.Push("state2");
        Console.WriteLine(undoStack.Pop()); // state2 — most recent first
    }
}
