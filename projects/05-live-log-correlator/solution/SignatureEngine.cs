using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public record Alert(string RuleName, string Line, DateTime DetectedAt);

public class SignatureEngine
{
    // Demo/teaching signatures only — nowhere near comprehensive enough
    // for production use. Real detection needs much broader coverage and
    // tuning to reduce false positives.
    private static readonly (string Name, Regex Pattern)[] Rules =
    {
        ("SQL Injection", new Regex(@"('|--|;|\bUNION\b|\bOR\b\s+'?\d+'?\s*=\s*'?\d+'?)", RegexOptions.Compiled | RegexOptions.IgnoreCase)),
        ("XSS", new Regex(@"<script|onerror\s*=|onload\s*=", RegexOptions.Compiled | RegexOptions.IgnoreCase)),
        ("Path Traversal", new Regex(@"\.\./", RegexOptions.Compiled)),
    };

    public event Action<Alert>? AlertRaised;

    public void Check(string line)
    {
        foreach (var (name, pattern) in Rules)
        {
            if (pattern.IsMatch(line))
                AlertRaised?.Invoke(new Alert(name, line, DateTime.Now));
        }
    }
}
