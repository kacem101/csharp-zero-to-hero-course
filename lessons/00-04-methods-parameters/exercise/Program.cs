// TODO 1: Write `void Swap(ref int a, ref int b)` and prove it works on
// two local variables.

// TODO 2: Write `bool TryParsePositiveInt(string input, out int value)`
// that returns true and sets value only if the string parses to an int
// AND that int is positive; otherwise returns false and sets value to 0.

// TODO 3: Write two overloads of `Max`: one for two ints, one for two
// doubles. Call both and print the results.

// TODO 4: Write `void PrintReceipt(string item, double price, int qty =
// 1)` with a default quantity, and call it both with and without
// specifying qty.

// TODO 5 (bug hunt): why won't this compile, and how do you fix the call site?
static void Increment(ref int x) => x++;
int n = 5;
Increment(n); // ???
