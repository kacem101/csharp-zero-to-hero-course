// TODO 1: Implement a `class DatabaseConnectionSim : IDisposable` that
// simulates holding an "open connection" (just a bool _isOpen flag and a
// Console.WriteLine on open/close). Use it with a `using` block and
// prove Dispose() runs even when an exception is thrown inside the block
// (wrap the block's body in a try/catch outside, or let it propagate and
// observe the Console output order).

// TODO 2: Implement a class `ResourcePool` that manages TWO disposable
// resources internally (e.g. two DatabaseConnectionSim instances) and
// is itself IDisposable — its Dispose() must dispose both inner
// resources, even if disposing the first one throws (hint: don't let
// one failed Dispose prevent the other's cleanup).

// TODO 3 (bug hunt): find the resource leak and fix it using `using`.
void WriteLog(string path, string message)
{
    var writer = new StreamWriter(path, append: true);
    writer.WriteLine(message);
    if (message.Contains("ERROR")) throw new InvalidOperationException("error logged");
    writer.Close();
}
