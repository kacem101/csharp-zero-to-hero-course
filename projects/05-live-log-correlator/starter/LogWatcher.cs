using System;
using System.Threading;
using System.Threading.Tasks;

public class LogWatcher
{
    private readonly string _path;
    private long _lastPosition;

    public event Action<string>? NewLine;

    public LogWatcher(string path) => _path = path;

    /// <summary>
    /// Polls the file every `pollInterval` for new content appended
    /// since the last check, firing NewLine once per new line found.
    /// Runs until `ct` is cancelled.
    /// </summary>
    public Task WatchAsync(TimeSpan pollInterval, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
