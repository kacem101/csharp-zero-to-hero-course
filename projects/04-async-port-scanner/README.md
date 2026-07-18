# Project 4 — Async Port & Banner Scanner

## ⚠️ Only scan systems you own or are authorized to test
Run this against `localhost`, your own lab VMs, or an authorized CTF/lab
target only. Unauthorized scanning of systems you don't own or have
explicit permission to test is illegal in most jurisdictions.

## Scenario
Build a small, fast, cancellable TCP connect scanner — the same basic
technique tools like `nmap` use for a "connect scan," scaled down and
built from what you already know: bounded async concurrency (Lesson 19/19a)
and raw sockets (Lesson 21).

## Requirements
- Accept a target host and a port range (e.g. `1-1024`) via command-line arguments, defaulting to `localhost` and a small common-ports range if not given.
- Scan ports **concurrently**, bounded to a configurable max (default 50) using `SemaphoreSlim` — do not open unbounded simultaneous connections.
- For each port: attempt `TcpClient.ConnectAsync` with a short timeout (e.g. 500ms) via `CancellationTokenSource`. If it connects, mark the port open.
- For open ports, attempt a quick **banner grab**: read whatever bytes the service sends first (with its own short timeout) — many services (SSH, SMTP, some HTTP servers) announce themselves immediately on connect.
- Support `Ctrl+C` to cancel the whole scan cleanly (reuse the pattern from the Lesson 23 capstone) and still print whatever results were gathered before cancellation.
- Print a results table: port, open/closed, and banner (if any) for open ports.

## Best-practices / secure-coding checklist
- [ ] Concurrency is explicitly bounded (`SemaphoreSlim`) — never "fire everything at once" (Lesson 19a, Lesson 23).
- [ ] Every socket operation has its own timeout — a single hung port must never stall the whole scan (Lesson 20's timeout lesson, applied to raw sockets).
- [ ] `TcpClient`/`NetworkStream` are properly disposed (`using`) on every code path, including exceptions (Lesson 17).
- [ ] Catches specific exceptions (`SocketException`, `OperationCanceledException`) — not a bare `catch (Exception)` swallowing real bugs (Lesson 16).
- [ ] The README/usage text includes the authorized-use disclaimer, and the program prints it on startup too.

## Stretch goals
- Add a `--top-ports` mode using a small built-in list of the ~20 most commonly open ports instead of scanning a full range.
- Export results as CSV or JSON.

## Lessons this draws on
Lesson 19/19a (async concurrency, `SemaphoreSlim`), Lesson 21 (raw sockets), Lesson 17 (`IDisposable`), Lesson 16 (specific exception handling).

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
