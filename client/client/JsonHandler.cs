/*
 * JsonHandler.cs
 */
using System;
using Newtonsoft.Json;
using System.IO;
using Client.Models;

namespace Client.Data.Json
{
    class JsonHandler
    {
        public List<Invoice> ReadInvoiceFromJson(string jsonPath)
        {
            try
            {
                if (!File.Exists(jsonPath))
                {
                    return new List<Invoice>();
                }

                string json = File.ReadAllText(jsonPath);
                if (string.IsNullOrWhiteSpace(json))
                {
                    return new List<Invoice>();
                }

                return JsonConvert.DeserializeObject<List<Invoice>>(json) ?? new List<Invoice>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error JsonHandler.ReadInvoiceFromJson(): {ex}");
                return new List<Invoice>();
            }
        }

        public bool WriteNewInvoiceToJson(string jsonPath, Invoice invoice)
        {
            try
            {
                string? directory = System.IO.Path.GetDirectoryName(jsonPath);
                if (!string.IsNullOrWhiteSpace(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                List<Invoice> allInvoice = new List<Invoice>();

                if (File.Exists(jsonPath))
                {
                    string json = File.ReadAllText(jsonPath);
                    if (!string.IsNullOrWhiteSpace(json))
                    {
                        allInvoice = JsonConvert.DeserializeObject<List<Invoice>>(json) ?? new List<Invoice>();
                    }
                }

                allInvoice.Add(invoice);

                string updatedJson = JsonConvert.SerializeObject(allInvoice, Formatting.Indented);
                File.WriteAllText(jsonPath, updatedJson);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error UserVerifier.WriteNewUserToJson(): {ex}");
                return false;
            }
        }
    }
}
