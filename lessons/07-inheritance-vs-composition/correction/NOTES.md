# Correction Notes — Lesson 07 — Inheritance vs Composition

## Answer

**Common mistakes to watch for:**
- Inheriting from a built-in collection to "add a couple of methods" — this is almost always composition in disguise.
- Using inheritance purely for code sharing between unrelated concepts.
- Forgetting that a legitimate `abstract class` base still needs its subtypes to be truly substitutable (see Lesson 08).
