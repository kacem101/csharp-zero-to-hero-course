using System;
using System.Collections.Generic;

public record Alert(string RuleName, string Line, DateTime DetectedAt);

public class SignatureEngine
{
    // TODO: build a small set of named, precompiled Regex rules for
    // SQL injection / XSS / path traversal patterns (see README).

    public event Action<Alert>? AlertRaised;

    /// <summary>
    /// Checks a single line against every rule; raises AlertRaised for
    /// each rule that matches (a line could match more than one rule).
    /// </summary>
    public void Check(string line)
    {
        throw new NotImplementedException();
    }
}
