using System;
using System.IO;
using PasswordManager.Domain;

namespace PasswordManager.Infrastructure;

/// <summary>
/// File layout: [16-byte salt][12-byte nonce][16-byte tag][ciphertext]
/// </summary>
public class FileVaultRepository : IVaultRepository
{
    private const int SaltSize = 16;
    private const int NonceSize = 12;
    private const int TagSize = 16;

    private readonly string _path;

    public FileVaultRepository(string path) => _path = path;

    public bool Exists() => File.Exists(_path);

    public EncryptedPayload LoadRaw()
    {
        byte[] fileBytes = File.ReadAllBytes(_path);
        if (fileBytes.Length < SaltSize + NonceSize + TagSize)
            throw new VaultCorruptedException("Vault file is too short to be valid.");

        int offset = 0;
        byte[] salt = fileBytes[offset..(offset += SaltSize)];
        byte[] nonce = fileBytes[offset..(offset += NonceSize)];
        byte[] tag = fileBytes[offset..(offset += TagSize)];
        byte[] ciphertext = fileBytes[offset..];

        return new EncryptedPayload(salt, nonce, tag, ciphertext);
    }

    public void SaveRaw(EncryptedPayload payload)
    {
        using var stream = new FileStream(_path, FileMode.Create, FileAccess.Write);
        stream.Write(payload.Salt);
        stream.Write(payload.Nonce);
        stream.Write(payload.Tag);
        stream.Write(payload.Ciphertext);
    }
}
