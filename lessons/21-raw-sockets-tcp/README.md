# Lesson 21 — Raw Sockets & TCP

## Why this matters
Most apps never touch a raw socket — but understanding what's underneath `HttpClient` explains *why* it behaves the way it does, and you'll need this if you ever build a custom protocol (useful territory for a security student: this is also exactly the layer where a lot of network-level attacks and defenses operate).

## The concept

### What actually happens under an HTTP call
1. **DNS resolution** — hostname → IP address.
2. **TCP handshake** — SYN, SYN-ACK, ACK establishes a connection on port 443/80.
3. **TLS handshake** (HTTPS only) — certificates exchanged, secure channel negotiated.
4. **HTTP request/response** — sent over the now-established connection.

`HttpClient`'s connection pooling reuses steps 2/3 across requests to the same host — which is exactly why creating a new `HttpClient` per call (Lesson 20) is so costly: you pay the full TCP+TLS handshake on every single request instead of once.

### Raw TCP with TcpClient/TcpListener

```csharp
// Minimal TCP echo server
var listener = new TcpListener(IPAddress.Any, 5000);
listener.Start();
while (true)
{
    using TcpClient client = await listener.AcceptTcpClientAsync();
    using NetworkStream stream = client.GetStream();
    byte[] buffer = new byte[1024];
    int bytesRead = await stream.ReadAsync(buffer);
    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
    byte[] response = Encoding.UTF8.GetBytes($"Echo: {message}");
    await stream.WriteAsync(response);
}
```

### The critical TCP gotcha: it's a stream, not a message protocol
```csharp
// Bad — assumes one Read() call == one logical message
int bytesRead = stream.Read(buffer); // may return PART of what was sent,
                                       // or MULTIPLE sends coalesced together
```
Fix: frame your messages, e.g. with a length prefix.
```csharp
// Sender
byte[] lengthPrefix = BitConverter.GetBytes(payload.Length);
await stream.WriteAsync(lengthPrefix);
await stream.WriteAsync(payload);
// Receiver
byte[] lengthBuffer = new byte[4];
await stream.ReadExactlyAsync(lengthBuffer); // .NET 7+: guarantees the full 4 bytes
int length = BitConverter.ToInt32(lengthBuffer);
byte[] messageBuffer = new byte[length];
await stream.ReadExactlyAsync(messageBuffer);
```
(For real applications, prefer an established protocol — HTTP, gRPC, WebSockets — over hand-rolled TCP framing. This section is here so you understand *why* those protocols exist.)
