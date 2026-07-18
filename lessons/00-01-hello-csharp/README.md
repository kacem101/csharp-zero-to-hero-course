# Lesson 00.1 — Hello C#: Project Structure & the Console

## Why this matters
You already know C — you understand variables, functions, compiling, and running a program. This lesson is purely about translation: same ideas, different tools. Once the "where does my code live and how do I run it" question is answered, everything else builds on solid ground.

## The concept

### From `gcc` to `dotnet`
In C, you write a `.c` file, compile it with `gcc main.c -o main`, then run `./main` as a separate step. C# uses the **.NET SDK**, and `dotnet run` does both in one command — it compiles your code (to an intermediate language, then JIT-compiles to machine code at runtime) and immediately runs it.

```bash
dotnet new console -o MyApp   # scaffolds a new project (like starting a new .c file, but with extra project metadata)
cd MyApp
dotnet run                     # compiles AND runs
```

This creates two important files:
- **`MyApp.csproj`** — project metadata (target framework, settings). There's no real C equivalent; think of it loosely like a minimal Makefile that also declares "what version of the language/runtime am I targeting."
- **`Program.cs`** — your code. `.cs` is to C# what `.c` is to C.

### `Main` — same idea as C's `main`
Every C program starts execution in `int main(void)`. Every C# console app starts in a method called `Main`. Modern C# (since C# 9) lets you skip the boilerplate with **top-level statements** — the compiler generates the `Main` method for you behind the scenes.

```csharp
// Program.cs — top-level statements (modern, minimal style)
Console.WriteLine("Hello, world!");
```

is equivalent to the more explicit, C-like form:

```csharp
class Program
{
    static void Main(string[] args)   // args is like C's (int argc, char *argv[]) combined into one array
    {
        Console.WriteLine("Hello, world!");
    }
}
```

Both compile and run identically. This course uses the explicit `static void Main()` form in most lessons because it's clearer once classes enter the picture (Lesson 00.6) — but know that top-level statements exist for quick scripts.

### Console I/O — `Console.WriteLine` vs `printf`, `Console.ReadLine` vs `scanf`
```csharp
Console.WriteLine("Simple output, with a newline.");
Console.Write("No newline after this.");

Console.WriteLine($"Formatted: {2 + 2}"); // string interpolation — no format specifiers like %d needed

string? name = Console.ReadLine(); // reads one line as a string — no scanf format string, no buffer size to worry about
```
Notice: no `%d`, `%s`, `%f` format specifiers. C#'s `$"{expression}"` string interpolation embeds any expression directly and figures out how to display it — a huge quality-of-life difference from `printf`'s format-string matching.

### No manual memory management (yet)
In C, you're responsible for `malloc`/`free`. C# has **automatic garbage collection** — objects you create with `new` are cleaned up for you when nothing references them anymore. This is a big enough topic that it gets its own lesson later (Lesson 00.6 introduces `new`; Lesson 17 covers the deeper "some resources still need manual cleanup" nuance). For now: no `free()`, no dangling pointers from forgetting to free, no double-free bugs.

## Practice

```csharp
// TODO 1: Print "Hello, C#!" to the console.

// TODO 2: Ask the user for their name using Console.ReadLine(), then
// print "Welcome, {name}!" using string interpolation.

// TODO 3: Ask the user for their age as a string, convert it to an int
// (look up int.Parse), and print how old they'll be in 10 years.

// TODO 4: Print the command-line arguments passed to the program (use
// the `args` parameter in `static void Main(string[] args)` — this is
// C#'s equivalent of C's argv, minus argv[0] being the program name;
// C#'s args array does NOT include the program name).
```

## Answer

```csharp
using System;

class Program
{
    static void Main(string[] args)
    {
        // TODO 1
        Console.WriteLine("Hello, C#!");

        // TODO 2
        Console.Write("What's your name? ");
        string? name = Console.ReadLine();
        Console.WriteLine($"Welcome, {name}!");

        // TODO 3
        Console.Write("How old are you? ");
        string? ageInput = Console.ReadLine();
        int age = int.Parse(ageInput!); // like atoi() in C, but throws a clear exception on bad input instead of silently returning 0
        Console.WriteLine($"In 10 years you'll be {age + 10}.");

        // TODO 4
        Console.WriteLine($"You passed {args.Length} argument(s):");
        foreach (var arg in args)
            Console.WriteLine($"  {arg}");
    }
}
```

**Common mistakes to watch for:**
- Forgetting `Console.ReadLine()` returns `string?` (nullable) — if input is redirected from a closed stream it can be `null`, and `int.Parse(null)` throws. `int.TryParse` (covered in later lessons) is the safer alternative.
- Expecting `dotnet run` to work outside a project folder — you need a `.csproj` present (or run `dotnet run --project path/to/MyApp.csproj`).
- Trying to use C's format specifiers (`%d`, `%s`) inside a C# string — they simply don't do anything; use `$"{value}"` interpolation instead.
