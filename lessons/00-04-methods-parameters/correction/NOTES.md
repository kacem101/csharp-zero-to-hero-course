# Correction Notes — Lesson 00.4 — Methods & Parameters: ref/out vs C Pointers

## Answer

**Common mistakes to watch for:**
- Forgetting `ref`/`out` at the call site — C# requires it explicitly at both ends, unlike C where `&address` at the call site is the only signal.
- Using `out` when the method doesn't actually need the "no value going in" guarantee — if you're passing a value in AND getting a modified one out, `ref` is usually the better fit.
- Assuming overload resolution "just picks the closest" ambiguously — if two overloads are equally valid for the arguments given, the compiler raises an ambiguity error rather than silently guessing.
