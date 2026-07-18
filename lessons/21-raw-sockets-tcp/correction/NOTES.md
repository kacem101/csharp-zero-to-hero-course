# Correction Notes — Lesson 21 — Raw Sockets & TCP

## Answer

**Common mistakes to watch for:**
- Assuming `stream.Read()` returns one full "message" — TCP has no concept of message boundaries; you must define your own framing.
- Forgetting `ReadExactlyAsync` (or a manual read-loop) is required to guarantee you actually got all N bytes you expect — a single `ReadAsync` can return fewer.
- Rolling custom TCP protocols for problems an existing, battle-tested protocol (HTTP, gRPC, WebSockets) already solves correctly.
