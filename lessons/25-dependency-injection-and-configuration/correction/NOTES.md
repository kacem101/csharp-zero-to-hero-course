# Correction Notes — Lesson 25 — Dependency Injection & Configuration

## Answer

**Common mistakes to watch for:**
- Registering a service with mutable per-request state as `Singleton` — causes data to leak between unrelated requests/users.
- Manually `new`-ing a dependency inside a class registered with DI, instead of taking it as a constructor parameter — silently reintroduces the Dependency Inversion violation from Lesson 10, even though DI is "available."
- Hardcoding configuration values instead of reading them through `IConfiguration`/`IOptions<T>` — makes it impossible to change settings per environment (dev/staging/prod) without recompiling.
