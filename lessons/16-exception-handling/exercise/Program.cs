// TODO 1: Write a method `Product? SafeGetProduct(Dictionary<int,
// Product> catalog, int id)` that catches only the exceptions that make
// sense (think about what Dictionary indexing can throw) and returns
// null instead of crashing.

// TODO 2: Write a method that calls a flaky external operation (simulate
// with a method that throws InvalidOperationException on the first 2
// calls, then succeeds) and retries up to 3 times, using a specific
// catch — not a bare catch(Exception).

// TODO 3 (bug hunt): find TWO separate bugs in this code and fix both.
void ProcessFile(string path)
{
    try
    {
        var text = File.ReadAllText(path);
        Process(text);
    }
    catch (Exception ex)
    {
        Console.WriteLine("Something went wrong");
        throw ex;
    }
}
