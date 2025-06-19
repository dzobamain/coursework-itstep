using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;

namespace courier
{
    class DataFormatter
    {
        public bool ValidateUserData(Courier courier)
        {
            if (string.IsNullOrWhiteSpace(courier.phoneNumber) ||
                string.IsNullOrWhiteSpace(courier.password))
            {
                return false;
            }

            /* Check phone number (must contain only digits and '+') */
            if (!Regex.IsMatch(courier.phoneNumber, @"^\+?\d+$"))
            {
                return false;
            }

            /* Check password (must be at least 8 characters long) */
            if (courier.password.Length < 8)
            {
                return false;
            }

            return true;
        }

        public bool WriteUserToJson(string writeTo, Courier courier)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(writeTo));

                string json = JsonConvert.SerializeObject(courier, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(writeTo, json);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Send.WriteSingleUserToJson(): {ex}");
                return false;
            }
        }

        public Courier ReadUserFromJson(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    return new Courier();
                }

                string json = File.ReadAllText(filePath);
                if (string.IsNullOrWhiteSpace(json))
                {
                    return new Courier();
                }

                /* Deserialize the JSON object directly */
                return JsonConvert.DeserializeObject<Courier>(json) ?? new Courier();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error UserVerifier.ReadSingleUserFromJson(): {ex}");
                return new Courier();
            }
        }
    }
}
