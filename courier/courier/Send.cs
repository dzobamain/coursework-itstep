/*
 * Send.cs
 */
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Network
{
    class Send
    {
        private const int port = 5050;
        private const string server = "127.0.0.1";

        public static async Task<string> SendJsonAndReceiveAllAsync(string jsonFilePath)
        {
            try
            {
                string json = await File.ReadAllTextAsync(jsonFilePath);
                Console.WriteLine("Send.SendJsonAndReceiveAllAsync(): JSON before sending:\n" + json);

                using TcpClient client = new TcpClient();
                await client.ConnectAsync(server, port);

                using NetworkStream stream = client.GetStream();

                /* Send JSON */
                byte[] jsonBytes = Encoding.UTF8.GetBytes(json);
                await stream.WriteAsync(jsonBytes, 0, jsonBytes.Length);
                Console.WriteLine("Send.SendJsonAndReceiveAllAsync(): JSON sent. Waiting for response...");

                /* Read plain text response */
                byte[] buffer = new byte[256];
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                string textResponse = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine("Send.SendJsonAndReceiveAllAsync(): Text response: " + textResponse);

                if (textResponse.Trim() == "true")
                {
                    /* Read expected JSON length */
                    byte[] lengthBytes = new byte[4];
                    int readLength = await stream.ReadAsync(lengthBytes, 0, 4);
                    if (readLength < 4)
                    {
                        Console.WriteLine("Send.SendJsonAndReceiveAllAsync(): Failed to read JSON length.");
                        return textResponse;
                    }

                    int jsonLength = BitConverter.ToInt32(lengthBytes, 0);
                    Console.WriteLine($"Send.SendJsonAndReceiveAllAsync(): Expected JSON length: {jsonLength} bytes");

                    /* Read JSON by exact length */
                    byte[] jsonBuffer = new byte[jsonLength];
                    int totalRead = 0;
                    while (totalRead < jsonLength)
                    {
                        int read = await stream.ReadAsync(jsonBuffer, totalRead, jsonLength - totalRead);
                        if (read == 0) break;
                        totalRead += read;
                    }

                    if (totalRead == jsonLength)
                    {
                        string jsonResponse = Encoding.UTF8.GetString(jsonBuffer);
                        Console.WriteLine("Send.SendJsonAndReceiveAllAsync(): Full JSON response:\n" + jsonResponse);

                        var dataPath = new Data.Path.DataPath();
                        string invoicePath = dataPath.GetInvoicePath();

                        string directory = Path.GetDirectoryName(invoicePath);
                        if (!Directory.Exists(directory))
                        {
                            Directory.CreateDirectory(directory);
                            Console.WriteLine("Send.SendJsonAndReceiveAllAsync(): Created directory: " + directory);
                        }

                        await File.WriteAllTextAsync(invoicePath, jsonResponse);
                        Console.WriteLine("Send.SendJsonAndReceiveAllAsync(): JSON saved to file: " + invoicePath);
                    }
                    else
                    {
                        Console.WriteLine("Send.SendJsonAndReceiveAllAsync(): Incomplete JSON received.");
                    }
                }

                return textResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Send.SendJsonAndReceiveAllAsync(): {ex.Message}");
                return "false";
            }
        }
    }
}
