// TODO 1 (bug hunt): This design reuses code via inheritance where it
// shouldn't. Identify the problem and rewrite using composition.
class Queue : List<string>
{
    public void Enqueue(string item) => Add(item);
    public string Dequeue()
    {
        var first = this[0];
        RemoveAt(0);
        return first;
    }
}

// TODO 2: Design a `Car` and `Engine` relationship using composition
// (a Car HAS-A Engine, it is not an Engine). Engine should have a
// `Start()` method that prints a message; Car should have a `Drive()`
// method that starts the engine then prints "Driving...".

// TODO 3: Design `Employee` (abstract base with Name and abstract
// CalculatePay()) with two real "is-a" subtypes: SalariedEmployee and
// HourlyEmployee. This one SHOULD use inheritance — explain why in a
// comment.
