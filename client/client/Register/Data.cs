using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace client.Register
{
    class Data
    {
        private readonly string dataPath; /* Path to the user data file */
        private User user;

        public Data()
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

        public bool ValidateUserData()
        {
            try
            {
                /* Check if any name fields are empty */
                if (string.IsNullOrWhiteSpace(user.firstName) ||
                    string.IsNullOrWhiteSpace(user.lastName) ||
                    string.IsNullOrWhiteSpace(user.middleName))
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
        } /* <-- Додана закриваюча фігурна дужка тут */

        public bool WriteSingleUserToJson(User newUser, string filePath)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath)); /* Ensure directory exists */

                /* Serialize the User object (without a list) */
                string json = JsonConvert.SerializeObject(newUser, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(filePath, json);

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
    }
}
