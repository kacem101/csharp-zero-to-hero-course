# C# Zero to Hero — Practice Course

New here? Read [COURSE_SUMMARY.md](COURSE_SUMMARY.md) first — why C#, what's
in the course, and how to actually stick with it. Then open
[tools/progress-tracker.html](tools/progress-tracker.html) to track your progress
through everything below (it remembers your state between sessions).

A hands-on companion course laid out as a repo: each lesson has a detailed
explanation, a runnable exercise with TODOs, and a full correction.

**Starting point:** you know C and basic data structures & algorithms, but
you're new to C#. Module 0 is written specifically to translate what you
already know (pointers, structs, manual memory management, arrays) into
C#'s equivalent tools, before Module 1 goes deep on language internals.
Later modules include a dedicated security lesson and real-world
engineering practices (testing, DI, project structure) — written with a
cybersecurity/DevSec audience in mind.

## How to use this repo
Each lesson lives in `lessons/NN-topic-name/`:
- `README.md` — the full explanation of the concept (read this first)
- `exercise/` — a `dotnet run`-able project with `// TODO` gaps for you to fill in
- `correction/` — the full solution, runnable the same way, plus `NOTES.md` with common mistakes

```bash
cd lessons/00-01-hello-csharp/exercise
dotnet run
```

(A few lessons use `dotnet test` instead of `dotnet run` — Lesson 24 is an
xUnit test project, and Lesson 26 is a CLI walkthrough rather than a
single file. Both are noted in their own `INSTRUCTIONS.md`.)

## Module 0 — From C to C# (start here if you're new to C#)
- [00.1 - Hello C#: Project Structure & the Console](lessons/00-01-hello-csharp/README.md)
- [00.2 - Variables, Types & Operators: the C# vs C Cheat Sheet](lessons/00-02-variables-types-operators/README.md)
- [00.3 - Control Flow: if/else, Loops, switch](lessons/00-03-control-flow/README.md)
- [00.4 - Methods & Parameters: ref/out vs C Pointers](lessons/00-04-methods-parameters/README.md)
- [00.5 - Arrays & Strings: the Safety Nets You Didn't Have in C](lessons/00-05-arrays-strings/README.md)
- [00.6 - Classes & Objects: From C structs to C# Objects](lessons/00-06-classes-objects/README.md)
- [00.7 - Intro to Collections: List<T> as a Safer, Growable Array](lessons/00-07-collections-intro/README.md)

## Module 1 — Foundations
- [01 - Value Types vs Reference Types](lessons/01-value-vs-reference-types/README.md)
- [01a - Records and Immutability](lessons/01a-records-and-immutability/README.md)
- [02 - Nullable Types](lessons/02-nullable-types/README.md)
- [03 - Boxing and Unboxing](lessons/03-boxing-unboxing/README.md)
- [04 - Pattern Matching](lessons/04-pattern-matching/README.md)
- [05 - Expression-Bodied Members](lessons/05-expression-bodied-members/README.md)

## Module 2 — OOP Deep Dive
- [06 - Encapsulation](lessons/06-encapsulation/README.md)
- [07 - Inheritance vs Composition](lessons/07-inheritance-vs-composition/README.md)
- [08 - Polymorphism & Liskov Substitution](lessons/08-polymorphism-lsp/README.md)
- [09 - Interfaces vs Abstract Classes](lessons/09-interfaces-vs-abstract-classes/README.md)
- [10 - SOLID Principles](lessons/10-solid-principles/README.md)

## Module 3 — Generics, Collections, LINQ
- [11 - Generics](lessons/11-generics/README.md)
- [12 - Choosing the Right Collection](lessons/12-choosing-collections/README.md)
- [13 - LINQ & Deferred Execution](lessons/13-linq-deferred-execution/README.md)
- [14 - IQueryable vs IEnumerable](lessons/14-iqueryable-vs-ienumerable/README.md)

## Module 4 — Delegates, Errors, Resources
- [15 - Delegates, Func/Action, Events](lessons/15-delegates-events/README.md)
- [16 - Exception Handling Deep Dive](lessons/16-exception-handling/README.md)
- [17 - IDisposable & Memory Management](lessons/17-idisposable-memory/README.md)

## Module 5 — Async & Concurrency Deep Dive
- [18 - async/await Fundamentals](lessons/18-async-await-fundamentals/README.md)
- [19 - Deadlocks, Task Types, Cancellation, async void](lessons/19-async-deadlocks-cancellation/README.md)
- [19a - Multithreading & Locks](lessons/19a-multithreading-and-locks/README.md)
- [19b - unsafe Pointers & Span\<T\>](lessons/19b-unsafe-pointers-and-span/README.md)

## Module 6 — Networking & I/O
- [20 - HttpClient Done Right](lessons/20-httpclient/README.md)
- [21 - Raw Sockets & TCP](lessons/21-raw-sockets-tcp/README.md)
- [22 - File I/O](lessons/22-file-io/README.md)
- [22a - Secure Coding Fundamentals in C#](lessons/22a-secure-coding-fundamentals/README.md)

## Capstone
- [23 - Concurrent URL Health Checker](lessons/23-capstone-url-health-checker/README.md)

## Module 7 — Real-World Engineering Practices
- [24 - Testing with xUnit](lessons/24-testing-with-xunit/README.md)
- [25 - Dependency Injection & Configuration](lessons/25-dependency-injection-and-configuration/README.md)
- [26 - Solution & Project Structure](lessons/26-solution-and-project-structure/README.md)

## Projects — Build Real Things
Six standalone, cybersecurity-themed projects that force you to combine
lessons instead of practicing them in isolation. Each ships as a brief
(requirements + secure-coding checklist) plus a `starter/` skeleton and a
full `solution/` — see [projects/README.md](projects/README.md) for the
full index and the ethical-use note for the networking-related ones.

1. [Password Strength & Pattern Auditor](projects/01-password-strength-auditor/README.md)
2. [Log Threat Hunter](projects/02-log-threat-hunter/README.md)
3. [Encrypted Vault CLI](projects/03-encrypted-vault-cli/README.md)
4. [Async Port & Banner Scanner](projects/04-async-port-scanner/README.md)
5. [Live Log Correlator ("mini-SIEM")](projects/05-live-log-correlator/README.md)
6. [Grand Capstone: Secure Password Manager](projects/06-secure-password-manager/README.md)
