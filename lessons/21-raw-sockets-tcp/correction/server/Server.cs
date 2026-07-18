using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

class EchoServer
{
    static async Task Main()
    {
        var listener = new TcpListener(IPAddress.Any, 5000);
        listener.Start();
        Console.WriteLine("Listening on port 5000...");
        while (true)
        {
            using TcpClient client = await listener.AcceptTcpClientAsync();
            using NetworkStream stream = client.GetStream();

            // Length-prefixed read
            byte[] lengthBuffer = new byte[4];
            await stream.ReadExactlyAsync(lengthBuffer);
            int length = BitConverter.ToInt32(lengthBuffer);
            byte[] messageBuffer = new byte[length];
            await stream.ReadExactlyAsync(messageBuffer);
            string message = Encoding.UTF8.GetString(messageBuffer);
            Console.WriteLine($"Received: {message}");

            // Length-prefixed write
            byte[] payload = Encoding.UTF8.GetBytes($"Echo: {message}");
            await stream.WriteAsync(BitConverter.GetBytes(payload.Length));
            await stream.WriteAsync(payload);
        }
    }
}
