# Project 6 — Grand Capstone: Secure Password Manager

## Scenario
Everything the course covered, in one real application: a password
manager with a proper layered architecture (Domain / Infrastructure /
Console / Tests), wired together with dependency injection, storing an
AES-256-GCM-encrypted vault protected by a PBKDF2-derived master
password key, fully unit tested.

## Requirements

### Architecture (Lesson 26)
- **`PasswordManager.Domain`** (class library) — pure business logic, references nothing else in the solution:
  - `record VaultEntry(string Site, string Username, string Password)`
  - `record EncryptedPayload(byte[] Salt, byte[] Nonce, byte[] Tag, byte[] Ciphertext)`
  - `interface IEncryptionService` — `DeriveKey`, `GenerateSalt`, `Encrypt`, `Decrypt`
  - `interface IVaultRepository` — `LoadRaw()`, `SaveRaw(EncryptedPayload)`, `Exists()`
  - `static class PasswordGenerator` — generates strong random passwords using `RandomNumberGenerator`, **never** `System.Random` (that's not cryptographically secure — a real secure-coding distinction, not a style choice)
  - `VaultAuthenticationException`, `VaultCorruptedException`
  - `class VaultService` — the orchestrator: `Unlock(masterPassword)`, `AddEntry(...)`, `GetEntry(site)`, `ListSites()`, `Save(masterPassword)`. Takes `IEncryptionService` and `IVaultRepository` via constructor injection — it must not know or care whether encryption is AES-GCM or where the vault is actually stored.
- **`PasswordManager.Infrastructure`** (class library, references Domain) — the concrete implementations: `AesGcmEncryptionService : IEncryptionService`, `FileVaultRepository : IVaultRepository`.
- **`PasswordManager.Console`** (console app, references Domain + Infrastructure) — the composition root: wires everything with `Microsoft.Extensions.DependencyInjection`, runs the CLI loop (`unlock`/`init`, `add`, `get`, `list`, `generate`, `save`, `exit`).
- **`PasswordManager.Tests`** (xUnit, references Domain only) — tests `VaultService` against a **fake** `IVaultRepository` (in-memory, no real file I/O) and `PasswordGenerator` directly. This is only possible because `VaultService` depends on interfaces, not concrete classes.

### Functional
- `init`/first run: prompt for a new master password, generate a random salt, create an empty vault.
- `unlock`: derive the key from the master password + stored salt, decrypt, and load entries. Wrong password → `VaultAuthenticationException` with a clean message, no stack trace shown to the user.
- `add <site> <username> <password>` and `generate <site> <username> <length>` (auto-generates a strong password).
- `get <site>`, `list`, `save`, `exit`.

## Best-practices / secure-coding checklist
- [ ] `Domain` has zero references to `Infrastructure` or `Console` — verify with `dotnet list reference` or just by inspection of the `.csproj` files.
- [ ] `PasswordGenerator` uses `RandomNumberGenerator`, not `System.Random` (Lesson 22a).
- [ ] Master password is never logged, written to disk, or included in any exception message — only its derived key exists in memory, and only for the current session.
- [ ] `AesGcm`/`FileStream`/other disposables are properly `using`-scoped (Lesson 17).
- [ ] At least one xUnit test proves `VaultService` round-trips correctly using a **fake** repository — no real file touched during tests (Lesson 24).
- [ ] Service lifetimes in DI registration make sense (Lesson 25) — `VaultService` should be a Singleton within a single CLI session (one vault, one process).

## Lessons this draws on
Practically all of them — Lesson 01a (records), Module 2 (OOP/SOLID), Lesson 17 (`IDisposable`), Lesson 22a (crypto/secure coding), Lesson 24 (testing), Lesson 25 (DI), Lesson 26 (project structure).

## Running it
From inside `solution/` (or `starter/` once you've implemented it):
```bash
cd tests/PasswordManager.Tests && dotnet test && cd ../..
cd src/PasswordManager.Console && dotnet run
```
There's no `.sln` file provided — wire one up yourself as practice for Lesson 26:
```bash
dotnet new sln -n PasswordManager
dotnet sln add src/PasswordManager.Domain/PasswordManager.Domain.csproj
dotnet sln add src/PasswordManager.Infrastructure/PasswordManager.Infrastructure.csproj
dotnet sln add src/PasswordManager.Console/PasswordManager.Console.csproj
dotnet sln add tests/PasswordManager.Tests/PasswordManager.Tests.csproj
dotnet build
```
