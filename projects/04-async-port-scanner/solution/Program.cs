using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Only scan systems you own or are explicitly authorized to test.\n");

        string host = args.Length > 0 ? args[0] : "localhost";
        int startPort = 1, endPort = 1024, maxConcurrency = 50;

        if (args.Length > 1 && args[1].Contains('-'))
        {
            var range = args[1].Split('-');
            startPort = int.Parse(range[0]);
            endPort = int.Parse(range[1]);
        }
        if (args.Length > 2) maxConcurrency = int.Parse(args[2]);

        using var cts = new CancellationTokenSource();
        Console.CancelKeyPress += (_, e) =>
        {
            e.Cancel = true;
            cts.Cancel();
            Console.WriteLine("\nCancelling scan...");
        };

        Console.WriteLine($"Scanning {host} ports {startPort}-{endPort} (max {maxConcurrency} concurrent)...\n");

        var scanner = new PortScanner();
        var results = await scanner.ScanAsync(host, startPort, endPort, maxConcurrency, cts.Token);

        var openPorts = results.FindAll(r => r.IsOpen);
        Console.WriteLine($"\n--- {openPorts.Count} open port(s) out of {results.Count} scanned ---");
        foreach (var r in openPorts)
            Console.WriteLine($"  {r.Port}/tcp OPEN  {(r.Banner is null ? "" : $"banner: {r.Banner}")}");
    }
}
