using System;
using System.Text;

class Program
{
    static int Sum(int[] arr)
    {
        int total = 0;
        for (int i = 0; i < arr.Length; i++) total += arr[i];
        return total;
    }

    static string Reverse(string input)
    {
        char[] chars = input.ToCharArray();
        Array.Reverse(chars);
        return new string(chars);
    }

    static void Main()
    {
        int[] nums = { 4, 8, 15, 16, 23, 42 };
        Console.WriteLine(Sum(nums)); // 108

        try
        {
            Console.WriteLine(nums[10]);
        }
        catch (IndexOutOfRangeException)
        {
            Console.WriteLine("That index doesn't exist in this array.");
        }

        Console.WriteLine(Reverse("hello")); // "olleh"

        var sb = new StringBuilder();
        for (int i = 0; i < 1000; i++)
        {
            if (i > 0) sb.Append(',');
            sb.Append(i);
        }
        string joined = sb.ToString();
        Console.WriteLine(joined.Substring(0, 20) + "..."); // just a peek at the start

        // TODO 5 fix: strings are immutable — ToUpper() returns a NEW
        // string instead of modifying `name` in place. The original line
        // discards that return value entirely. Fix:
        string name = "Belkacem";
        name = name.ToUpper(); // must reassign
        Console.WriteLine(name); // "BELKACEM"
    }
}
