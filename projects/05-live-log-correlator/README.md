# Project 5 — Live Log Correlator ("mini-SIEM")

## Scenario
Detection engineering, in miniature: watch a log file for new lines as
they're appended (like `tail -f`), run each new line through a set of
attack-signature regexes, and raise an alert event the moment something
matches — without re-scanning the whole file every time.

## Requirements
- `LogWatcher` class: polls a file for new content (track the last-read byte position; on each poll, read only what's new since last time — never re-read the whole file). Expose an event, e.g. `event Action<string> NewLine`, fired once per new line.
- `SignatureEngine` class: holds a set of compiled `Regex` rules with human-readable names, e.g.:
  - SQL injection: `('|--|;|\bUNION\b|\bOR\b\s+'?\d+'?='?\d+'?)`  (keep it simple — this is a demo detector, not a production WAF)
  - XSS: `<script|onerror=|onload=`
  - Path traversal: `\.\./` 
- Wire them together: `LogWatcher.NewLine` → `SignatureEngine.Check(line)` → if a rule matches, raise a `ThreatDetector`-style `event Action<Alert> AlertRaised` (Lesson 15's `event` pattern, not a public delegate field).
- An alert subscriber prints the alert to the console (with color if you like) AND appends a structured line to `alerts.log`.
- Support `Ctrl+C` to stop watching cleanly.
- Include a small script or simple mode that appends a mix of benign and malicious-looking lines to a target file over time, so you can demo detection live (this can be a second, trivial console app or a `--simulate` flag on the same program).

## Best-practices / secure-coding checklist
- [ ] Uses `event`, not a public delegate field, for `NewLine` and `AlertRaised` (Lesson 15) — prevents one subscriber from silently wiping out another's registration.
- [ ] Never re-reads the entire file on each poll — tracks a byte offset and reads only the delta (this matters at real log volumes; Lesson 22's streaming-not-loading-everything principle).
- [ ] Regex patterns are precompiled (`RegexOptions.Compiled` or a `static readonly Regex`), not rebuilt on every line checked.
- [ ] The alert log file write uses `using`/proper disposal, and a failure to write the alert log must not crash the whole watcher (catch specific I/O exceptions around that one operation).
- [ ] Clearly comment that these are **demo/teaching signatures** — a real detection engine needs much more nuance and a lower false-positive rate; don't oversell this as production-ready.

## Stretch goals
- Add a simple allowlist/suppression list (e.g. don't alert on traffic from a known internal test IP).
- Track and print a rolling alert-rate (alerts per minute) to catch a sudden burst.

## Lessons this draws on
Lesson 15 (events), Lesson 22 (streaming file I/O), Lesson 19a-adjacent (background polling loop), Lesson 16 (targeted exception handling).

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
