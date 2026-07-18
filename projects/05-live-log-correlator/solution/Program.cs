using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        string path = args.Length > 0 ? args[0] : "watched.log";
        const string alertLogPath = "alerts.log";

        if (!File.Exists(path)) File.WriteAllText(path, "");

        var watcher = new LogWatcher(path);
        var engine = new SignatureEngine();

        watcher.NewLine += engine.Check;

        engine.AlertRaised += alert =>
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ALERT] {alert.DetectedAt:HH:mm:ss} {alert.RuleName}: {alert.Line}");
            Console.ResetColor();

            try
            {
                File.AppendAllText(alertLogPath, $"{alert.DetectedAt:O}\t{alert.RuleName}\t{alert.Line}{Environment.NewLine}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"(warning: could not write to alert log: {ex.Message})");
            }
        };

        using var cts = new CancellationTokenSource();
        Console.CancelKeyPress += (_, e) =>
        {
            e.Cancel = true;
            cts.Cancel();
            Console.WriteLine("\nStopping watcher...");
        };

        Console.WriteLine($"Watching {path} for new lines (Ctrl+C to stop)...");
        Console.WriteLine("Demo signatures only — SQLi / XSS / path traversal patterns, not production-grade.\n");

        await watcher.WatchAsync(TimeSpan.FromSeconds(1), cts.Token);
    }
}
