using PasswordManager.Domain;
using Xunit;

public class VaultServiceTests
{
    // TODO: [Fact] InitNew + AddEntry + Save + (new VaultService instance,
    // same fake repository) Unlock + GetEntry round-trips correctly —
    // proves encrypt/decrypt and the repository abstraction work
    // together without ever touching a real file.

    // TODO: [Fact] Unlock with the WRONG master password throws
    // VaultAuthenticationException.
}
