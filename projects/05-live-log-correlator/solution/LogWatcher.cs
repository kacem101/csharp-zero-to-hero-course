using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

public class LogWatcher
{
    private readonly string _path;
    private long _lastPosition;

    public event Action<string>? NewLine;

    public LogWatcher(string path)
    {
        _path = path;
        // Start at the CURRENT end of file if it already exists, so we
        // only report genuinely new lines from this point forward.
        _lastPosition = File.Exists(path) ? new FileInfo(path).Length : 0;
    }

    public async Task WatchAsync(TimeSpan pollInterval, CancellationToken ct)
    {
        while (!ct.IsCancellationRequested)
        {
            try
            {
                if (File.Exists(_path))
                {
                    using var stream = new FileStream(_path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    if (stream.Length > _lastPosition)
                    {
                        stream.Seek(_lastPosition, SeekOrigin.Begin);
                        using var reader = new StreamReader(stream);
                        string? line;
                        while ((line = await reader.ReadLineAsync(ct)) != null)
                        {
                            NewLine?.Invoke(line);
                        }
                        _lastPosition = stream.Position;
                    }
                }
            }
            catch (IOException)
            {
                // File briefly locked by the writer — just try again next poll.
            }

            try { await Task.Delay(pollInterval, ct); }
            catch (OperationCanceledException) { break; }
        }
    }
}
