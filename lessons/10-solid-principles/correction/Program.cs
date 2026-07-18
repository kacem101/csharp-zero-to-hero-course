using System;
using System.Collections.Generic;

// TODO 1
class ReportBuilder { public string Build(List<int> data) => string.Join(",", data); }
class PdfExporter { public void SaveToPdf(string report) => Console.WriteLine($"Saved PDF: {report}"); }
class ReportEmailer { public void Email(string report, string address) => Console.WriteLine($"Emailed to {address}: {report}"); }

// TODO 2
interface IMessageSender { void Send(string to, string msg); }
class EmailSender : IMessageSender { public void Send(string to, string msg) => Console.WriteLine($"Emailed {to}: {msg}"); }
class FakeSender : IMessageSender
{
    public List<string> Sent = new();
    public void Send(string to, string msg) => Sent.Add($"{to}: {msg}"); // no real I/O — great for tests
}
class NotificationService
{
    private readonly IMessageSender _sender;
    public NotificationService(IMessageSender sender) => _sender = sender; // depends on abstraction
    public void Notify(string user, string msg) => _sender.Send(user, msg);
}

// TODO 3
interface IPrinter { void Print(string doc); }
interface IScanner { void Scan(string doc); }
interface IFax { void Fax(string doc); }
class SimplePrinter : IPrinter { public void Print(string doc) => Console.WriteLine($"Printing: {doc}"); }
class AllInOnePrinter : IPrinter, IScanner, IFax
{
    public void Print(string doc) => Console.WriteLine($"Printing: {doc}");
    public void Scan(string doc) => Console.WriteLine($"Scanning: {doc}");
    public void Fax(string doc) => Console.WriteLine($"Faxing: {doc}");
}

class Program
{
    static void Main()
    {
        var real = new NotificationService(new EmailSender());
        real.Notify("belkacem@example.com", "Hello");

        var fake = new FakeSender();
        var testable = new NotificationService(fake);
        testable.Notify("test@example.com", "Test message");
        Console.WriteLine(fake.Sent[0]); // verifiable without any real network call

        IPrinter simple = new SimplePrinter();
        simple.Print("resume.pdf"); // no forced Scan()/Fax() it can't support
    }
}
