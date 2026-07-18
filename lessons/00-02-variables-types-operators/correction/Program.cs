using System;

class Program
{
    static void Main()
    {
        // TODO 1
        var age = 21;
        var gpa = 3.8;
        var isEnrolled = true;
        var university = "NSCS";
        Console.WriteLine($"{university}: age {age}, GPA {gpa}, enrolled: {isEnrolled}");

        // TODO 2
        Console.Write("Enter Fahrenheit: ");
        double f = double.Parse(Console.ReadLine()!);
        double c = (f - 32) * 5.0 / 9.0;
        Console.WriteLine($"{f}F = {c:F2}C");

        // TODO 3
        string original = "hello";
        original.ToUpper(); // result discarded — original is untouched
        Console.WriteLine(original); // still "hello"

        string upper = original.ToUpper(); // NOW we capture the new string
        Console.WriteLine(upper); // "HELLO"

        // TODO 4 fix: `a + b` where `a` is a string and `b` is an int
        // doesn't add numerically — in C# it triggers implicit
        // ToString() on `b` and CONCATENATES, giving "53" (a string), not
        // 8. To do numeric addition you must parse `a` first:
        string aStr = "5";
        int bNum = 3;
        int correctResult = int.Parse(aStr) + bNum; // 8
        Console.WriteLine(correctResult);
    }
}
