# Project 3 — Encrypted Vault CLI

## Scenario
A small command-line "secrets vault": store key/value pairs (think API
keys, tokens) encrypted at rest in a single local file, protected by a
master password. This is a scaled-down version of what Project 6's
password manager will become.

## Requirements
- On first run (no vault file exists): prompt to set a master password, generate a random salt, and create an empty encrypted vault file.
- On subsequent runs: prompt for the master password and attempt to decrypt the vault.
  - Wrong password → a specific, custom exception (`VaultAuthenticationException`), caught in `Main` with a clean error message — never a raw stack trace shown to the user.
  - Corrupted/tampered file → a different custom exception (`VaultCorruptedException`) — this should be *detected*, not silently produce garbage data (this is exactly what authenticated encryption, not just plain AES-CBC, gives you).
- CLI commands once unlocked: `add <key> <value>`, `get <key>`, `list`, `remove <key>`, `save`, `exit`.
- Every `save` re-encrypts the full in-memory vault and overwrites the file.
- Use `AesGcm` (authenticated encryption — confidentiality AND tamper detection in one primitive) rather than plain `Aes`/CBC mode.
- Derive the encryption key from the master password with `Rfc2898DeriveBytes.Pbkdf2` and a random, stored salt — never use the raw password bytes as the key.

## Best-practices / secure-coding checklist
- [ ] The master password is never written to disk or logged anywhere — only its PBKDF2-derived key (and that key only lives in memory).
- [ ] A wrong master password produces a clear, specific error — not a generic crash, and not a silent wrong-decryption that returns garbage without telling you.
- [ ] File streams (`FileStream`) use `using`/proper `IDisposable` handling, including on the failure paths (Lesson 17).
- [ ] The nonce/IV used by `AesGcm` is randomly generated per-encryption and stored alongside the ciphertext — never reused across saves.
- [ ] No secret value is ever printed except when the user explicitly runs `get <key>` for that specific key.

## Stretch goals
- Add a `generate <key> <length>` command that generates and stores a strong random password using `RandomNumberGenerator`.
- Add an auto-lock: after N minutes of inactivity, require the master password again before any command runs.

## Lessons this draws on
Lesson 17 (`IDisposable`), Lesson 16 (custom exceptions), Lesson 22a (crypto/secure coding), Lesson 22 (file I/O).

## Self-check tests
A `tests/` folder ships alongside `starter/` and `solution/` with an
xUnit suite that checks your implementation against the requirements
above — not against the reference solution's exact internals. Run it
against your own work:
```bash
cd tests
dotnet test
```
It's pre-wired to `../starter/exercise.csproj`. Once you've replaced the
`NotImplementedException` stubs, red tests turn green. (To sanity-check
the reference solution instead, point the `ProjectReference` in
`verify.csproj` at `../solution/solution.csproj`.)
