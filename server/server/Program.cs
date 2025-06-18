using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

class Server
{
    static async Task Main()
    {
        TcpListener server = new TcpListener(IPAddress.Any, 5050);
        server.Start();
        Console.WriteLine("[SERVER] started, waiting for clients");

        while (true)
        {
            TcpClient client = await server.AcceptTcpClientAsync();
            _ = HandleClient(client);
        }
    }

    static async Task HandleClient(TcpClient client)
    {
        NetworkStream stream = client.GetStream();

        try
        {
            byte[] buffer = new byte[4096];
            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            string json = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            using (JsonDocument doc = JsonDocument.Parse(json))
            {
                if (!doc.RootElement.TryGetProperty("status", out JsonElement statusElem))
                {
                    /* No status field in JSON */
                    await SendResponse(stream, "false");
                    client.Close();
                    return;
                }

                string status = statusElem.GetString();

                if (status == "client")
                {
                    User user = JsonSerializer.Deserialize<User>(json);

                    await SendResponse(stream, "true");
                }
                else if (status == "courier")
                {
                    await SendResponse(stream, "true");
                }
                else if (status == "package")
                {
                    await SendResponse(stream, "true");
                }
                else
                {
                    await SendResponse(stream, "false");
                }
            }

            client.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[SERVER][ERROR] {ex.Message}");
            await SendResponse(stream, "false");
        }
    }

    static async Task SendResponse(NetworkStream stream, string message)
    {
        byte[] responseBytes = Encoding.UTF8.GetBytes(message);
        await stream.WriteAsync(responseBytes, 0, responseBytes.Length);
    }
}
