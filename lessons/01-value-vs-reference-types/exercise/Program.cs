// TODO 1: Define a `readonly struct Vector2` with X and Y (double), a
// constructor, and a method `Length()` that returns sqrt(X*X + Y*Y).

// TODO 2: Define a `class Player` with a mutable `int Health` field and a
// method `TakeDamage(int amount)` that reduces Health.

// TODO 3: Write a method `void Heal(Player p)` that sets p.Health += 20.
// Call it on a Player instance and prove the change is visible to the
// caller (print Health before and after).

// TODO 4: Write a method `void TryHeal(Vector2 v)` that adds 1 to v.X.
// Call it on a Vector2 (you'll need to make a mutable copy, or explain in
// a comment why this won't compile on a readonly struct). Prove that the
// change does NOT propagate back to the caller's original Vector2.

// TODO 5 (bug hunt): The following struct is written like a class-style
// entity. Explain in a comment why it's a design mistake, then rewrite it
// as a class.
struct Inventory
{
    public List<string> Items;
}
