using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

public class UserVerifier
{
    private readonly string dataPath; /* Path to the user data file */
    private User user;

    public UserVerifier()
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
    public void SetUser(User newUser)
    {
        user = newUser;
    }
    public User GetUser()
    {
        return user;
    }

    public bool ValidateUserData()
    {
        try
        {
            /* Check if any name fields are empty */
            if (string.IsNullOrWhiteSpace(user.firstName) ||
                string.IsNullOrWhiteSpace(user.lastName))
            {
                return false;
            }

            /* Check phone number (must contain only digits and '+') */
            if (!Regex.IsMatch(user.phoneNumber, @"^\+?\d+$"))
            {
                return false;
            }

            /* Check password (must be at least 8 characters long) */
            if (user.password.Length < 8)
            {
                return false;
            }

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error UserVerifier.ValidateUserData(): {ex}");
            return false;
        }
    }

    public bool WriteNewUserToJson(User newUser)
    {
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(dataPath)); /* Ensure directory exists */

            List<User> allUsers = new List<User>();

            if (File.Exists(dataPath))
            {
                string json = File.ReadAllText(dataPath);

                if (!string.IsNullOrWhiteSpace(json))
                {
                    allUsers = JsonConvert.DeserializeObject<List<User>>(json) ?? new List<User>();
                }
            }

            allUsers.Add(newUser);

            string updatedJson = JsonConvert.SerializeObject(allUsers, Formatting.Indented);
            File.WriteAllText(dataPath, updatedJson);

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error UserVerifier.WriteNewUserToJson(): {ex}");
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

    public List<User> ReadUsersFromJson()
    {
        try
        {
            if (!File.Exists(dataPath))
            {
                return new List<User>();
            }

            string json = File.ReadAllText(dataPath);
            if (string.IsNullOrWhiteSpace(json))
            {
                return new List<User>();
            }

            return JsonConvert.DeserializeObject<List<User>>(json) ?? new List<User>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error UserVerifier.ReadUsersFromJson(): {ex}");
            return new List<User>();
        }
    }

    public bool FindUserInList(List<User> allUsers, User inputUser)
    {
        /* Search for a user in the list by phone number and password */
        return allUsers.Exists(u =>
            u.password.Equals(inputUser.password) &&
            u.phoneNumber.Equals(inputUser.phoneNumber));
    }
}
