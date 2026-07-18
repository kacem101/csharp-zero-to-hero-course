using System;
using System.Collections.Generic;

namespace PasswordManager.Domain;

/// <summary>
/// Orchestrates the vault: unlocking, adding/reading entries, and
/// saving — without knowing HOW encryption or storage actually happen
/// (that's IEncryptionService / IVaultRepository's job, injected here).
/// </summary>
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

    /// <summary>Creates a brand-new, empty vault protected by masterPassword.</summary>
    public void InitNew(string masterPassword) => throw new NotImplementedException();

    /// <summary>
    /// Unlocks an existing vault. Throws VaultAuthenticationException on
    /// wrong password, VaultCorruptedException if the file is invalid.
    /// </summary>
    public void Unlock(string masterPassword) => throw new NotImplementedException();

    public void AddEntry(string site, string username, string password) => throw new NotImplementedException();

    public VaultEntry? GetEntry(string site) => throw new NotImplementedException();

    public IEnumerable<string> ListSites() => throw new NotImplementedException();

    public void Save(string masterPassword) => throw new NotImplementedException();
}
