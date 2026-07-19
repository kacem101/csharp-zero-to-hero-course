using System;
using System.Linq;

record Order(int CustomerId, decimal Amount);

class Program
{
    static void Main()
    {
        // Simulated in-memory stand-in for a real IQueryable<Order> from EF Core.
        IQueryable<Order> Orders = new[]
        {
            new Order(42, 30), new Order(42, 80), new Order(7, 200), new Order(42, 60)
        }.AsQueryable();

        // TODO 1 — stays IQueryable through every filter; only Sum()
        // (which triggers execution) touches the database, and only for
        // the rows that already matched both conditions server-side.
        decimal total = Orders
            .Where(o => o.CustomerId == 42)
            .Where(o => o.Amount > 50)
            .Sum(o => o.Amount);
        Console.WriteLine(total); // 140

        // TODO 2 answer: `Orders.ToList()` on the very first line is the
        // problem — it materializes EVERY order from the database into
        // memory before any filtering happens at all. Every .Where()
        // after that runs in C#, over data that should never have left
        // the database in the first place.

        // TODO 3 fix — chain all filters BEFORE any materializing call:
        decimal totalFixed = Orders
            .Where(o => o.CustomerId == 42)
            .Where(o => o.Amount > 50)
            .Sum(o => o.Amount); // same as TODO 1 — the fix IS keeping it IQueryable
        Console.WriteLine(totalFixed);
    }
}
