namespace PasswordManager.Domain;

public record VaultEntry(string Site, string Username, string Password);

public record EncryptedPayload(byte[] Salt, byte[] Nonce, byte[] Tag, byte[] Ciphertext);
