using System;

/// <summary>
/// Handles key derivation and AES-GCM encrypt/decrypt for the vault file
/// format. The stored file layout is up to you — document it here once
/// you decide (e.g. [salt][nonce][tag][ciphertext]).
/// </summary>
public static class VaultCrypto
{
    public static byte[] DeriveKey(string masterPassword, byte[] salt)
    {
        // TODO: use Rfc2898DeriveBytes.Pbkdf2 with a reasonable iteration
        // count and a 32-byte output (for AES-256).
        throw new NotImplementedException();
    }

    public static (byte[] ciphertext, byte[] nonce, byte[] tag) Encrypt(byte[] plaintext, byte[] key)
    {
        // TODO: use AesGcm to encrypt plaintext, generating a random
        // nonce, and returning the ciphertext + authentication tag.
        throw new NotImplementedException();
    }

    public static byte[] Decrypt(byte[] ciphertext, byte[] key, byte[] nonce, byte[] tag)
    {
        // TODO: use AesGcm to decrypt. If the tag doesn't verify (file
        // was tampered with, or the key/password was wrong), AesGcm
        // throws — catch that and translate it into a
        // VaultAuthenticationException or VaultCorruptedException as
        // appropriate.
        throw new NotImplementedException();
    }
}
