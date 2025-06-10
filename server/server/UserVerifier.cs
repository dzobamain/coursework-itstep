using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class UserVerifier
{
    private readonly string dataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "user", "data.json");

    private User user;

    public void SetUser(in User newUser)
    {
        user = newUser;
    }

    public User GetUser()
    {
        return user;
    }

    public bool WriteUserToJson(in User newUser)
    {
        try {
            Directory.CreateDirectory(Path.GetDirectoryName(dataPath));

            List<User> allUsers = new List<User>();

            if (File.Exists(dataPath)) {
                string json = File.ReadAllText(dataPath);

                if (!string.IsNullOrWhiteSpace(json)) {
                    allUsers = JsonConvert.DeserializeObject<List<User>>(json) ?? new List<User>();
                }
            }

            allUsers.Add(newUser);

            string updatedJson = JsonConvert.SerializeObject(allUsers, Formatting.Indented);
            File.WriteAllText(dataPath, updatedJson);

            return true;
        }
        catch (Exception ex) {
            Console.WriteLine("Error in UserVerifier.WriteUserToJson(): " + ex.ToString());
            return false;
        }
    }
}
