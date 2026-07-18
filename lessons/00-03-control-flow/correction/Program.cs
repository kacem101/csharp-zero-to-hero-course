using System;

class Program
{
    static void Main()
    {
        // TODO 1
        for (int i = 1; i <= 30; i++)
        {
            if (i % 15 == 0) Console.WriteLine("FizzBuzz");
            else if (i % 3 == 0) Console.WriteLine("Fizz");
            else if (i % 5 == 0) Console.WriteLine("Buzz");
            else Console.WriteLine(i);
        }

        // TODO 2
        int[] scores = { 55, 72, 88, 91, 40, 67 };
        foreach (int score in scores) Console.WriteLine(score);

        for (int i = 0; i < scores.Length; i++)
            Console.WriteLine($"Score at index {i}: {scores[i]}");

        // TODO 3
        string SizeLabel(int liters) => liters switch
        {
            < 1 => "small",
            <= 2 => "medium",
            _ => "large"
        };
        Console.WriteLine(SizeLabel(0));
        Console.WriteLine(SizeLabel(2));
        Console.WriteLine(SizeLabel(5));

        // TODO 4
        int[] nums = { 1,2,3,4,5,6,7,8,9,10 };
        int[] lastThree = nums[^3..];
        Console.WriteLine(string.Join(", ", lastThree)); // 8, 9, 10
    }
}
