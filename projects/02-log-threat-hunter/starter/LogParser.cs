using System;
using System.Collections.Generic;

public class LogParser
{
    /// <summary>
    /// Reads every line of the file at `path`, parses valid lines into
    /// LogEntry records, and returns them. Malformed lines should be
    /// skipped with a warning printed to Console.Error — never let one
    /// bad line throw an unhandled exception that kills the whole run.
    /// </summary>
    public List<LogEntry> ParseFile(string path)
    {
        throw new NotImplementedException();
    }
}
