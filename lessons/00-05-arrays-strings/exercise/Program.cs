// TODO 1: Given int[] nums = { 4, 8, 15, 16, 23, 42 }, write a method
// `int Sum(int[] arr)` that sums all elements using a `for` loop, and
// prove it works.

// TODO 2: Wrap an out-of-bounds array access in a try/catch and catch
// IndexOutOfRangeException specifically, printing a friendly message
// instead of letting the program crash.

// TODO 3: Write `string Reverse(string input)` using the ToCharArray +
// Array.Reverse approach shown above.

// TODO 4: Use a StringBuilder to build the string "0,1,2,3,...,999"
// (comma-separated numbers 0 through 999) efficiently.

// TODO 5 (bug hunt): what's wrong with this C-habits code, and how do
// you fix it in idiomatic C#?
string name = "Belkacem";
name.ToUpper();
Console.WriteLine(name); // expected uppercase, got lowercase — why?
