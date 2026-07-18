using PasswordManager.Domain;

/// <summary>
/// In-memory stand-in for IVaultRepository, so VaultService tests never
/// touch the real file system.
/// </summary>
public class FakeVaultRepository : IVaultRepository
{
    private EncryptedPayload? _stored;

    public bool Exists() => _stored is not null;
    public EncryptedPayload LoadRaw() => _stored!;
    public void SaveRaw(EncryptedPayload payload) => _stored = payload;
}
