using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client
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
                string? directory = Path.GetDirectoryName(jsonPath);
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
