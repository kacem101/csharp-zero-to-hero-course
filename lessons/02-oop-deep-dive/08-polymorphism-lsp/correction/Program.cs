using System;
using System.Collections.Generic;

abstract class Shape
{
    public virtual string Describe() => "A shape";
    public abstract double Area();
}
class Circle : Shape
{
    public double Radius;
    public Circle(double r) => Radius = r;
    public override double Area() => Math.PI * Radius * Radius;
    public override string Describe() => base.Describe() + $" — a circle with radius {Radius}";
}
class Rectangle : Shape
{
    public double Width, Height;
    public Rectangle(double w, double h) { Width = w; Height = h; }
    public override double Area() => Width * Height;
    public override string Describe() => base.Describe() + $" — a {Width}x{Height} rectangle";
}

// TODO 2 fix: the problem is modeling "Fly" as something every Bird must
// support. Not all birds fly — that's a fact about the domain, so the
// type hierarchy should reflect it instead of faking a capability and
// throwing when it's missing.
abstract class Bird2 { }
interface IFlyable { void Fly(); }
class Sparrow2 : Bird2, IFlyable { public void Fly() => Console.WriteLine("Flying"); }
class Penguin2 : Bird2 { } // simply doesn't implement IFlyable — no fake method to break

class Program
{
    static void MakeItFlyIfItCan(Bird2 b)
    {
        if (b is IFlyable flyer) flyer.Fly();
        else Console.WriteLine("This bird can't fly.");
    }

    static void Main()
    {
        List<Shape> shapes = new() { new Circle(3), new Rectangle(4, 5) };
        foreach (var s in shapes) Console.WriteLine(s.Describe()); // polymorphism, no type checks needed

        MakeItFlyIfItCan(new Sparrow2()); // Flying
        MakeItFlyIfItCan(new Penguin2()); // This bird can't fly. — no crash, no LSP violation
    }
}
