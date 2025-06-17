using Newtonsoft.Json;
using server;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Server
{
    static void Main()
    {
        string receivedData = "user/send/received.json";
        Directory.CreateDirectory(Path.GetDirectoryName(receivedData));

        User user;

        JsonHandler jsonHandler = new JsonHandler();
        user = jsonHandler.ReadSingleUserFromJson(receivedData);
    }
}
