// TODO 1: Write a switch expression `string Classify(int n)` that returns
// "zero", "negative", "small positive" (1-9), or "large positive" (10+).

// TODO 2: Given this hierarchy:
abstract class Shape { }
class Circle : Shape { public double Radius; }
class Rectangle : Shape { public double Width, Height; }
class Triangle : Shape { public double Base, Height; }
// Write `double Area(Shape s)` using a switch expression with type
// patterns (no manual casting, no if/else chain).

// TODO 3: Write `string Grade(int score)` using range-style `when`
// clauses: score >= 90 "A", >= 80 "B", >= 70 "C", else "F".

// TODO 4 (bug hunt): this switch expression won't compile — why, and fix it.
static string Check(object o) => o switch
{
    string s => "string",
    int n => "int"
    // no default arm
};
