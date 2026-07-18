# C# Zero to Hero — Course Summary

## Why learn C#

For a cybersecurity/DevSec student, C# is a strange, useful gap to fill.
You already know C — you understand memory, pointers, and how programs
actually execute. C# takes that foundation and adds the things that make
building *real, secure, maintainable* software practical: a managed
runtime with no manual memory bugs, a serious standard library for
cryptography and networking, and a type system strict enough to catch
whole categories of mistakes at compile time instead of in production.

Three reasons this specific language, for this specific direction:

- **It's the language of the platform, not just an app.** ASP.NET Core,
  Windows internals, Active Directory tooling, EDR/XDR agents (like
  SMAI's own agent), and a large share of enterprise security tooling
  are built in C#/.NET. If you're doing DevSec, blue-team tooling, or
  Windows-focused security work, C# isn't optional background — it's
  where a lot of the actual work happens.
- **The standard library takes security seriously.** `System.Security.Cryptography`
  gives you correct, audited implementations of AES-GCM, PBKDF2,
  constant-time comparison — the primitives this course's Lesson 22a and
  Projects 3/6 are built on. You don't need to trust a random npm
  package for something as sensitive as password hashing.
- **It rewards the habits DevSec already wants from you.** Strong typing,
  explicit nullability, mandatory exception handling, and a
  garbage-collected runtime all push you toward code that fails loudly
  and predictably instead of silently and dangerously — which is most of
  what "secure coding" actually is in practice.

## What you get out of it

- A language you can build real tools in — not toy scripts, but things
  like the SMAI agent you're already extending, or a genuine internal
  tool for CIC CyberInvators.
- Transferable systems thinking: async I/O, concurrency, memory
  management, and networking in C# map directly onto the same concepts
  in any other language you'll touch later (Go, Rust, even back to C).
- A portfolio, not just notes. Six projects — a password auditor, a log
  threat hunter, an encrypted vault, a port scanner, a mini-SIEM, and a
  full layered password manager — are real, demonstrable, blue-team-flavored
  work, not "print hello world" exercises.

## What's in the course

**37 lessons** across 8 modules, each with a deep explanation, a
`// TODO` exercise, and a full worked correction:

| Module | Focus |
|---|---|
| 0 | Bridging C/DSA knowledge into C# syntax and idioms |
| 1 | Type system foundations — value vs. reference, records, nullability, boxing |
| 2 | OOP done properly — encapsulation, inheritance vs. composition, SOLID |
| 3 | Generics, collections, and LINQ |
| 4 | Delegates/events, exception handling, resource management |
| 5 | Async/await, real multithreading, unsafe/`Span<T>` |
| 6 | Networking (`HttpClient`, raw sockets), file I/O, secure coding |
| 7 | Testing (xUnit), dependency injection, project structure |

Plus a **capstone** (a concurrent URL health checker tying the whole
language together) and **six standalone projects**, each with a brief,
a secure-coding checklist, a starter skeleton, a full solution, and an
automated xUnit self-check suite so you know objectively whether your
implementation actually works — not just whether it compiles.

## How to actually keep up with it

A course like this dies from two failure modes: going too fast and
losing depth, or stalling out entirely. A few concrete habits that work
better than "I'll get to it":

- **One lesson, fully, beats three lessons skimmed.** Every lesson has a
  "common mistakes" section for a reason — those are the bugs that
  actually show up in real code. If you can't explain why the bad
  example is bad, the lesson isn't done yet.
- **Run the self-check tests, don't eyeball the solution.** For the six
  projects, `cd tests && dotnet test` gives you an honest, objective
  answer. Comparing your code to `solution/` by reading is where people
  quietly fool themselves.
- **Track it.** Use the progress tracker (`tools/progress-tracker.html`
  — open it, check things off, it remembers state between sessions) so
  "how far am I" is a fact, not a feeling.
- **Timebox module by module, not lesson by lesson.** Modules 0–2 are
  foundational and worth taking slowly. Module 5 (async/concurrency) is
  genuinely hard — budget more time there than the lesson count suggests.
- **Do the projects at the module checkpoints they're placed at**, not
  all at the end. A project right after you learn something is what
  makes it stick; six projects done in one sitting at the end is just
  more exercises.
- **When you get stuck, isolate the smallest failing case.** This is
  the actual skill the course is trying to build alongside C# itself —
  it's the same debugging instinct you already use in C, applied to a
  new environment.

If you're planning to bring this to CIC CyberInvators as an onboarding
track rather than just for yourself, the project checklists double as a
grading rubric, and the self-check tests mean you're not the sole
bottleneck for verifying everyone's work.
