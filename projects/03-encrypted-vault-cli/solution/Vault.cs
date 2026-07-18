using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

public class Vault
{
    private readonly Dictionary<string, string> _entries;

    private Vault(Dictionary<string, string> entries) => _entries = entries;

    public void Add(string key, string value) => _entries[key] = value;

    public string? Get(string key) => _entries.TryGetValue(key, out var value) ? value : null;

    public void Remove(string key) => _entries.Remove(key);

    public IEnumerable<string> ListKeys() => _entries.Keys;

    public static Vault LoadOrCreate(string path, string masterPassword)
    {
        if (!File.Exists(path))
            return new Vault(new Dictionary<string, string>());

        byte[] fileBytes = File.ReadAllBytes(path);
        if (fileBytes.Length < VaultCrypto.SaltSize + VaultCrypto.NonceSize + VaultCrypto.TagSize)
            throw new VaultCorruptedException("Vault file is too short to be valid.");

        int offset = 0;
        byte[] salt = fileBytes[offset..(offset += VaultCrypto.SaltSize)];
        byte[] nonce = fileBytes[offset..(offset += VaultCrypto.NonceSize)];
        byte[] tag = fileBytes[offset..(offset += VaultCrypto.TagSize)];
        byte[] ciphertext = fileBytes[offset..];

        byte[] key = VaultCrypto.DeriveKey(masterPassword, salt);
        byte[] plaintext = VaultCrypto.Decrypt(ciphertext, key, nonce, tag); // throws VaultAuthenticationException on failure

        string json = Encoding.UTF8.GetString(plaintext);
        var entries = JsonSerializer.Deserialize<Dictionary<string, string>>(json)
            ?? throw new VaultCorruptedException("Vault contents did not deserialize correctly.");

        return new Vault(entries);
    }

    public void Save(string path, string masterPassword)
    {
        byte[] salt = File.Exists(path)
            ? File.ReadAllBytes(path)[..VaultCrypto.SaltSize] // reuse the existing salt so the same password re-derives the same key
            : System.Security.Cryptography.RandomNumberGenerator.GetBytes(VaultCrypto.SaltSize);

        byte[] key = VaultCrypto.DeriveKey(masterPassword, salt);
        string json = JsonSerializer.Serialize(_entries);
        byte[] plaintext = Encoding.UTF8.GetBytes(json);

        var (ciphertext, nonce, tag) = VaultCrypto.Encrypt(plaintext, key);

        using var stream = new FileStream(path, FileMode.Create, FileAccess.Write);
        stream.Write(salt);
        stream.Write(nonce);
        stream.Write(tag);
        stream.Write(ciphertext);
    }
}
