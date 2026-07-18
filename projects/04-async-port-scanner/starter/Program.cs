using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Only scan systems you own or are explicitly authorized to test.");

        // TODO: parse args for host/port range/max concurrency (with
        // sensible defaults), wire Ctrl+C to a CancellationTokenSource,
        // run PortScanner.ScanAsync, print a results table even if
        // cancelled partway through.
    }
}
