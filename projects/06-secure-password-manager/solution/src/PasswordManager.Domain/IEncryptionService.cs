namespace PasswordManager.Domain;

public interface IEncryptionService
{
    byte[] GenerateSalt();
    byte[] DeriveKey(string masterPassword, byte[] salt);
    EncryptedPayload Encrypt(byte[] plaintext, byte[] key, byte[] salt);
    byte[] Decrypt(EncryptedPayload payload, byte[] key);
}
