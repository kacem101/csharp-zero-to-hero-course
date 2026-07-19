// TODO 1: Build the echo server above as a full runnable console app.

// TODO 2: Build a matching TCP client that connects to 127.0.0.1:5000,
// sends "Hello, server!", and prints the echoed response.

// TODO 3: Modify BOTH server and client to use length-prefix framing
// (4-byte length + payload) instead of a single naive Read()/Write(),
// so a message longer than one TCP packet's worth of data is still
// received correctly and completely.

// TODO 4: Explain in a comment why HTTPS to the same host repeatedly is
// faster when using a shared HttpClient than when creating a new
// HttpClient each time — connect this to the 4-step breakdown above.
