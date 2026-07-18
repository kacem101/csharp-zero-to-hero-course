# Lesson 23 — Capstone: Concurrent URL Health Checker

## Why this matters
This project deliberately forces you to combine almost everything from the course into one coherent, realistic tool: OOP design (records, clean class structure), generics/collections (`List<HealthCheckResult>`), async + networking (concurrent, cancellable, bounded HTTP checks with a shared `HttpClient`), correct exception handling, `IDisposable` awareness, and a LINQ summary at the end.

## Requirements
1. Read a list of URLs from a text file — async file I/O (Lesson 22).
2. Check each URL **concurrently**, bounded to N at a time — `SemaphoreSlim` (Lesson 19 concurrency pattern).
3. Use a single shared `HttpClient` with an explicit timeout (Lesson 20).
4. Model each result as an immutable `record HealthCheckResult(string Url, bool IsUp, int? StatusCode, TimeSpan Duration)`.
5. Support cancellation via `Ctrl+C` — `CancellationTokenSource` wired to `Console.CancelKeyPress` (Lesson 19).
6. Catch specific exceptions (`HttpRequestException`, `TaskCanceledException`) — not bare `Exception` (Lesson 16).
7. Print a summary table at the end using LINQ (`GroupBy`, `Average`) (Lesson 13).
