using System.Linq;
using PasswordManager.Domain;
using Xunit;

public class PasswordGeneratorTests
{
    [Fact]
    public void Generate_NoSymbols_ReturnsOnlyAlphanumericOfCorrectLength()
    {
        string password = PasswordGenerator.Generate(12, includeSymbols: false);

        Assert.Equal(12, password.Length);
        Assert.All(password, c => Assert.True(char.IsLetterOrDigit(c)));
    }

    [Fact]
    public void Generate_CalledTwice_ProducesDifferentPasswords()
    {
        string first = PasswordGenerator.Generate(16, includeSymbols: true);
        string second = PasswordGenerator.Generate(16, includeSymbols: true);

        Assert.NotEqual(first, second); // proves it's not a fixed/predictable value
    }
}
