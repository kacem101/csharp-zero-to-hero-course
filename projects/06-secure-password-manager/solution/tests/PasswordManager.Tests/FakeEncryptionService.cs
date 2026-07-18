using System.Security.Cryptography;
using PasswordManager.Domain;

/// <summary>
/// A deliberately simple, NON-cryptographically-secure stand-in for
/// IEncryptionService, used only so VaultService's orchestration logic
/// can be tested in isolation from real AES-GCM. Never use this outside
/// tests — it exists purely to keep the Tests project from needing a
/// reference to Infrastructure (Lesson 26's layering) while still being
/// able to prove the encrypt/decrypt round-trip and the
/// wrong-password-fails path work correctly.
/// </summary>
public class FakeEncryptionService : IEncryptionService
{
    public byte[] GenerateSalt() => new byte[] { 1, 2, 3, 4 };

    public byte[] DeriveKey(string masterPassword, byte[] salt)
        => System.Text.Encoding.UTF8.GetBytes(masterPassword.PadRight(32, 'x')[..32]);

    public EncryptedPayload Encrypt(byte[] plaintext, byte[] key, byte[] salt)
    {
        byte[] ciphertext = Xor(plaintext, key);
        byte[] tag = SHA256.HashData(key);
        byte[] emptyNonce = System.Array.Empty<byte>();
        return new EncryptedPayload(salt, emptyNonce, tag, ciphertext);
    }

    public byte[] Decrypt(EncryptedPayload payload, byte[] key)
    {
        byte[] expectedTag = SHA256.HashData(key);
        if (!expectedTag.AsSpan().SequenceEqual(payload.Tag))
            throw new VaultAuthenticationException("Incorrect master password (fake service).");
        return Xor(payload.Ciphertext, key);
    }

    private static byte[] Xor(byte[] data, byte[] key)
    {
        var result = new byte[data.Length];
        for (int i = 0; i < data.Length; i++)
            result[i] = (byte)(data[i] ^ key[i % key.Length]);
        return result;
    }
}
