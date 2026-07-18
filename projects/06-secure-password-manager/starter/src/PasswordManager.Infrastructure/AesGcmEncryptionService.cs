using System;
using PasswordManager.Domain;

namespace PasswordManager.Infrastructure;

public class AesGcmEncryptionService : IEncryptionService
{
    public byte[] GenerateSalt() => throw new NotImplementedException();

    public byte[] DeriveKey(string masterPassword, byte[] salt) => throw new NotImplementedException();

    public EncryptedPayload Encrypt(byte[] plaintext, byte[] key, byte[] salt) => throw new NotImplementedException();

    public byte[] Decrypt(EncryptedPayload payload, byte[] key) => throw new NotImplementedException();
    // Remember: a failed tag check should surface as VaultAuthenticationException, not a raw CryptographicException.
}
