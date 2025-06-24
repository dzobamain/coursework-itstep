using client;
using server;
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
                DataFormatter dataFormatter = new DataFormatter();

                switch (status)
                {
                    case "client":
                        Console.WriteLine("[SERVER] Client connected.");
                        User user = JsonSerializer.Deserialize<User>(json);
                        response = await ProcessClientRequest(user);
                        break;

                    case "courier":
                        Console.WriteLine("[SERVER] Courier connected.");
                        Courier сourier = JsonSerializer.Deserialize<Courier>(json);
                        response = await ProcessCourierRequest(сourier);
                        break;

                    case "package":
                        Console.WriteLine("[SERVER] Package connected.");
                        response = "true";
                        break;

                    default:
                        Console.WriteLine("[SERVER] Invalid status value.");
                        break;
                }
                response = "true";
                await SendResponse(stream, response);
            }

            client.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[SERVER][ERROR] {ex.Message}");
            await SendResponse(stream, "false");
        }
    }

    static async Task<string> ProcessClientRequest(User user)
    {
        string response = "false";
        DataFormatter dataFormatter = new DataFormatter();

        if (!dataFormatter.ValidateUserData(user))
        {
            return response;
        }

        JsonHandler jsonHandler = new JsonHandler();
        DataPath usersDataPath = new DataPath();

        if (user.regOrLog == "log")
        {
            List<User> users = jsonHandler.ReadUsersFromJson(usersDataPath.GetUsersPath());
            if(dataFormatter.FindUserInList(users, user))
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

    static async Task<string> ProcessCourierRequest(Courier сourier)
    {
        string response = "false";
        DataFormatter dataFormatter = new DataFormatter();

        if (!dataFormatter.ValidateCourierData(сourier))
        {
            return response;
        }

        JsonHandler jsonHandler = new JsonHandler();
        DataPath dataPath = new DataPath();

        List<Courier> сouriers = jsonHandler.ReadCouriersFromJson(dataPath.GetCouriersPath());
        if (dataFormatter.FindCourierInList(сouriers, сourier))
        {
            response = "true";
            return response;
        }

        return response;
    }

    static async Task SendResponse(NetworkStream stream, string message)
    {
        byte[] responseBytes = Encoding.UTF8.GetBytes(message);
        await stream.WriteAsync(responseBytes, 0, responseBytes.Length);
    }
}
