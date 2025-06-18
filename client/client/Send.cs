using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace client.Register
{
    class Send
    {
        private const int port = 5050;
        private const string server = "127.0.0.1";

        public static async Task<string> SendJsonAsync(string jsonFilePath)
        {
            const int port = 5050;
            const string serverIp = "127.0.0.1";

            try
            {
                string json = await File.ReadAllTextAsync(jsonFilePath);
                Console.WriteLine("[CLIENT] JSON до відправки:\n" + json);

                using TcpClient client = new TcpClient();
                await client.ConnectAsync(serverIp, port);

                using NetworkStream stream = client.GetStream();

                // Відправка JSON
                byte[] jsonBytes = Encoding.UTF8.GetBytes(json);
                await stream.WriteAsync(jsonBytes, 0, jsonBytes.Length);
                Console.WriteLine("[CLIENT] JSON відправлено. Чекаємо на відповідь...");

                // Очікування відповіді
                byte[] buffer = new byte[256];
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                Console.WriteLine("[CLIENT] Отримано відповідь: " + response);
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine("[CLIENT][ERROR] " + ex.Message);
                return "false";
            }
        }


        public static async Task<string> ReceiveMessageAsync()
        {
            try
            {
                using TcpClient client = new TcpClient();
                await client.ConnectAsync(server, port);
                NetworkStream stream = client.GetStream();

                byte[] buffer = new byte[4096];
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                return Encoding.UTF8.GetString(buffer, 0, bytesRead);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Send.ReceiveMessageAsync(): " + e.Message);
                return "false";
            }
        }
    }
}
