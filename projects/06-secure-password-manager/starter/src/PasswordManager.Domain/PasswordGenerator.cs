using System;

namespace PasswordManager.Domain;

public static class PasswordGenerator
{
    /// <summary>
    /// Generates a cryptographically secure random password of the
    /// given length. MUST use RandomNumberGenerator, not System.Random —
    /// System.Random is predictable and unsuitable for anything
    /// security-sensitive.
    /// </summary>
    public static string Generate(int length, bool includeSymbols)
    {
        throw new NotImplementedException();
    }
}
