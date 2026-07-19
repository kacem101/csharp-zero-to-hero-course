using System;
using System.Data;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

class Program
{
    // TODO 1 — parameterized query construction (illustrative; a real
    // app would execute this against an open SqlConnection)
    static void BuildSafeQuery(string username)
    {
        // In real code: using var command = new SqlCommand(
        //     "SELECT * FROM Users WHERE Username = @username", connection);
        string commandText = "SELECT * FROM Users WHERE Username = @username";
        string parameterValue = username; // stored as DATA, never concatenated into commandText
        Console.WriteLine($"SQL: {commandText}");
        Console.WriteLine($"@username = {parameterValue}");
        // Even if username is: ' OR '1'='1  — it's just a string value
        // bound to the parameter, never parsed as SQL syntax.
    }

    // TODO 2
    static (byte[] hash, byte[] salt) HashPassword(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(16);
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, 100_000, HashAlgorithmName.SHA256, 32);
        return (hash, salt);
    }

    static bool VerifyPassword(string password, byte[] expectedHash, byte[] salt)
    {
        byte[] actualHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, 100_000, HashAlgorithmName.SHA256, 32);
        return CryptographicOperations.FixedTimeEquals(actualHash, expectedHash);
    }

    // TODO 3
    static bool IsValidUsername(string input) => Regex.IsMatch(input, @"^[a-zA-Z0-9_]{3,20}$");

    static void Main()
    {
        BuildSafeQuery("' OR '1'='1"); // proves the payload is inert as a parameter value

        var (hash1, salt1) = HashPassword("correct horse battery staple");
        var (hash2, salt2) = HashPassword("correct horse battery staple");
        Console.WriteLine(Convert.ToBase64String(hash1) == Convert.ToBase64String(hash2)); // False — different random salts
        Console.WriteLine(VerifyPassword("correct horse battery staple", hash1, salt1));   // True
        Console.WriteLine(VerifyPassword("wrong guess", hash1, salt1));                    // False

        Console.WriteLine(IsValidUsername("belkacem_01"));      // True
        Console.WriteLine(IsValidUsername("' OR '1'='1"));       // False — rejected by the allowlist, not a denylist scan
    }
}

// TODO 4 fix: a hardcoded secret in source code is committed to version
// control permanently (even deleting it later leaves it in git history)
// and is visible to anyone with repo access. Load it from the
// environment (or a real secrets manager in production) instead:
class Config
{
    public static string DbPassword =>
        Environment.GetEnvironmentVariable("DB_PASSWORD")
        ?? throw new InvalidOperationException("DB_PASSWORD environment variable is not set.");
}
