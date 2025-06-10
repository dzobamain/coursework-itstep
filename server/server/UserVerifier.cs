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

    public bool ValidateUserData(in User user)
    {
        // Name
        try {
            if (string.IsNullOrWhiteSpace(user.firstName) ||
                string.IsNullOrWhiteSpace(user.lastName) ||
                string.IsNullOrWhiteSpace(user.middleName)) {
                return false;
            }

            // Phone number
            if (!System.Text.RegularExpressions.Regex.IsMatch(user.phoneNumber, @"^\+?\d+$")) {
                return false;
            }

            // Password
            if (user.password.Length < 8) {
                return false;
            }

            return true;
        }
        catch (Exception ex) {
            Console.WriteLine("Error ValidateUserData: " + ex.ToString());
            return false;
        }
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
            Console.WriteLine("Error UserVerifier.WriteUserToJson(): " + ex.ToString());
            return false;
        }
    }

    public bool FindUserInJson(User inputUser)
    {
        try {
            if (!File.Exists(dataPath)) {
                return false;
            }

            string json = File.ReadAllText(dataPath);
            if (string.IsNullOrWhiteSpace(json)) {
                return false;
            }

            List<User> allUsers = JsonConvert.DeserializeObject<List<User>>(json) ?? new List<User>();

            return allUsers.Exists(u =>
                u.firstName.Equals(inputUser.firstName, StringComparison.OrdinalIgnoreCase) &&
                u.lastName.Equals(inputUser.lastName, StringComparison.OrdinalIgnoreCase) &&
                u.middleName.Equals(inputUser.middleName, StringComparison.OrdinalIgnoreCase) &&
                u.password.Equals(inputUser.password));
        }
        catch (Exception ex) {
            Console.WriteLine("Error UserVerifier.FindUserInJson(): " + ex.ToString());
            return false;
        }
    }
}
