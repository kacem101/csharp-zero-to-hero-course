using System;

public class VaultAuthenticationException : Exception
{
    public VaultAuthenticationException(string message) : base(message) { }
}

public class VaultCorruptedException : Exception
{
    public VaultCorruptedException(string message) : base(message) { }
}
