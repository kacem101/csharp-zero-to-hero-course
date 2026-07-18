# Correction Notes — Lesson 00.5 — Arrays & Strings: the Safety Nets You Didn't Have in C

## Answer

**Common mistakes to watch for:**
- Calling a string method for its "side effect" and forgetting the result is a brand-new string that must be captured (same trap as Lesson 00.2, worth repeating here since it bites people constantly with `.ToUpper()`, `.Trim()`, `.Replace()`).
- Building large strings with repeated `+`/`+=` in a hot loop instead of `StringBuilder`.
- Assuming an out-of-bounds array access will "probably still work" like it sometimes does in C — in C# it always throws immediately and loudly.
