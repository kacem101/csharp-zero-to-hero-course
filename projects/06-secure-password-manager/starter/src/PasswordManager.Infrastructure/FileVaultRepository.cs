using System;
using PasswordManager.Domain;

namespace PasswordManager.Infrastructure;

public class FileVaultRepository : IVaultRepository
{
    private readonly string _path;

    public FileVaultRepository(string path) => _path = path;

    public bool Exists() => throw new NotImplementedException();

    public EncryptedPayload LoadRaw() => throw new NotImplementedException();
    // File layout suggestion: [salt][nonce][tag][ciphertext] with fixed-size prefixes.

    public void SaveRaw(EncryptedPayload payload) => throw new NotImplementedException();
}
