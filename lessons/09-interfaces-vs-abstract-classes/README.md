# Lesson 09 — Interfaces vs Abstract Classes

## Why this matters
Both let you code against a contract instead of a concrete type — but they solve different problems. Picking the wrong one leads either to duplicated code (interface everywhere) or to forced, unnatural hierarchies (abstract class everywhere).

## The concept

| | Interface | Abstract class |
|---|---|---|
| Implementation | none (default interface members are rare) | can share real implementation |
| State (fields) | no | yes |
| Multiple inheritance | a class can implement many | a class can inherit only one |
| Use when | unrelated types share a *capability* | related types share *code and state* |

```csharp
public interface IComparable<T> { int CompareTo(T other); }
// Money and Temperature can both be IComparable despite being unrelated domains.

public abstract class Employee
{
    public string Name { get; }
    protected Employee(string name) => Name = name;
    public abstract decimal CalculatePay();
    public void PrintPaystub() => Console.WriteLine($"{Name}: {CalculatePay():C}"); // shared code
}
```

Misusing an interface where related types keep duplicating the same logic:

```csharp
public interface IEmployee { decimal CalculatePay(); }
public class SalariedEmployee : IEmployee { public decimal CalculatePay() {...} public void PrintPaystub() {...} }
public class HourlyEmployee : IEmployee { public decimal CalculatePay() {...} public void PrintPaystub() {...} }
// PrintPaystub is duplicated in every implementer
```
