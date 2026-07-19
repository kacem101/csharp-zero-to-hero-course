# Correction Notes — Lesson 22a — Secure Coding Fundamentals in C#

## Answer

**Common mistakes to watch for:**
- Using `==` or `Equals` to compare secret hashes/tokens — always use `CryptographicOperations.FixedTimeEquals` for anything security-sensitive.
- Reaching for `MD5`/`SHA1`/plain `SHA256` for password storage — those are fast general-purpose hashes, exactly wrong for passwords; use a slow, salted, purpose-built KDF (PBKDF2, bcrypt, Argon2).
- Writing a denylist of "dangerous characters" instead of an allowlist of accepted input shapes.
- Committing any secret to source control "just for now" — there's no real undo once it's in git history; rotate the credential immediately if this happens.
