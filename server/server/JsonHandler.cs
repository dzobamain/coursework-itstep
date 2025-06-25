using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server
{
    class JsonHandler
    {
        public List<User> ReadUsersFromJson(string jsonPath)
        {
            try
            {
                if (!File.Exists(jsonPath))
                {
                    return new List<User>();
                }

                string json = File.ReadAllText(jsonPath);
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

        public List<Courier> ReadCouriersFromJson(string jsonPath)
        {
            try
            {
                if (!File.Exists(jsonPath))
                {
                    return new List<Courier>();
                }

                string json = File.ReadAllText(jsonPath);
                if (string.IsNullOrWhiteSpace(json))
                {
                    return new List<Courier>();
                }

                return JsonConvert.DeserializeObject<List<Courier>>(json) ?? new List<Courier>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error UserVerifier.ReadUsersFromJson(): {ex}");
                return new List<Courier>();
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
                Console.WriteLine($"Error UserVerifier.ReadUserFromJson(): {ex}");
                return new User();
            }
        }

        public bool WriteNewUserToJson(string jsonPath, User newUser)
        {
            try
            {
                string? directory = Path.GetDirectoryName(jsonPath);
                if (!string.IsNullOrWhiteSpace(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                List<User> allUsers = new List<User>();

                if (File.Exists(jsonPath))
                {
                    string json = File.ReadAllText(jsonPath);
                    if (!string.IsNullOrWhiteSpace(json))
                    {
                        allUsers = JsonConvert.DeserializeObject<List<User>>(json) ?? new List<User>();
                    }
                }

                allUsers.Add(newUser);

                string updatedJson = JsonConvert.SerializeObject(allUsers, Formatting.Indented);
                File.WriteAllText(jsonPath, updatedJson);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error UserVerifier.WriteNewUserToJson(): {ex}");
                return false;
            }
        }

        public bool WriteNewInvoiceToJson(string path, Invoice newiInvoices)
        {
            try
            {
                string? directory = Path.GetDirectoryName(path);
                if (!string.IsNullOrWhiteSpace(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                List<Invoice> allUsers = new List<Invoice>();

                if (File.Exists(path))
                {
                    string json = File.ReadAllText(path);
                    if (!string.IsNullOrWhiteSpace(json))
                    {
                        allUsers = JsonConvert.DeserializeObject<List<Invoice>>(json) ?? new List<Invoice>();
                    }
                }

                allUsers.Add(newiInvoices);

                string updatedJson = JsonConvert.SerializeObject(allUsers, Formatting.Indented);
                File.WriteAllText(path, updatedJson);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error JsonHandler.WriteNewInvoiceToJson(): {ex}");
                return false;
            }
        }
    }
}
