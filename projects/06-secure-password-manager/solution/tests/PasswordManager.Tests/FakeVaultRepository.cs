using PasswordManager.Domain;

public class FakeVaultRepository : IVaultRepository
{
    private EncryptedPayload? _stored;

    public bool Exists() => _stored is not null;
    public EncryptedPayload LoadRaw() => _stored!;
    public void SaveRaw(EncryptedPayload payload) => _stored = payload;
}
