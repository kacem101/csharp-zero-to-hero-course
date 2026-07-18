# Projects — Build Real Things, Not Just Exercises

The lessons teach concepts in isolation with `// TODO` gaps. These six
projects are different on purpose: each one gives you a **brief**
(a problem statement, requirements, and a best-practices/secure-coding
checklist) instead of a fill-in-the-blank template. You design the
classes, you organize the files, you decide the architecture — then
compare against a reference `solution/`.

All six are blue-team/defensive-security tools — useful, portfolio-worthy,
and aligned with DevSec rather than "how do I attack something."

## ⚠️ Ethical use
Project 4 (port scanner) and project 5 (log correlator) are network/security
tooling. Only ever point them at systems you own or are explicitly
authorized to test — your own machine, a lab VM, or a target provided in
an authorized exercise (e.g. an NSCS/CTF lab). Scanning or monitoring
systems without authorization is illegal in most jurisdictions, full stop.

## How each project is organized
```
projects/NN-project-name/
├── README.md      ← the brief: problem, requirements, best-practices checklist, which lessons it draws on
├── starter/        ← a skeleton: csproj + class/method signatures, no implementation
├── solution/       ← a full working reference implementation
└── tests/          ← xUnit self-check suite — run `dotnet test` here against your own starter/ code
```
Try to build the whole thing from the brief before opening `solution/`.
The checklist in each README is the real grading rubric — a working
program that skips the secure-coding checklist hasn't really finished
the assignment. The `tests/` folder gives you an objective pass/fail
instead of eyeballing your code against the reference solution.

## The six projects, in suggested order
1. [Password Strength & Pattern Auditor](01-password-strength-auditor/README.md) — after Module 2 (OOP)
2. [Log Threat Hunter](02-log-threat-hunter/README.md) — after Module 3 (LINQ)
3. [Encrypted Vault CLI](03-encrypted-vault-cli/README.md) — after Module 4 (Exceptions/IDisposable)
4. [Async Port & Banner Scanner](04-async-port-scanner/README.md) — after Module 5/6 (async, sockets)
5. [Live Log Correlator ("mini-SIEM")](05-live-log-correlator/README.md) — after Module 6, near the capstone
6. [Grand Capstone: Secure Password Manager](06-secure-password-manager/README.md) — after Module 7 (everything)
