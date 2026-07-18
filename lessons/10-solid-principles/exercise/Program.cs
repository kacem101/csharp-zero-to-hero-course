// TODO 1 (bug hunt, Single Responsibility): split this class into three
// classes, each with one reason to change.
class ReportGenerator
{
    public string BuildReport(List<int> data) { /* formatting logic */ return ""; }
    public void SaveToPdf(string report) { /* PDF library calls */ }
    public void EmailReport(string report, string address) { /* SMTP calls */ }
}

// TODO 2 (Dependency Inversion): rewrite this so NotificationService
// depends on an abstraction instead of a concrete EmailSender, and
// implement a fake/test-double sender to prove it's swappable.
class EmailSender { public void Send(string to, string msg) => Console.WriteLine($"Emailed {to}: {msg}"); }
class NotificationService
{
    private readonly EmailSender _sender = new();
    public void Notify(string user, string msg) => _sender.Send(user, msg);
}

// TODO 3 (Interface Segregation): this interface forces every printer to
// implement Scan and Fax even if it can't. Split it into smaller,
// focused interfaces, then implement a SimplePrinter that only prints.
interface IMachine { void Print(string doc); void Scan(string doc); void Fax(string doc); }
