// TODO 1: Write two versions of a function that sums a million integers:
//   SumWithArrayList() using System.Collections.ArrayList
//   SumWithGenericList() using List<int>
// Use System.Diagnostics.Stopwatch to time both and print the difference.

// TODO 2: Explain in a comment: why does storing a `struct` (like our
// Vector2 from Lesson 01) in a List<object> box it, but storing it in a
// List<Vector2> not box it?

// TODO 3 (bug hunt): what's wrong here, and how would you fix it?
object[] mixedBag = new object[3];
mixedBag[0] = 5;
mixedBag[1] = "hello";
mixedBag[2] = 3.14;
int sum = 0;
foreach (var item in mixedBag)
    sum += (int)item; // crashes on "hello"
