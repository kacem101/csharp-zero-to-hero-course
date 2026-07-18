# Lesson 22 — File I/O

## Why this matters
Synchronous file I/O inside an async app defeats the whole point of going async — and loading huge files fully into memory when you only need to scan them wastes memory for no reason.

## The concept

```csharp
// Good — async file I/O for anything beyond trivial scripts
string content = await File.ReadAllTextAsync(path);
await File.WriteAllTextAsync(path, content);
```

```csharp
// Bad — sync I/O blocks a thread pool thread inside an async method
public async Task ProcessAsync(string path)
{
    string content = File.ReadAllText(path); // blocks during disk I/O
    await SomethingElseAsync(content);
}
```

### Stream large files instead of loading them fully
```csharp
// Good — process line by line
using var reader = new StreamReader(path);
string? line;
while ((line = await reader.ReadLineAsync()) != null)
    ProcessLine(line);
```
```csharp
// Bad — loads the ENTIRE file into memory just to scan it
string[] allLines = File.ReadAllLines(hugeLogFile);
var match = allLines.FirstOrDefault(l => l.Contains("ERROR"));
```
