using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

public class LogParser
{
    private static readonly Regex LinePattern = new(
        @"^(?<ts>\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}) (?<event>\S+) user=(?<user>\S+) ip=(?<ip>\S+)$",
        RegexOptions.Compiled);

    public List<LogEntry> ParseFile(string path)
    {
        var entries = new List<LogEntry>();
        int lineNumber = 0;

        foreach (var line in File.ReadLines(path))
        {
            lineNumber++;
            if (string.IsNullOrWhiteSpace(line)) continue;

            var match = LinePattern.Match(line);
            if (!match.Success)
            {
                Console.Error.WriteLine($"Warning: skipping malformed line {lineNumber}");
                continue;
            }

            try
            {
                var entry = new LogEntry(
                    DateTime.Parse(match.Groups["ts"].Value),
                    match.Groups["event"].Value,
                    match.Groups["user"].Value,
                    match.Groups["ip"].Value);
                entries.Add(entry);
            }
            catch (FormatException)
            {
                Console.Error.WriteLine($"Warning: unparseable timestamp on line {lineNumber}");
            }
        }

        return entries;
    }
}
