using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Server
{
    class Server
    {
        static async Task Main()
        {
            TcpListener server = new TcpListener(IPAddress.Any, 5050);
            server.Start();
            Console.WriteLine("[SERVER] Started, waiting for <>.");

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

                    string response = "false";
                    Logic.Validation.DataFormatter dataFormatter = new();

                    switch (status)
                    {
                        case "client":
                            Console.WriteLine("[SERVER] Client connected.");
                            Models.User user = JsonSerializer.Deserialize<Models.User>(json);
                            response = await ProcessClientRequest(user);

                            await SendResponse(stream, response);
                            break;

                        case "courier":
                            Console.WriteLine("[SERVER] Courier connected.");
                            Models.Courier сourier = JsonSerializer.Deserialize<Models.Courier>(json);
                            response = await ProcessCourierRequest(сourier);

                            response = "true"; // ---TEMP---
                            await SendResponse(stream, response);

                            Data.Path.DataPath dataPath = new();
                            await SendJsonFile(stream, dataPath.GetInvoicePath());
                            break;

                        case "package":
                            Console.WriteLine("[SERVER] Package connected.");
                            Models.Invoice invoice = JsonSerializer.Deserialize<Models.Invoice>(json);
                            await ProcessInvoiceRequest(invoice);
                            break;

                        default:
                            Console.WriteLine("[SERVER] Invalid status value.");
                            break;
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

        static async Task<string> ProcessClientRequest(Models.User user)
        {
            string response = "false";
            Logic.Validation.DataFormatter dataFormatter = new();

            if (!dataFormatter.ValidateUserData(user))
            {
                return response;
            }

            Data.Json.JsonHandler jsonHandler = new();
            Data.Path.DataPath usersDataPath = new();

            if (user.regOrLog == "log")
            {
                List<Models.User> users = jsonHandler.ReadUsersFromJson(usersDataPath.GetUsersPath());
                if (dataFormatter.FindUserInList(users, user))
                {
                    response = "true";
                    return response;
                }
            }
            if (user.regOrLog == "reg")
            {
                bool result = jsonHandler.WriteNewUserToJson(usersDataPath.GetUsersPath(), user);
                response = result.ToString().ToLower();
                return response;
            }

            return response;
        }

        static async Task<string> ProcessCourierRequest(Models.Courier сourier)
        {
            string response = "false";
            Logic.Validation.DataFormatter dataFormatter = new();

            if (!dataFormatter.ValidateCourierData(сourier))
            {
                return response;
            }

            Data.Json.JsonHandler jsonHandler = new();
            Data.Path.DataPath dataPath = new();

            List<Models.Courier> сouriers = jsonHandler.ReadCouriersFromJson(dataPath.GetCouriersPath());
            if (dataFormatter.FindCourierInList(сouriers, сourier))
            {
                response = "true";
                return response;
            }

            return response;
        }

        static async Task ProcessInvoiceRequest(Models.Invoice invoice)
        {
            Data.Json.JsonHandler jsonHandler = new();
        }

        static async Task SendResponse(NetworkStream stream, string message)
        {
            Console.WriteLine($"[SERVER] Send response.");
            byte[] responseBytes = Encoding.UTF8.GetBytes(message);
            // Send true||false
            await stream.WriteAsync(responseBytes, 0, responseBytes.Length);
        }
        static async Task SendJsonFile(NetworkStream stream, string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Error Server.SendJsonFile(): Json  file, not fond.", filePath);

            Console.WriteLine($"[SERVER] Send file.");

            string jsonContent = await File.ReadAllTextAsync(filePath);
            byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonContent);
            int length = jsonBytes.Length;

            // Length
            byte[] lengthBytes = BitConverter.GetBytes(length);
            await stream.WriteAsync(lengthBytes, 0, lengthBytes.Length);

            // JSON
            await stream.WriteAsync(jsonBytes, 0, jsonBytes.Length);
        }
    }
}
