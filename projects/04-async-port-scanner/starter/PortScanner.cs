using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public record ScanResult(int Port, bool IsOpen, string? Banner);

public class PortScanner
{
    /// <summary>
    /// Scans ports [startPort, endPort] on `host`, bounded to
    /// `maxConcurrency` simultaneous connection attempts, honoring `ct`
    /// for cancellation. Returns a result for every port scanned before
    /// cancellation (if any).
    /// </summary>
    public Task<List<ScanResult>> ScanAsync(string host, int startPort, int endPort, int maxConcurrency, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Attempts to connect to a single port with a short timeout, and if
    /// successful, attempts a quick banner read with its own short
    /// timeout. Never throws for a closed/unreachable port — that's an
    /// expected, ordinary outcome, not an error.
    /// </summary>
    private Task<ScanResult> ScanPortAsync(string host, int port, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
