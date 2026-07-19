# Correction Notes — Lesson 20 — HttpClient Done Right

## Answer

**Common mistakes to watch for:**
- `new HttpClient()` inside a method that gets called repeatedly — always share one instance (or use `IHttpClientFactory` in ASP.NET Core).
- Calling `ReadFromJsonAsync` without first checking the status code — a non-2xx response with an HTML/text error body produces a confusing deserialization exception instead of a clear error.
- Never setting a `Timeout` — the 100-second default can leave your app hanging far longer than acceptable for a responsive service.
