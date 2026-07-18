# Correction Notes — Lesson 00.1: Hello C#

**Common mistakes to watch for:**
- Forgetting `Console.ReadLine()` returns `string?` (nullable) — if input is redirected from a closed stream it can be `null`, and `int.Parse(null)` throws. `int.TryParse` (covered in later lessons) is the safer alternative.
- Expecting `dotnet run` to work outside a project folder — you need a `.csproj` present.
- Trying to use C's format specifiers (`%d`, `%s`) inside a C# string — use `$"{value}"` interpolation instead.
