// TODO 1: Write a generic class `Pair<T1, T2>` holding a First and
// Second value of possibly different types, with a `Swap()` method that
// returns a new Pair<T2, T1> with the values reversed.

// TODO 2: Write a generic method `T FindMax<T>(List<T> items) where T :
// IComparable<T>` that returns the largest item in the list (throw if
// the list is empty).

// TODO 3: Write a generic class `Repository<T> where T : class` with an
// internal List<T>, `Add(T item)`, `GetAll()` returning IReadOnlyList<T>,
// and `Find(Func<T, bool> predicate)` returning the first match or null.

// TODO 4 (bug hunt): why won't this compile, and how do you fix it?
public class Wrapper<T>
{
    public T Value;
    public bool IsDefault() => Value == null; // error for value types
}
