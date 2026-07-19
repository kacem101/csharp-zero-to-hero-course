using System;

class Program
{
    static void Swap(ref int a, ref int b)
    {
        int tmp = a; a = b; b = tmp;
    }

    static bool TryParsePositiveInt(string input, out int value)
    {
        if (int.TryParse(input, out value) && value > 0) return true;
        value = 0;
        return false;
    }

    static int Max(int a, int b) => a > b ? a : b;
    static double Max(double a, double b) => a > b ? a : b;

    static void PrintReceipt(string item, double price, int qty = 1)
        => Console.WriteLine($"{qty} x {item} @ {price:C} = {price * qty:C}");

    static void Main()
    {
        int x = 1, y = 2;
        Swap(ref x, ref y);
        Console.WriteLine($"{x}, {y}"); // 2, 1

        if (TryParsePositiveInt("42", out int v)) Console.WriteLine(v); // 42
        if (!TryParsePositiveInt("-5", out int v2)) Console.WriteLine("rejected: not positive");

        Console.WriteLine(Max(3, 7));       // calls the int overload
        Console.WriteLine(Max(3.5, 2.1));   // calls the double overload

        PrintReceipt("Coffee", 2.50);       // qty defaults to 1
        PrintReceipt("Coffee", 2.50, 3);    // explicit qty

        // TODO 5 fix: `ref` must be written at the CALL SITE too, not
        // just the method signature — this is deliberate, so you can
        // never accidentally pass something by reference without
        // realizing it (unlike C, where & is easy to forget or add by
        // accident with very different consequences):
        int n = 5;
        Increment(ref n);
        Console.WriteLine(n); // 6
    }

    static void Increment(ref int x) => x++;
}
