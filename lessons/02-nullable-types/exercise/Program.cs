// TODO 1: Write a method `int? ParseIntOrNull(string input)` that returns
// the parsed int, or null if parsing fails. Use int.TryParse — no
// exceptions.

// TODO 2: Write a method `string Describe(int? value)` that returns
// "no value" if null, otherwise "value: {value}". Use pattern matching
// or ?? — not a manual if/else with .HasValue.

// TODO 3: Model a `class User { public string Name; public string? Bio; }`
// Write `int BioLength(User u)` using the null-conditional operator to
// safely get Bio's length, defaulting to 0 if Bio is null.

// TODO 4 (bug hunt): find and fix the bug.
string? GetNickname(User u) => null; // pretend this can legitimately return null
void PrintNickname(User u)
{
    string nickname = GetNickname(u)!; // is this safe? why or why not?
    Console.WriteLine(nickname.ToUpper());
}
