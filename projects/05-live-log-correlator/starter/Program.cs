using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        string path = args.Length > 0 ? args[0] : "watched.log";

        // TODO: wire LogWatcher -> SignatureEngine -> alert printing +
        // alerts.log writing. Handle Ctrl+C to stop cleanly.
    }
}
