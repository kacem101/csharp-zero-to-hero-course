# Correction Notes — Lesson 23 — Capstone: Concurrent URL Health Checker

## Answer



## Where each lesson shows up
- **Records & clean design** → `HealthCheckResult` is immutable, matching Lesson 06's encapsulation principle (no way to mutate a result after it's created).
- **Generics/Collections** → `List<HealthCheckResult>`, LINQ `GroupBy`/`Average` (Lessons 11–13).
- **Async fundamentals + concurrency bound** → `SemaphoreSlim` + `Task.WhenAll` (Lessons 18–19), exactly the pattern from Lesson 19's "concurrent, bounded" example.
- **HttpClient reuse** → one shared static `Client` (Lesson 20) — critical here since dozens of checks run concurrently.
- **Cancellation** → `CancellationTokenSource` wired to `Ctrl+C`, `CancellationToken` threaded through every async call (Lesson 19).
- **Specific exception handling** → `HttpRequestException` and `TaskCanceledException` caught separately, not a bare `catch (Exception)` (Lesson 16).
- **File I/O** → async read of the URL list (Lesson 22).

**Extension ideas once this works:** add retry-with-backoff for flaky sites (Lesson 16's retry pattern), write results to a CSV file (Lesson 22), or add a `--json` output mode using `System.Text.Json`.
