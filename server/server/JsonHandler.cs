using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Server.Data.Json
{
    class JsonHandler
    {
        public List<Models.User> ReadUsersFromJson(string jsonPath)
        {
            try
            {
                if (!File.Exists(jsonPath))
                {
                    return new List<Models.User>();
                }

                string json = File.ReadAllText(jsonPath);
                if (string.IsNullOrWhiteSpace(json))
                {
                    return new List<Models.User>();
                }

                return JsonConvert.DeserializeObject<List<Models.User>>(json) ?? new List<Models.User>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error UserVerifier.ReadUsersFromJson(): {ex}");
                return new List<Models.User>();
            }
        }

        public List<Models.Courier> ReadCouriersFromJson(string jsonPath)
        {
            try
            {
                if (!File.Exists(jsonPath))
                {
                    return new List<Models.Courier>();
                }

                string json = File.ReadAllText(jsonPath);
                if (string.IsNullOrWhiteSpace(json))
                {
                    return new List<Models.Courier>();
                }

                return JsonConvert.DeserializeObject<List<Models.Courier>>(json) ?? new List<Models.Courier>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error UserVerifier.ReadUsersFromJson(): {ex}");
                return new List<Models.Courier>();
            }
        }

        public Models.User ReadUserFromJson(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    return new Models.User(); /* Return an empty User if the file does not exist */
                }

                string json = File.ReadAllText(filePath);
                if (string.IsNullOrWhiteSpace(json))
                {
                    return new Models.User(); /* Return an empty User if the file is empty */
                }

                /* Deserialize the JSON object directly */
                return JsonConvert.DeserializeObject<Models.User>(json) ?? new Models.User();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error UserVerifier.ReadUserFromJson(): {ex}");
                return new Models.User();
            }
        }

        public bool WriteNewUserToJson(string jsonPath, Models.User newUser)
        {
            try
            {
                string? directory = System.IO.Path.GetDirectoryName(jsonPath);
                if (!string.IsNullOrWhiteSpace(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                List<Models.User> allUsers = new List<Models.User>();

                if (File.Exists(jsonPath))
                {
                    string json = File.ReadAllText(jsonPath);
                    if (!string.IsNullOrWhiteSpace(json))
                    {
                        allUsers = JsonConvert.DeserializeObject<List<Models.User>>(json) ?? new List<Models.User>();
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

        public bool WriteNewInvoiceToJson(string path, Models.Invoice newiInvoices)
        {
            try
            {
                string? directory = System.IO.Path.GetDirectoryName(path);
                if (!string.IsNullOrWhiteSpace(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                List<Models.Invoice> allUsers = new List<Models.Invoice>();

                if (File.Exists(path))
                {
                    string json = File.ReadAllText(path);
                    if (!string.IsNullOrWhiteSpace(json))
                    {
                        allUsers = JsonConvert.DeserializeObject<List<Models.Invoice>>(json) ?? new List<Models.Invoice>();
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
