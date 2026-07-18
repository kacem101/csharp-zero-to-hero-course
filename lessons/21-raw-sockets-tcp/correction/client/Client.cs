using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

class EchoClient
{
    static async Task Main()
    {
        using var client = new TcpClient();
        await client.ConnectAsync("127.0.0.1", 5000);
        using NetworkStream stream = client.GetStream();

        byte[] payload = Encoding.UTF8.GetBytes("Hello, server!");
        await stream.WriteAsync(BitConverter.GetBytes(payload.Length));
        await stream.WriteAsync(payload);

        byte[] lengthBuffer = new byte[4];
        await stream.ReadExactlyAsync(lengthBuffer);
        int length = BitConverter.ToInt32(lengthBuffer);
        byte[] messageBuffer = new byte[length];
        await stream.ReadExactlyAsync(messageBuffer);
        Console.WriteLine(Encoding.UTF8.GetString(messageBuffer));
    }
}

// TODO 4 answer: each new HttpClient starts a fresh connection pool, so
// its first request must redo DNS resolution, the TCP three-way
// handshake, AND the TLS handshake before a single byte of the actual
// HTTP request goes out. A shared HttpClient reuses an already-open,
// already-TLS-negotiated connection for subsequent requests to the same
// host — skipping straight to step 4 (send request, get response) —
// which is why reuse is dramatically faster under repeated calls,
// independent of anything to do with async or threading.
