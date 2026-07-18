using System;
using System.Security.Cryptography;
using PasswordManager.Domain;

namespace PasswordManager.Infrastructure;

public class AesGcmEncryptionService : IEncryptionService
{
    private const int SaltSize = 16;
    private const int NonceSize = 12;
    private const int TagSize = 16;
    private const int Pbkdf2Iterations = 100_000;

    public byte[] GenerateSalt() => RandomNumberGenerator.GetBytes(SaltSize);

    public byte[] DeriveKey(string masterPassword, byte[] salt)
        => Rfc2898DeriveBytes.Pbkdf2(masterPassword, salt, Pbkdf2Iterations, HashAlgorithmName.SHA256, 32);

    public EncryptedPayload Encrypt(byte[] plaintext, byte[] key, byte[] salt)
    {
        byte[] nonce = RandomNumberGenerator.GetBytes(NonceSize);
        byte[] tag = new byte[TagSize];
        byte[] ciphertext = new byte[plaintext.Length];

        using var aesGcm = new AesGcm(key, TagSize);
        aesGcm.Encrypt(nonce, plaintext, ciphertext, tag);

        return new EncryptedPayload(salt, nonce, tag, ciphertext);
    }

    public byte[] Decrypt(EncryptedPayload payload, byte[] key)
    {
        byte[] plaintext = new byte[payload.Ciphertext.Length];
        using var aesGcm = new AesGcm(key, TagSize);
        try
        {
            aesGcm.Decrypt(payload.Nonce, payload.Ciphertext, payload.Tag, plaintext);
        }
        catch (CryptographicException)
        {
            // Wrong master password or tampered/corrupted file — either
            // way the tag failed to verify, so we don't trust this data.
            throw new VaultAuthenticationException("Incorrect master password, or the vault file has been corrupted/tampered with.");
        }
        return plaintext;
    }
}
