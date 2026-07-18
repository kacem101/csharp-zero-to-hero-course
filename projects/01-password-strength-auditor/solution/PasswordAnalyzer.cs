using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

/// <summary>
/// Analyzes a single password and reports entropy, weakness patterns,
/// and an overall strength classification. Never persists or logs the
/// password itself — analysis only, in memory, for the current call.
/// </summary>
public class PasswordAnalyzer
{
    private static readonly HashSet<string> CommonPasswords = new(StringComparer.OrdinalIgnoreCase)
    {
        "password", "123456", "12345678", "qwerty", "letmein", "admin",
        "welcome", "monkey", "dragon", "111111", "iloveyou", "sunshine",
        "password1", "abc123", "football", "master", "shadow"
    };

    private static readonly HashSet<string> KeyboardWalks = new(StringComparer.OrdinalIgnoreCase)
    {
        "qwerty", "qwertyuiop", "asdfgh", "asdfghjkl", "zxcvbn", "zxcvbnm",
        "12345", "123456789", "09876", "0987654321"
    };

    // Common leetspeak substitutions, used to "de-leet" a password before
    // checking it against the common-password / dictionary-word lists.
    private static readonly Dictionary<char, char> LeetMap = new()
    {
        ['0'] = 'o', ['1'] = 'l', ['3'] = 'e', ['4'] = 'a',
        ['5'] = 's', ['7'] = 't', ['@'] = 'a', ['$'] = 's'
    };

    public double CalculateEntropyBits(string password)
    {
        if (string.IsNullOrEmpty(password)) return 0;

        int poolSize = 0;
        if (password.Any(char.IsLower)) poolSize += 26;
        if (password.Any(char.IsUpper)) poolSize += 26;
        if (password.Any(char.IsDigit)) poolSize += 10;
        if (password.Any(c => !char.IsLetterOrDigit(c))) poolSize += 32; // rough symbol-space estimate

        if (poolSize == 0) return 0;
        return password.Length * Math.Log2(poolSize);
    }

    public List<string> DetectPatterns(string password)
    {
        var issues = new List<string>();
        if (string.IsNullOrEmpty(password)) { issues.Add("Empty password"); return issues; }

        if (HasSequentialRun(password, 4))
            issues.Add("Contains a sequential run of characters (e.g. 'abcd' or '1234')");

        if (KeyboardWalks.Any(walk => password.Contains(walk, StringComparison.OrdinalIgnoreCase)))
            issues.Add("Contains a keyboard-walk pattern (e.g. 'qwerty', 'asdfgh')");

        if (Regex.IsMatch(password, @"(.)\1{2,}"))
            issues.Add("Contains 3+ repeated identical characters in a row");

        string deLeeted = DeLeet(password);
        if (CommonPasswords.Contains(deLeeted) || CommonPasswords.Contains(password))
            issues.Add("Matches (or de-leets to) a commonly used password");

        return issues;
    }

    public string ClassifyStrength(string password)
    {
        double entropy = CalculateEntropyBits(password);
        var patterns = DetectPatterns(password);

        string baseRating = entropy switch
        {
            < 28 => "Very Weak",
            < 36 => "Weak",
            < 60 => "Moderate",
            < 80 => "Strong",
            _ => "Very Strong"
        };

        if (patterns.Count == 0) return baseRating;

        // Any detected pattern caps the rating — a long password that's
        // still a leetspeak dictionary word is not actually strong.
        string[] order = { "Very Weak", "Weak", "Moderate", "Strong", "Very Strong" };
        int baseIndex = Array.IndexOf(order, baseRating);
        int cappedIndex = Math.Min(baseIndex, Array.IndexOf(order, "Weak"));
        return order[cappedIndex];
    }

    private static bool HasSequentialRun(string s, int runLength)
    {
        for (int i = 0; i <= s.Length - runLength; i++)
        {
            bool ascending = true, descending = true;
            for (int j = 1; j < runLength; j++)
            {
                if (s[i + j] != s[i + j - 1] + 1) ascending = false;
                if (s[i + j] != s[i + j - 1] - 1) descending = false;
            }
            if (ascending || descending) return true;
        }
        return false;
    }

    private static string DeLeet(string input)
    {
        var chars = input.Select(c => LeetMap.TryGetValue(c, out char replacement) ? replacement : c).ToArray();
        return new string(chars);
    }
}
