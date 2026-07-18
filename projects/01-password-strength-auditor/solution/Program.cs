using System;

class Program
{
    static void Main()
    {
        var analyzer = new PasswordAnalyzer();
        Console.WriteLine("Password Strength Auditor — type 'exit' to quit.");
        Console.WriteLine("(Nothing you type here is logged or saved anywhere.)\n");

        while (true)
        {
            Console.Write("Password to analyze: ");
            string? input = Console.ReadLine();

            if (input is null || input.Equals("exit", StringComparison.OrdinalIgnoreCase))
                break;

            double entropy = analyzer.CalculateEntropyBits(input);
            var issues = analyzer.DetectPatterns(input);
            string rating = analyzer.ClassifyStrength(input);

            Console.WriteLine($"  Entropy: {entropy:F1} bits");
            Console.WriteLine($"  Rating: {rating}");
            if (issues.Count > 0)
            {
                Console.WriteLine("  Issues found:");
                foreach (var issue in issues) Console.WriteLine($"    - {issue}");
            }
            else
            {
                Console.WriteLine("  No specific weak patterns detected.");
            }
            Console.WriteLine();
        }
    }
}
