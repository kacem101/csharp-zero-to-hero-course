# Lesson 16 — Exception Handling Deep Dive

## Why this matters
Exception handling done badly hides real bugs (catching too broadly), destroys debugging information (`throw ex;`), or uses exceptions where they were never meant to be used (control flow for expected cases). All three are extremely common in real codebases.

## The concept

### Catch the most specific exception you can meaningfully handle
```csharp
try { var data = File.ReadAllText(path); }
catch (FileNotFoundException) { Console.WriteLine("File not found — using defaults."); }
catch (UnauthorizedAccessException) { Console.WriteLine("No permission to read this file."); }
```
Catching bare `Exception` swallows genuine bugs (like a `NullReferenceException` elsewhere in the block) alongside the expected cases — you'll never know which one actually happened.

### Exception filters (`when`)
```csharp
try { CallExternalApi(); }
catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.TooManyRequests)
{ Console.WriteLine("Rate limited — back off and retry."); }
catch (HttpRequestException ex) { Console.WriteLine($"Request failed: {ex.Message}"); }
```

### Rethrowing correctly
```csharp
catch (Exception ex) { LogError(ex); throw ex; }  // BAD — resets the stack trace
catch (Exception ex) { LogError(ex); throw; }      // GOOD — preserves it
```

### Don't use exceptions for expected, routine control flow
```csharp
int ParseOrDefault(string input) { try { return int.Parse(input); } catch { return 0; } } // BAD — slow & unclear
int ParseOrDefault2(string input) => int.TryParse(input, out int result) ? result : 0;    // GOOD
```

### `using`/`finally` for guaranteed cleanup
```csharp
using var stream = File.OpenRead(path); // Dispose() called automatically at scope exit, even on exception
```
