using System;
using System.Text;
using Xunit;

public class VaultCryptoTests
{
    [Fact]
    public void EncryptThenDecrypt_SameKey_ReturnsOriginalPlaintext()
    {
        byte[] plaintext = Encoding.UTF8.GetBytes("a secret value");
        byte[] salt = new byte[16];
        byte[] key = VaultCrypto.DeriveKey("correct-password", salt);

        var (ciphertext, nonce, tag) = VaultCrypto.Encrypt(plaintext, key);
        byte[] decrypted = VaultCrypto.Decrypt(ciphertext, key, nonce, tag);

        Assert.Equal(plaintext, decrypted);
    }

    [Fact]
    public void Decrypt_WrongKey_ThrowsVaultAuthenticationException()
    {
        byte[] plaintext = Encoding.UTF8.GetBytes("a secret value");
        byte[] salt = new byte[16];
        byte[] rightKey = VaultCrypto.DeriveKey("correct-password", salt);
        byte[] wrongKey = VaultCrypto.DeriveKey("wrong-password", salt);

        var (ciphertext, nonce, tag) = VaultCrypto.Encrypt(plaintext, rightKey);

        Assert.Throws<VaultAuthenticationException>(() => VaultCrypto.Decrypt(ciphertext, wrongKey, nonce, tag));
    }

    [Fact]
    public void DeriveKey_SamePasswordAndSalt_ProducesSameKey()
    {
        byte[] salt = new byte[16];
        byte[] key1 = VaultCrypto.DeriveKey("my-password", salt);
        byte[] key2 = VaultCrypto.DeriveKey("my-password", salt);

        Assert.Equal(key1, key2);
    }
}

public class VaultTests
{
    [Fact]
    public void SaveThenLoad_SamePassword_RecoversAllEntries()
    {
        string tempPath = System.IO.Path.GetTempFileName();
        System.IO.File.Delete(tempPath); // LoadOrCreate should treat a missing file as "new vault"

        var vault = Vault.LoadOrCreate(tempPath, "master-pw");
        vault.Add("github", "myapikey123");
        vault.Save(tempPath, "master-pw");

        var reloaded = Vault.LoadOrCreate(tempPath, "master-pw");
        Assert.Equal("myapikey123", reloaded.Get("github"));

        System.IO.File.Delete(tempPath);
    }

    [Fact]
    public void LoadOrCreate_WrongPassword_ThrowsVaultAuthenticationException()
    {
        string tempPath = System.IO.Path.GetTempFileName();
        System.IO.File.Delete(tempPath);

        var vault = Vault.LoadOrCreate(tempPath, "correct-pw");
        vault.Add("site", "secret");
        vault.Save(tempPath, "correct-pw");

        Assert.Throws<VaultAuthenticationException>(() => Vault.LoadOrCreate(tempPath, "wrong-pw"));

        System.IO.File.Delete(tempPath);
    }
}
