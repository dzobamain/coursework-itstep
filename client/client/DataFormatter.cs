/*
 * DataFormatter.cs
 */
using System;
using System.IO;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

using Client.Models;

namespace Client.Validation
{
    class DataFormatter
    {
        public bool ValidateUserData(User user)
        {
            if (user.regOrLog != "reg" && user.regOrLog != "log")
            {
                return false;
            }

            if (user.regOrLog == "reg")
            {
                if (string.IsNullOrWhiteSpace(user.firstName) ||
                    string.IsNullOrWhiteSpace(user.lastName))
                {
                    return false;
                }
            }

            if (string.IsNullOrWhiteSpace(user.phoneNumber) ||
                string.IsNullOrWhiteSpace(user.password))
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

        public bool WriteUserToJson(string writeTo, User user)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(writeTo)); /* Ensure directory exists */

                /* Serialize the User object (without a list) */
                string json = JsonConvert.SerializeObject(user, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(writeTo, json);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Send.WriteSingleUserToJson(): {ex}");
                return false;
            }
        }

        public User ReadUserFromJson(string filePath)
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
    }
}
