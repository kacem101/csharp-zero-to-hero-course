using System;
using System.IO;

class DatabaseConnectionSim : IDisposable
{
    private bool _isOpen = true;
    public DatabaseConnectionSim() => Console.WriteLine("Connection opened");
    public void Query(string sql) => Console.WriteLine($"Running: {sql}");
    public void Dispose()
    {
        if (_isOpen) { Console.WriteLine("Connection closed"); _isOpen = false; }
    }
}

class ResourcePool : IDisposable
{
    private readonly DatabaseConnectionSim _connA = new();
    private readonly DatabaseConnectionSim _connB = new();

    public void Dispose()
    {
        // Dispose both even if one throws — don't let a failure in _connA
        // prevent _connB from being cleaned up.
        try { _connA.Dispose(); }
        finally { _connB.Dispose(); }
    }
}

class Program
{
    // TODO 3 fix
    static void WriteLog(string path, string message)
    {
        using var writer = new StreamWriter(path, append: true); // Dispose() runs even if an exception is thrown below
        writer.WriteLine(message);
        if (message.Contains("ERROR")) throw new InvalidOperationException("error logged");
    }

    static void Main()
    {
        try
        {
            using var conn = new DatabaseConnectionSim();
            conn.Query("SELECT 1");
            throw new Exception("simulated failure mid-block");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Caught: {ex.Message}");
            // "Connection closed" was already printed BEFORE this line ran —
            // proving Dispose() fired during unwind, not after the catch.
        }

        using var pool = new ResourcePool();
        // both connections opened above; both will close when `pool` goes out of scope
    }
}
