using System;
using System.Collections.Generic;

interface ILogger { void Log(string message); }

class ConsoleLogger : ILogger { public void Log(string message) => Console.WriteLine($"[console] {message}"); }
class FileLogger : ILogger
{
    private readonly List<string> _fakeFile = new();
    public void Log(string message) => _fakeFile.Add(message);
    public IReadOnlyList<string> Lines => _fakeFile;
}

abstract class PaymentMethod
{
    public string AccountLabel { get; }
    protected PaymentMethod(string accountLabel) => AccountLabel = accountLabel;
    public abstract bool Charge(decimal amount);
    public void PrintReceipt(decimal amount)
    {
        bool success = Charge(amount);
        Console.WriteLine(success
            ? $"Charged {amount:C} to {AccountLabel}"
            : $"FAILED to charge {amount:C} to {AccountLabel}");
    }
}
class CreditCard : PaymentMethod
{
    public CreditCard(string label) : base(label) { }
    public override bool Charge(decimal amount) => amount <= 5000; // pretend limit
}
class BankTransfer : PaymentMethod
{
    public BankTransfer(string label) : base(label) { }
    public override bool Charge(decimal amount) => true; // always succeeds in this sim
}

class SmartCreditCard : PaymentMethod, ILogger
{
    private readonly List<string> _log = new();
    public SmartCreditCard(string label) : base(label) { }
    public override bool Charge(decimal amount)
    {
        Log($"Attempting charge of {amount:C}");
        return amount <= 5000;
    }
    public void Log(string message) => _log.Add(message);
    public IReadOnlyList<string> Log_Entries => _log;
}

class Program
{
    static void Main()
    {
        ILogger console = new ConsoleLogger();
        console.Log("App started");

        PaymentMethod card = new CreditCard("Visa ...1234");
        card.PrintReceipt(200);

        var smart = new SmartCreditCard("Amex ...9999");
        smart.PrintReceipt(300);
        foreach (var line in smart.Log_Entries) Console.WriteLine(line);
    }
}
