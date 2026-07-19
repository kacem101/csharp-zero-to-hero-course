using System;

record Coordinate(double Lat, double Lng);
record Employee(string Name, string Department, decimal Salary);

class Program
{
    static void Main()
    {
        // TODO 1
        var p1 = new Coordinate(36.75, 3.06);
        var p2 = new Coordinate(36.75, 3.06);
        Console.WriteLine(p1 == p2);                 // True
        Console.WriteLine(ReferenceEquals(p1, p2));   // False

        // TODO 2
        var emp = new Employee("Amel", "Engineering", 3000);
        var raised = emp with { Salary = emp.Salary * 1.15m };
        Console.WriteLine($"Original: {emp.Salary}, Raised: {raised.Salary}");

        // TODO 3
        var (lat, lng) = p1;
        Console.WriteLine($"lat={lat}, lng={lng}");

        // TODO 4 fix: Salary is an `init`-only property — it can only be
        // set inside the object initializer at construction time, never
        // afterward. To change it, create a new record with a
        // with-expression instead of assigning directly:
        var emp2 = new Employee("Yanis", "Sales", 2000);
        var updated = emp2 with { Salary = 2200 };
        Console.WriteLine(updated);
    }
}
