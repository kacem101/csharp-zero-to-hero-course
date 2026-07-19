using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

class Program
{
    static async Task WriteLinesAsync(string path, IEnumerable<string> lines)
    {
        using var writer = new StreamWriter(path, append: false);
        foreach (var line in lines)
            await writer.WriteLineAsync(line);
    }

    static async Task<List<string>> ReadLinesAsync(string path)
    {
        var result = new List<string>();
        using var reader = new StreamReader(path);
        string? line;
        while ((line = await reader.ReadLineAsync()) != null)
            result.Add(line);
        return result;
    }

    static async Task<string?> FindFirstMatchAsync(string path, string keyword)
    {
        using var reader = new StreamReader(path);
        string? line;
        while ((line = await reader.ReadLineAsync()) != null)
            if (line.Contains(keyword)) return line; // stops as soon as it's found — never loads the whole file
        return null;
    }

    // TODO 3 fix: File.ReadAllLines is synchronous — it blocks the
    // calling thread for the full duration of disk I/O even though this
    // method is `async`. Use the async equivalent instead:
    static async Task<int> CountLinesAsync(string path)
    {
        var lines = await File.ReadAllLinesAsync(path);
        return lines.Length;
    }

    static async Task Main()
    {
        string tempPath = Path.GetTempFileName();
        await WriteLinesAsync(tempPath, new[] { "first", "second", "ERROR: disk full", "fourth" });

        var roundTripped = await ReadLinesAsync(tempPath);
        Console.WriteLine(string.Join(" | ", roundTripped));

        var match = await FindFirstMatchAsync(tempPath, "ERROR");
        Console.WriteLine(match ?? "not found");

        Console.WriteLine(await CountLinesAsync(tempPath));

        File.Delete(tempPath);
    }
}
