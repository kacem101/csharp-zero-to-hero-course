using System;
using System.Collections.Generic;
using System.IO;

record Product(int Id, string Name);

class Program
{
    static Product? SafeGetProduct(Dictionary<int, Product> catalog, int id)
    {
        try { return catalog[id]; }
        catch (KeyNotFoundException) { return null; } // the ONE exception indexing can realistically throw for a missing key
    }

    static int _attempt = 0;
    static string FlakyOperation()
    {
        _attempt++;
        if (_attempt <= 2) throw new InvalidOperationException("Temporary failure");
        return "success";
    }

    static string RetryFlakyOperation(int maxAttempts = 3)
    {
        for (int i = 1; i <= maxAttempts; i++)
        {
            try { return FlakyOperation(); }
            catch (InvalidOperationException) when (i < maxAttempts)
            {
                Console.WriteLine($"Attempt {i} failed, retrying...");
            }
        }
        throw new InvalidOperationException("All retries exhausted");
    }

    // TODO 3 fixes:
    // Bug 1: catching bare `Exception` hides which specific failure
    // happened (file missing? no permission? Process() itself buggy?).
    // Bug 2: `throw ex;` resets the stack trace, hiding where the error
    // actually originated when this gets logged/reported upstream.
    static void ProcessFile(string path)
    {
        try
        {
            var text = File.ReadAllText(path);
            Process(text);
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"File not found: {path}");
            throw; // preserves original stack trace
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine($"No permission to read: {path}");
            throw;
        }
    }

    static void Process(string text) => Console.WriteLine($"Processing {text.Length} chars");

    static void Main()
    {
        var catalog = new Dictionary<int, Product> { { 1, new Product(1, "Widget") } };
        Console.WriteLine(SafeGetProduct(catalog, 1)); // Product { ... }
        Console.WriteLine(SafeGetProduct(catalog, 99)); // null, no crash

        Console.WriteLine(RetryFlakyOperation()); // "Attempt 1 failed...", "Attempt 2 failed...", then "success"
    }
}
