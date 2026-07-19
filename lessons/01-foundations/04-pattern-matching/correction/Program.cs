using System;

abstract class Shape { }
class Circle : Shape { public double Radius; }
class Rectangle : Shape { public double Width, Height; }
class Triangle : Shape { public double Base, Height; }

class Program
{
    static string Classify(int n) => n switch
    {
        0 => "zero",
        < 0 => "negative",
        < 10 => "small positive",
        _ => "large positive"
    };

    static double Area(Shape s) => s switch
    {
        Circle c => Math.PI * c.Radius * c.Radius,
        Rectangle r => r.Width * r.Height,
        Triangle t => 0.5 * t.Base * t.Height,
        _ => throw new ArgumentException("Unknown shape")
    };

    static string Grade(int score) => score switch
    {
        >= 90 => "A",
        >= 80 => "B",
        >= 70 => "C",
        _ => "F"
    };

    // Fix: a switch EXPRESSION (unlike a switch STATEMENT) must be
    // exhaustive — the compiler can't prove every `object` is covered by
    // just `string`/`int`, so it requires a discard arm `_` to handle
    // everything else.
    static string Check(object o) => o switch
    {
        string s => "string",
        int n => "int",
        _ => "something else"
    };

    static void Main()
    {
        Console.WriteLine(Classify(-5));
        Console.WriteLine(Area(new Circle { Radius = 2 }));
        Console.WriteLine(Grade(85));
    }
}
