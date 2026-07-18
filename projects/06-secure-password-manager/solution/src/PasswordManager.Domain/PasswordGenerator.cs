using System;
using System.Security.Cryptography;

namespace PasswordManager.Domain;

public static class PasswordGenerator
{
    private const string LowerChars = "abcdefghijklmnopqrstuvwxyz";
    private const string UpperChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string DigitChars = "0123456789";
    private const string SymbolChars = "!@#$%^&*()-_=+[]{}";

    public static string Generate(int length, bool includeSymbols)
    {
        if (length < 4) throw new ArgumentException("Password length must be at least 4.", nameof(length));

        string charPool = LowerChars + UpperChars + DigitChars + (includeSymbols ? SymbolChars : "");
        var result = new char[length];

        // RandomNumberGenerator, not System.Random — System.Random is a
        // predictable PRNG seeded from system time; anyone who can guess
        // or narrow down the seed can predict its output, which is
        // unacceptable for anything security-sensitive like a password.
        for (int i = 0; i < length; i++)
        {
            int index = RandomNumberGenerator.GetInt32(charPool.Length);
            result[i] = charPool[index];
        }

        return new string(result);
    }
}
