/*
 * JsonHandler.cs
 */
using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using Courier.Models;

namespace Courier.Data.Json
{
    class JsonHandler
    {
        public List<CourierData> ReadCouriersFromJson(string jsonPath)
        {
            try
            {
                if (!File.Exists(jsonPath))
                {
                    return new List<CourierData>();
                }

                string json = File.ReadAllText(jsonPath);
                if (string.IsNullOrWhiteSpace(json))
                {
                    return new List<CourierData>();
                }

                return JsonConvert.DeserializeObject<List<CourierData>>(json) ?? new List<CourierData>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error JsonHandler.ReadUsersFromJson(): {ex}");
                return new List<CourierData>();
            }
        }

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

        public bool WriteInvoicesToJson(string path, List<Invoice> invoices)
        {
            try
            {
                string? directory = System.IO.Path.GetDirectoryName(path);
                if (!string.IsNullOrWhiteSpace(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                string json = JsonConvert.SerializeObject(invoices, Formatting.Indented);
                File.WriteAllText(path, json);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error JsonHandler.WriteInvoicesToJson(): {ex.Message}");
                return false;
            }
        }

        public bool WriteNewInvoiceToJson(string path, Invoice newiInvoices)
        {
            try
            {
                string? directory = System.IO.Path.GetDirectoryName(path);
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
