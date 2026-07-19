// TODO 1: Given a vulnerable method that builds SQL via string
// interpolation, rewrite it to use SqlCommand with a parameterized
// query. (You don't need a real database connection to demonstrate the
// API correctly — build the SqlCommand object and print its
// CommandText + parameter value to prove the query text never contains
// raw user input.)

// TODO 2: Implement HashPassword and VerifyPassword exactly as shown
// above. Prove that hashing the same password twice produces DIFFERENT
// hashes (because of the random salt), and that VerifyPassword still
// correctly returns true for the right password against either hash.

// TODO 3: Write a method `bool IsValidUsername(string input)` using an
// allowlist regex (letters, digits, underscore, 3-20 chars). Test it
// against a few valid and invalid inputs including one containing a
// SQL-injection-style payload.

// TODO 4 (bug hunt): find the vulnerability and fix it.
class Config
{
    public const string DbPassword = "SuperSecret123!"; // ???
}
