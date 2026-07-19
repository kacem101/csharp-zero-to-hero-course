using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, C#!");

        Console.Write("What's your name? ");
        string? name = Console.ReadLine();
        Console.WriteLine($"Welcome, {name}!");

        Console.Write("How old are you? ");
        string? ageInput = Console.ReadLine();
        int age = int.Parse(ageInput!); // like atoi() in C, but throws a clear exception on bad input
        Console.WriteLine($"In 10 years you'll be {age + 10}.");

        Console.WriteLine($"You passed {args.Length} argument(s):");
        foreach (var arg in args)
            Console.WriteLine($"  {arg}");
    }
}
