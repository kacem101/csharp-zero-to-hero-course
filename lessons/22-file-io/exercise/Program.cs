// TODO 1: Write async Task WriteLinesAsync(string path, IEnumerable<string>
// lines) using StreamWriter with async writes, and
// async Task<List<string>> ReadLinesAsync(string path) using StreamReader
// with async reads. Round-trip a small list of strings through a temp
// file and print them back.

// TODO 2: Write async Task<string?> FindFirstMatchAsync(string path,
// string keyword) that streams a (potentially huge) file line by line
// and returns the first line containing keyword, WITHOUT loading the
// whole file into memory. Return null if not found.

// TODO 3 (bug hunt): fix the blocking-inside-async bug.
async Task<int> CountLinesAsync(string path)
{
    var lines = File.ReadAllLines(path); // problem?
    return lines.Length;
}
