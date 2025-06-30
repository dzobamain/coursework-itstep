/*
 * DataFormatter.cs
 */
using System;
using System.IO;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

using Courier.Models;

namespace Courier
{
    class DataFormatter
    {
        public bool ValidateUserData(CourierData courier)
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

        public bool WriteUserToJson(string writeTo, CourierData courier)
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

        public CourierData ReadCourierFromJson(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    return new CourierData();
                }

                string json = File.ReadAllText(filePath);
                if (string.IsNullOrWhiteSpace(json))
                {
                    return new CourierData();
                }

                /* Deserialize the JSON object directly */
                return JsonConvert.DeserializeObject<CourierData>(json) ?? new CourierData();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error UserVerifier.ReadSingleUserFromJson(): {ex}");
                return new CourierData();
            }
        }
    }
}
