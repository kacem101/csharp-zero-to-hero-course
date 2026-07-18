using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace PasswordManager.Domain;

public class VaultService
{
    private readonly IEncryptionService _encryption;
    private readonly IVaultRepository _repository;
    private readonly Dictionary<string, VaultEntry> _entries = new();
    private byte[]? _key;
    private byte[]? _salt;

    public VaultService(IEncryptionService encryption, IVaultRepository repository)
    {
        _encryption = encryption;
        _repository = repository;
    }

    public void InitNew(string masterPassword)
    {
        _salt = _encryption.GenerateSalt();
        _key = _encryption.DeriveKey(masterPassword, _salt);
        _entries.Clear();
    }

    public void Unlock(string masterPassword)
    {
        if (!_repository.Exists())
            throw new VaultCorruptedException("No vault exists to unlock — use init first.");

        EncryptedPayload payload = _repository.LoadRaw();
        byte[] key = _encryption.DeriveKey(masterPassword, payload.Salt);

        byte[] plaintext;
        try
        {
            plaintext = _encryption.Decrypt(payload, key);
        }
        catch (VaultAuthenticationException)
        {
            throw; // already the right exception type, from the encryption service
        }

        string json = Encoding.UTF8.GetString(plaintext);
        var loaded = JsonSerializer.Deserialize<Dictionary<string, VaultEntry>>(json)
            ?? throw new VaultCorruptedException("Vault contents did not deserialize correctly.");

        _entries.Clear();
        foreach (var kvp in loaded) _entries[kvp.Key] = kvp.Value;

        _key = key;
        _salt = payload.Salt;
    }

    public void AddEntry(string site, string username, string password)
    {
        EnsureUnlocked();
        _entries[site] = new VaultEntry(site, username, password);
    }

    public VaultEntry? GetEntry(string site)
    {
        EnsureUnlocked();
        return _entries.TryGetValue(site, out var entry) ? entry : null;
    }

    public IEnumerable<string> ListSites()
    {
        EnsureUnlocked();
        return _entries.Keys;
    }

    public void Save(string masterPassword)
    {
        EnsureUnlocked();
        string json = JsonSerializer.Serialize(_entries);
        byte[] plaintext = Encoding.UTF8.GetBytes(json);

        var payload = _encryption.Encrypt(plaintext, _key!, _salt!);
        _repository.SaveRaw(payload);
    }

    private void EnsureUnlocked()
    {
        if (_key is null || _salt is null)
            throw new InvalidOperationException("Vault is not unlocked — call InitNew or Unlock first.");
    }
}
