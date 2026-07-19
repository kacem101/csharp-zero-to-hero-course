// TODO 1: Design an `ILogger` interface with `void Log(string message)`.
// Implement it in two unrelated classes: `ConsoleLogger` and `FileLogger`
// (FileLogger can just append to a List<string> to simulate a file for
// this exercise). This is the "unrelated types, shared capability" case.

// TODO 2: Design an abstract class `PaymentMethod` with a constructor
// taking `string AccountLabel`, an abstract `bool Charge(decimal amount)`,
// and a shared concrete method `void PrintReceipt(decimal amount)` that
// uses AccountLabel and calls Charge. Implement `CreditCard` and
// `BankTransfer` subclasses. This is the "related types, shared code"
// case.

// TODO 3: A class can implement multiple interfaces but inherit only one
// class. Write a `class SmartCreditCard : PaymentMethod, ILogger` that
// combines both from above, logging every charge attempt.
