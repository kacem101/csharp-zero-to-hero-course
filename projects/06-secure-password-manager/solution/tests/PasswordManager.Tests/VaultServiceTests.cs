using PasswordManager.Domain;
using Xunit;

public class VaultServiceTests
{
    [Fact]
    public void InitAddSaveUnlock_RoundTrips_EntryIsRecoveredCorrectly()
    {
        var repository = new FakeVaultRepository();
        var encryption = new FakeEncryptionService();

        var writer = new VaultService(encryption, repository);
        writer.InitNew("correct-horse-battery-staple");
        writer.AddEntry("github.com", "belkacem", "s3cr3t-p@ss");
        writer.Save("correct-horse-battery-staple");

        // A SECOND VaultService instance, same fake repository — proves
        // the round trip goes through actual serialization + encryption,
        // not just an in-memory reference still being alive.
        var reader = new VaultService(encryption, repository);
        reader.Unlock("correct-horse-battery-staple");

        var entry = reader.GetEntry("github.com");
        Assert.NotNull(entry);
        Assert.Equal("belkacem", entry!.Username);
        Assert.Equal("s3cr3t-p@ss", entry.Password);
    }

    [Fact]
    public void Unlock_WrongMasterPassword_ThrowsVaultAuthenticationException()
    {
        var repository = new FakeVaultRepository();
        var encryption = new FakeEncryptionService();

        var writer = new VaultService(encryption, repository);
        writer.InitNew("correct-password");
        writer.AddEntry("example.com", "user", "pw");
        writer.Save("correct-password");

        var reader = new VaultService(encryption, repository);
        Assert.Throws<VaultAuthenticationException>(() => reader.Unlock("wrong-password"));
    }
}
