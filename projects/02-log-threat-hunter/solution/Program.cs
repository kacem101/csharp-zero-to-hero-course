using System;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        string path = args.Length > 0 ? args[0] : "sample-data/auth.log";

        var parser = new LogParser();
        var entries = parser.ParseFile(path);
        Console.WriteLine($"Parsed {entries.Count} log entries from {path}\n");

        var detector = new ThreatDetector();

        Console.WriteLine("--- Top offending IPs (failed logins) ---");
        var topOffenders = entries
            .Where(e => e.EventType == "LOGIN_FAILED")
            .GroupBy(e => e.Ip)
            .Select(g => new { Ip = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .Take(5);
        foreach (var o in topOffenders) Console.WriteLine($"  {o.Ip}: {o.Count} failed attempts");

        Console.WriteLine("\n--- Brute-force alerts ---");
        var bruteForce = detector.DetectBruteForce(entries);
        if (bruteForce.Count == 0) Console.WriteLine("  none detected");
        foreach (var alert in bruteForce)
            Console.WriteLine($"  {alert.Ip}: {alert.FailedAttempts} failed logins starting {alert.WindowStart:HH:mm:ss}");

        Console.WriteLine("\n--- Impossible travel alerts ---");
        var travel = detector.DetectImpossibleTravel(entries);
        if (travel.Count == 0) Console.WriteLine("  none detected");
        foreach (var alert in travel)
            Console.WriteLine($"  {alert.User}: {alert.FirstIp} -> {alert.SecondIp} in {alert.Gap.TotalSeconds:F0}s");
    }
}
