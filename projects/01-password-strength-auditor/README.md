# Project 1 — Password Strength & Pattern Auditor

## Scenario
CIC CyberInvators wants a small internal CLI tool that audits a password
and explains *why* it's weak or strong — not just a pass/fail, but the
specific patterns an attacker's cracking toolkit would exploit.

## Requirements
- Accept a password from `Console.ReadLine()` (loop until the user types `exit`).
- Report:
  1. **Entropy estimate** (bits), based on the character space actually
     used (lowercase / uppercase / digits / symbols) and length.
  2. **Pattern detection** — flag, with a specific reason each:
     - Sequential characters (`abcd`, `1234`, `4321`)
     - Keyboard walks (`qwerty`, `asdfgh`, `zxcvbn`)
     - Repeated characters (`aaaa`, `1111`)
     - Common leetspeak substitutions of dictionary words (`p@ssw0rd`, `adm1n`)
     - Membership in a small built-in "commonly used passwords" list
  3. **Overall strength classification**: Very Weak / Weak / Moderate / Strong / Very Strong,
     combining entropy with pattern penalties (a high-entropy password
     that's still a leetspeak dictionary word should NOT be rated "Strong").
- Never print the password back in a log file or write it anywhere persistent — this is a live-analysis tool only, not a password logger.

## Best-practices / secure-coding checklist
- [ ] Analysis logic lives in its own class (`PasswordAnalyzer`), separate from the CLI loop in `Program.cs` — single responsibility (Lesson 10).
- [ ] The common-password list and keyboard-walk list are `HashSet<string>`/collections chosen for the access pattern, not `List<string>.Contains()` in a hot path (Lesson 12).
- [ ] No password is ever logged, written to disk, or included in an exception message.
- [ ] Input handling doesn't crash on empty string, null, or extremely long input — validate defensively (Lesson 16).
- [ ] Code close-reads cleanly: a reviewer should be able to tell WHY a password was flagged from your code, not just that it was.

## Stretch goals
- Support reading a batch of passwords from a file (one per line) and output a CSV report.
- Add a rough "estimated crack time" using entropy and an assumed guesses/second rate (clearly labeled as a rough estimate, not a guarantee).

## Lessons this draws on
Module 2 (OOP/encapsulation), Lesson 12 (collections), Lesson 16 (exceptions/validation), Lesson 22a (secure coding mindset).

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
