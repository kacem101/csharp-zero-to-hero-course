# Lesson 10 — SOLID Principles

## Why this matters
SOLID isn't decoration — each letter names a specific, recognizable failure mode you will hit in real codebases. Knowing the names helps you spot and fix them fast.

## The concept
- **S**ingle Responsibility — a class should have one reason to change.
- **O**pen/Closed — open for extension, closed for modification.
- **L**iskov Substitution — covered in Lesson 08.
- **I**nterface Segregation — many small interfaces beat one large one.
- **D**ependency Inversion — depend on abstractions, not concretions.

**Single Responsibility violation:**
```csharp
public class Invoice
{
    public decimal CalculateTotal() { ... }
    public void SaveToDatabase() { /* SQL */ }     // persistence concern
    public void SendEmailReceipt() { /* SMTP */ }  // notification concern
}
// Three unrelated reasons to change: business rules, DB schema, email provider.
```
Fix: split by responsibility into `Invoice`, `InvoiceRepository`, `ReceiptEmailer`.

**Dependency Inversion violation:**
```csharp
public class OrderService
{
    private readonly SqlServerRepository _repo = new(); // depends on a CONCRETE class
    public void Place(Order order) => _repo.Save(order);
}
// Can't test without a real SQL Server. Can't swap databases.
```
Fix: depend on an interface, inject the implementation.
```csharp
public interface IOrderRepository { void Save(Order order); }
public class OrderService
{
    private readonly IOrderRepository _repo;
    public OrderService(IOrderRepository repo) => _repo = repo;
    public void Place(Order order) => _repo.Save(order);
}
```
