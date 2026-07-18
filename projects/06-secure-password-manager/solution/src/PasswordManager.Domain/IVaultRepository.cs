namespace PasswordManager.Domain;

public interface IVaultRepository
{
    bool Exists();
    EncryptedPayload LoadRaw();
    void SaveRaw(EncryptedPayload payload);
}
