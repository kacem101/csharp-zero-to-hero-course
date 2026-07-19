# Lesson 22a — Secure Coding Fundamentals in C#

## Why this matters
As a security student, you already know these vulnerability classes from the attacker's side (SQL injection, weak hashing, hardcoded secrets, timing attacks). This lesson is the defender's side: the specific C#/.NET APIs that prevent them, and the exact mistakes that reintroduce them even when a developer "knows better" in principle.

## The concept

### SQL injection — never build queries with string concatenation
```csharp
// BAD — the classic vulnerability
string query = $"SELECT * FROM Users WHERE Username = '{username}'";
// If username is:  ' OR '1'='1
// the query becomes: SELECT * FROM Users WHERE Username = '' OR '1'='1'  — returns everything
```
```csharp
// GOOD — parameterized query; the database treats the parameter as DATA, never as SQL syntax
using var command = new SqlCommand("SELECT * FROM Users WHERE Username = @username", connection);
command.Parameters.AddWithValue("@username", username);
```
This isn't a style preference — parameterization makes the injection **structurally impossible**, because the parameter value is never parsed as part of the SQL grammar at all. ORMs like Entity Framework Core parameterize automatically when you use LINQ (`db.Users.Where(u => u.Username == username)`) — the danger reappears the moment someone drops to raw SQL string interpolation "just this once."

### Password hashing — never store plaintext, never use a fast general-purpose hash
```csharp
using System.Security.Cryptography;

// GOOD — PBKDF2 with a random salt, deliberately slow (iteration count) to resist brute-forcing
static (byte[] hash, byte[] salt) HashPassword(string password)
{
    byte[] salt = RandomNumberGenerator.GetBytes(16); // unique per password — prevents rainbow-table attacks
    byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations: 100_000, HashAlgorithmName.SHA256, outputLength: 32);
    return (hash, salt);
}

static bool VerifyPassword(string password, byte[] expectedHash, byte[] salt)
{
    byte[] actualHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations: 100_000, HashAlgorithmName.SHA256, outputLength: 32);
    return CryptographicOperations.FixedTimeEquals(actualHash, expectedHash); // see below — never use `==` here
}
```
`MD5`/`SHA1`/plain `SHA256` on their own are **fast** hashes — exactly the wrong property for passwords, because fast means an attacker with a stolen hash database can brute-force billions of guesses per second on commodity GPUs. `Rfc2898DeriveBytes.Pbkdf2` (or, in a real app, `bcrypt`/`Argon2` via a library) is deliberately slow and salted.

### Timing attacks — comparing secrets safely
```csharp
// BAD — `==` on byte arrays / strings short-circuits on the FIRST mismatched byte,
// so comparison time leaks information about how many leading bytes were correct
if (actualHash == expectedHash) { ... } // wrong for byte[] anyway (reference equality!), and unsafe even if fixed

// GOOD — always takes the same amount of time regardless of where the mismatch is
if (CryptographicOperations.FixedTimeEquals(actualHash, expectedHash)) { ... }
```
This directly defends against the class of attack where measuring response-time differences lets an attacker recover a secret one byte at a time.

### Secrets management — never hardcode connection strings or API keys
```csharp
// BAD — committed straight into source control, visible to anyone with repo access, forever (even after later removal, it's in git history)
const string ApiKey = "sk_live_51H8x...";
```
```csharp
// GOOD — read from configuration, which in turn reads from environment
// variables, a secrets manager, or (for local dev only) the .NET
// "user-secrets" tool — never from a file that gets committed
string? apiKey = Environment.GetEnvironmentVariable("MY_API_KEY");
if (string.IsNullOrEmpty(apiKey))
    throw new InvalidOperationException("MY_API_KEY is not configured.");
```
(In a real ASP.NET Core app you'd use `IConfiguration` bound from `appsettings.json` + environment-specific overrides + a real secrets manager in production — Lesson 25 covers configuration in depth.)

### Input validation — allowlist, don't denylist
Trying to enumerate every "bad" character or pattern (a denylist) is a losing game — attackers find the case you missed. Validate against what's **allowed** instead:
```csharp
// BAD — trying to block "bad" input
if (username.Contains("'") || username.Contains(";")) throw new ArgumentException("Invalid username");

// GOOD — only accept what's explicitly allowed
if (!System.Text.RegularExpressions.Regex.IsMatch(username, @"^[a-zA-Z0-9_]{3,20}$"))
    throw new ArgumentException("Username must be 3-20 alphanumeric characters or underscores.");
```
Also: never rely on client-side validation alone (JavaScript checks, UI constraints) — always re-validate on the server, since a client is fully under the attacker's control.
