using System;

class User
{
    public string Name = "";
    public string? Bio;
}

class Program
{
    static int? ParseIntOrNull(string input)
        => int.TryParse(input, out int result) ? result : null;

    static string Describe(int? value) => value is int v ? $"value: {v}" : "no value";

    static int BioLength(User u) => u.Bio?.Length ?? 0;

    static string? GetNickname(User u) => null;

    // Fixed: don't use `!` to force-unwrap something that's genuinely
    // allowed to be null. Handle the null case explicitly.
    static void PrintNickname(User u)
    {
        string? nickname = GetNickname(u);
        Console.WriteLine(nickname is null ? "(no nickname)" : nickname.ToUpper());
    }

    static void Main()
    {
        Console.WriteLine(Describe(ParseIntOrNull("42")));  // value: 42
        Console.WriteLine(Describe(ParseIntOrNull("abc"))); // no value

        var u = new User { Name = "Belkacem" };
        Console.WriteLine(BioLength(u)); // 0
        PrintNickname(u); // (no nickname), not a crash
    }
}
