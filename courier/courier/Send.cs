using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace courier
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
                Console.WriteLine("[COURIER] JSON до відправки:\n" + json);

                using TcpClient client = new TcpClient();
                await client.ConnectAsync(server, port);

                using NetworkStream stream = client.GetStream();

                // Відправка JSON
                byte[] jsonBytes = Encoding.UTF8.GetBytes(json);
                await stream.WriteAsync(jsonBytes, 0, jsonBytes.Length);
                Console.WriteLine("[COURIER] JSON відправлено. Чекаємо на відповідь...");

                // Читання текстової відповіді
                byte[] buffer = new byte[256];
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                string textResponse = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine("[COURIER] Текстова відповідь: " + textResponse);

                if (textResponse.Trim() == "true")
                {
                    // Зчитуємо довжину JSON-файлу (4 байти)
                    byte[] lengthBytes = new byte[4];
                    int readLength = await stream.ReadAsync(lengthBytes, 0, 4);
                    if (readLength < 4)
                    {
                        Console.WriteLine("[COURIER] Не вдалося зчитати довжину JSON.");
                        return textResponse;
                    }

                    int jsonLength = BitConverter.ToInt32(lengthBytes, 0);
                    Console.WriteLine($"[COURIER] Очікувана довжина JSON: {jsonLength} байт");

                    // Зчитуємо JSON точно по довжині
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
                        Console.WriteLine("[COURIER] JSON-відповідь:\n" + jsonResponse);

                        var dataPath = new CourierDataPath();
                        string invoicePath = dataPath.GetInvoicePath();

                        string directory = Path.GetDirectoryName(invoicePath);
                        if (!Directory.Exists(directory))
                        {
                            Directory.CreateDirectory(directory);
                            Console.WriteLine("[COURIER] Створено директорію: " + directory);
                        }

                        await File.WriteAllTextAsync(invoicePath, jsonResponse);
                        Console.WriteLine("[COURIER] JSON збережено у файл: " + invoicePath);
                    }
                    else
                    {
                        Console.WriteLine("[COURIER] Отримано неповний JSON.");
                    }
                }

                return textResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine("[CLIENT][ERROR] " + ex.Message);
                return "false";
            }
        }
    }
}
