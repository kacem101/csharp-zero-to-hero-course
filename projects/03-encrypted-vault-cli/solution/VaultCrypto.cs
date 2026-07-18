using System;
using System.Security.Cryptography;

/// <summary>
/// Key derivation and AES-256-GCM encrypt/decrypt for the vault.
/// File format on disk: [16-byte salt][12-byte nonce][16-byte tag][ciphertext]
/// AES-GCM gives authenticated encryption: a wrong key OR a tampered
/// file both fail tag verification, which we surface as distinct,
/// specific exceptions rather than silently returning garbage bytes.
/// </summary>
public static class VaultCrypto
{
    public const int SaltSize = 16;
    public const int NonceSize = 12;
    public const int TagSize = 16;
    private const int Pbkdf2Iterations = 100_000;

    public static byte[] DeriveKey(string masterPassword, byte[] salt)
        => Rfc2898DeriveBytes.Pbkdf2(masterPassword, salt, Pbkdf2Iterations, HashAlgorithmName.SHA256, 32);

    public static (byte[] ciphertext, byte[] nonce, byte[] tag) Encrypt(byte[] plaintext, byte[] key)
    {
        byte[] nonce = RandomNumberGenerator.GetBytes(NonceSize);
        byte[] tag = new byte[TagSize];
        byte[] ciphertext = new byte[plaintext.Length];

        using var aesGcm = new AesGcm(key, TagSize);
        aesGcm.Encrypt(nonce, plaintext, ciphertext, tag);

        return (ciphertext, nonce, tag);
    }

    public static byte[] Decrypt(byte[] ciphertext, byte[] key, byte[] nonce, byte[] tag)
    {
        byte[] plaintext = new byte[ciphertext.Length];
        using var aesGcm = new AesGcm(key, TagSize);
        try
        {
            aesGcm.Decrypt(nonce, ciphertext, tag, plaintext);
        }
        catch (CryptographicException)
        {
            // AesGcm throws when the authentication tag doesn't verify —
            // this happens for either a wrong key (wrong master password)
            // or a tampered/corrupted file. We can't distinguish the two
            // cases from AesGcm alone, so we surface it as an
            // authentication failure, which covers both correctly:
            // either way, the vault cannot be trusted to open as-is.
            throw new VaultAuthenticationException("Incorrect master password, or the vault file has been corrupted/tampered with.");
        }
        return plaintext;
    }
}
