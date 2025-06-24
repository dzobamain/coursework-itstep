using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courier
{
    class JsonHandler
    {
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
                Console.WriteLine($"Error JsonHandler.ReadUsersFromJson(): {ex}");
                return new List<Courier>();
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
                string? directory = Path.GetDirectoryName(path);
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
    }
}
