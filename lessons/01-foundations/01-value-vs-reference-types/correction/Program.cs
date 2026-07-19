using System;
using System.Collections.Generic;

readonly struct Vector2
{
    public readonly double X, Y;
    public Vector2(double x, double y) { X = x; Y = y; }
    public double Length() => Math.Sqrt(X * X + Y * Y);
}

class Player
{
    public int Health;
    public Player(int health) => Health = health;
    public void TakeDamage(int amount) => Health -= amount;
}

class Program
{
    static void Heal(Player p) => p.Health += 20;

    // A struct method can't mutate `this` if the struct is `readonly`, and
    // even on a normal (non-readonly) struct, passing it by value into a
    // method means the method only ever sees a COPY. To actually mutate
    // the caller's value you'd need `ref Vector2 v`.
    static Vector2 TryHeal(Vector2 v) => new Vector2(v.X + 1, v.Y);

    static void Main()
    {
        var player = new Player(50);
        Console.WriteLine($"Before: {player.Health}");
        Heal(player);
        Console.WriteLine($"After: {player.Health}"); // 70 — reference semantics

        var v = new Vector2(1, 1);
        var v2 = TryHeal(v); // v itself is untouched; v2 is a new value
        Console.WriteLine($"Original v.X: {v.X}, new v2.X: {v2.X}");
    }
}

// Bug hunt answer:
// `Inventory` looks like a value type but holds a reference-type field
// (List<string>). Copying an Inventory struct copies the *reference* to
// the same List, not the list contents — so two "independent" Inventory
// copies would still share and corrupt the same underlying list. Also,
// an inventory is an entity with identity and mutable state over time —
// exactly the case for a class, not a struct:
class Inventory
{
    public List<string> Items = new();
}
