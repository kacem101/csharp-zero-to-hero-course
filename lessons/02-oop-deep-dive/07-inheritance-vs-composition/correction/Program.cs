using System;
using System.Collections.Generic;

// TODO 1 fix: a Queue is not a List — exposing List<string>'s full API
// (Insert at arbitrary index, sort, index-based removal) breaks FIFO
// guarantees. Composition instead:
class Queue
{
    private readonly List<string> _items = new();
    public void Enqueue(string item) => _items.Add(item);
    public string Dequeue()
    {
        var first = _items[0];
        _items.RemoveAt(0);
        return first;
    }
    public int Count => _items.Count;
}

// TODO 2
class Engine
{
    public void Start() => Console.WriteLine("Engine starting...");
}
class Car
{
    private readonly Engine _engine = new(); // Car HAS-A Engine
    public void Drive()
    {
        _engine.Start();
        Console.WriteLine("Driving...");
    }
}

// TODO 3 — legitimate inheritance: a SalariedEmployee genuinely IS an
// Employee (can be used anywhere Employee is expected, e.g. in a
// List<Employee> payroll run), and they share real state (Name) and
// behavior (PrintPaystub), differing only in how pay is calculated.
abstract class Employee
{
    public string Name { get; }
    protected Employee(string name) => Name = name;
    public abstract decimal CalculatePay();
    public void PrintPaystub() => Console.WriteLine($"{Name}: {CalculatePay():C}");
}
class SalariedEmployee : Employee
{
    private readonly decimal _monthlySalary;
    public SalariedEmployee(string name, decimal monthlySalary) : base(name) => _monthlySalary = monthlySalary;
    public override decimal CalculatePay() => _monthlySalary;
}
class HourlyEmployee : Employee
{
    private readonly decimal _rate; private readonly int _hours;
    public HourlyEmployee(string name, decimal rate, int hours) : base(name) { _rate = rate; _hours = hours; }
    public override decimal CalculatePay() => _rate * _hours;
}

class Program
{
    static void Main()
    {
        var q = new Queue();
        q.Enqueue("a"); q.Enqueue("b");
        Console.WriteLine(q.Dequeue()); // a

        new Car().Drive();

        List<Employee> payroll = new() { new SalariedEmployee("Amel", 3000), new HourlyEmployee("Yanis", 15, 160) };
        foreach (var e in payroll) e.PrintPaystub();
    }
}
