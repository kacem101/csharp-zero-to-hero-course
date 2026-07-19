// TODO 1: Rewrite the Shape hierarchy from Lesson 07 with a virtual
// method `Describe()` on the base and overrides in Circle/Rectangle that
// call `base.Describe()` then add shape-specific detail. Demonstrate
// polymorphism by looping over a List<Shape> and calling Describe().

// TODO 2 (bug hunt / redesign): find the LSP violation and fix it by
// redesigning (don't just patch the symptom).
class Bird { public virtual void Fly() => Console.WriteLine("Flying"); }
class Sparrow : Bird { }
class Penguin : Bird
{
    public override void Fly() => throw new NotSupportedException("Penguins can't fly");
}
void MakeItFly(Bird b) => b.Fly(); // crashes if b is a Penguin
