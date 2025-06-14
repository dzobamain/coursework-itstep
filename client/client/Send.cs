using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace client.Register
{
    class Send
    {
        private readonly string dataPath; /* Path to the user data file */

        public Send()
        {
            dataPath = GetDataPath();
        }

        private static string GetDataPath()
        {
            if (OperatingSystem.IsWindows())
            {
                return @"user\data.json"; /* Windows */
            }
            else
            {
                return "user/data.json"; /* Linux/macOS */
            }
        }

        public bool WriteSingleUserToJson(User newUser)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(dataPath)); /* Ensure directory exists */

                /* Serialize the User object (without a list) */
                string json = JsonConvert.SerializeObject(newUser, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(dataPath, json);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error UserVerifier.WriteSingleUserToJson(): {ex}");
                return false;
            }
        }

        public User ReadSingleUserFromJson(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    return new User(); /* Return an empty User if the file does not exist */
                }

                string json = File.ReadAllText(filePath);
                if (string.IsNullOrWhiteSpace(json))
                {
                    return new User(); /* Return an empty User if the file is empty */
                }

                /* Deserialize the JSON object directly */
                return JsonConvert.DeserializeObject<User>(json) ?? new User();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error UserVerifier.ReadSingleUserFromJson(): {ex}");
                return new User();
            }
        }

        public bool SendJsonAndWaitResponse()
        {
            if (!SendJsonFile())
            {
                Console.WriteLine("Не вдалося відправити JSON.");
                return false;
            }

            Console.WriteLine("JSON успішно відправлено. Очікуємо відповідь...");
            return WaitForServerResponse();
        }

        private bool SendJsonFile()
        {
            try
            {
                using (TcpClient client = new TcpClient("server_address", 5050))
                using (NetworkStream stream = client.GetStream())
                using (FileStream fileStream = new FileStream(dataPath, FileMode.Open, FileAccess.Read))
                {
                    byte[] buffer = new byte[8192];
                    int bytesRead;

                    while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        stream.Write(buffer, 0, bytesRead);
                    }
                }

                Console.WriteLine("Error : JSON успішно відправлено!");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка SendJsonFile(): {ex.Message}");
                return false;
            }
        }


        private bool WaitForServerResponse()
        {
            try
            {
                using (TcpClient client = new TcpClient("server_address", 5050))
                using (NetworkStream stream = client.GetStream())
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    string response = reader.ReadLine();

                    return response == "true"; // Якщо сервер надіслав "true", повертаємо true
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Send.WaitForServerResponse(): {ex.Message}");
                return false;
            }
        }

    }
}
