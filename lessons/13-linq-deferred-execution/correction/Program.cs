using System;
using System.Collections.Generic;
using System.Linq;

record Employee(string Name, string Department);

class Program
{
    static void Main()
    {
        var numbers = new List<int> { 1,2,3,4,5,6,7,8,9,10 };

        // TODO 1
        var evenSquares = numbers.Where(n => n % 2 == 0).Select(n => n * n);
        Console.WriteLine(string.Join(", ", evenSquares)); // 4, 16, 36, 64, 100

        // TODO 2
        var threshold = 5;
        var bigNumbers = numbers.Where(n => n > threshold); // NOT run yet
        Console.WriteLine("query built");
        numbers.Add(100); // mutate BEFORE enumeration
        Console.WriteLine(string.Join(", ", bigNumbers));
        // 100 DOES show up, because the query re-scans the live list only
        // when enumerated — it was never "snapshotted" at build time.
        // This is exactly the kind of surprise deferred execution causes
        // if you assume a query "runs" the moment you write it.

        // TODO 3
        var employees = new List<Employee>
        {
            new("Amel", "Engineering"), new("Yanis", "Engineering"), new("Sara", "Sales")
        };
        var counts = employees.GroupBy(e => e.Department)
                               .Select(g => new { Department = g.Key, Count = g.Count() });
        foreach (var c in counts) Console.WriteLine($"{c.Department}: {c.Count}");

        // TODO 4 fix: materialize ONCE with .ToList(), then reuse the
        // list for both operations — avoids re-running the filter/select
        // pipeline a second time.
        var materialized = numbers.Where(n => n % 2 == 0).Select(n => n * n).ToList();
        int fixedCount = materialized.Count;
        var fixedList = materialized; // same list, no re-enumeration
        Console.WriteLine(fixedCount);
    }
}
