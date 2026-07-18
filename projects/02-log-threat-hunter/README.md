# Project 2 — Log Threat Hunter

## Scenario
You're given a sample authentication log from a web application. Your
job: build a CLI tool that reads it and surfaces suspicious activity a
human analyst would want to see first, using LINQ instead of manual
loops wherever it makes the intent clearer.

A synthetic sample log is provided at `sample-data/auth.log` — lines look like:
```
2026-07-18 10:15:23 LOGIN_FAILED user=alice ip=203.0.113.5
2026-07-18 10:15:41 LOGIN_FAILED user=alice ip=203.0.113.5
2026-07-18 10:22:03 LOGIN_SUCCESS user=bob ip=198.51.100.9
```

## Requirements
- Parse each line into an immutable `record LogEntry(DateTime Timestamp, string EventType, string User, string Ip)`.
- Detect **brute-force attempts**: an IP with 5 or more `LOGIN_FAILED` events within any 2-minute window.
- Detect **impossible travel**: the same user with a `LOGIN_SUCCESS` from two *different* IPs within 60 seconds of each other.
- Print a summary report:
  - Top offending IPs by failed-login count
  - Any flagged brute-force windows (IP, window start, count)
  - Any flagged impossible-travel events (user, both IPs, time gap)
- Handle malformed lines gracefully (log a warning to stderr, skip the line — don't crash the whole run over one bad line).

## Best-practices / secure-coding checklist
- [ ] Parsing logic and detection logic are in separate classes (`LogParser`, `ThreatDetector`) — not one giant `Main`.
- [ ] Detection queries use LINQ (`GroupBy`, `Where`, windowed comparisons) rather than nested nested loops, and stay readable (Lesson 13).
- [ ] A malformed line causes a warning, never an unhandled exception that kills the whole analysis run (Lesson 16).
- [ ] The tool works on files far larger than fit comfortably in a `List<string>` printed in full — don't design yourself into loading-then-discarding patterns that wouldn't scale (Lesson 22, streaming awareness even if this file is small).
- [ ] No credentials or full log lines are echoed back in error messages — only the specific field that failed to parse.

## Stretch goals
- Make the brute-force window size and threshold configurable via command-line arguments.
- Output the report as JSON in addition to console text, using `System.Text.Json`.

## Lessons this draws on
Lesson 01a (records), Module 3 (LINQ/generics/collections), Lesson 16 (exception handling), Lesson 22 (file I/O).

## Self-check tests
A `tests/` folder ships alongside `starter/` and `solution/` with an
xUnit suite that checks your implementation against the requirements
above — not against the reference solution's exact internals. Run it
against your own work:
```bash
cd tests
dotnet test
```
It's pre-wired to `../starter/exercise.csproj`. Once you've replaced the
`NotImplementedException` stubs, red tests turn green. (To sanity-check
the reference solution instead, point the `ProjectReference` in
`verify.csproj` at `../solution/solution.csproj`.)
