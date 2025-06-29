using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Data.Path
{
    public class DataPath
    {
        private string userData;
        private string couriserData;
        private string invoiceData;

        public DataPath()
        {
            userData = GetUserDataPath();
            couriserData = GetCourierDataPath();
            invoiceData = GetInvoiceDataPath();
        }

        private static string GetUserDataPath()
        {
            if (OperatingSystem.IsWindows()) /* Windows */
            {
                return @"users\regusers.json"; 
            }
            else /* Linux/macOS */
            {
                return "users/regusers.json"; 
            }
        }

        private static string GetCourierDataPath()
        {
            if (OperatingSystem.IsWindows()) /* Windows */
            {
                return @"couriers\regcouriers.json";
            }
            else /* Linux/macOS */
            {
                return "couriers/regcouriers.json";
            }
        }

        private static string GetInvoiceDataPath()
        {
            if (OperatingSystem.IsWindows()) /* Windows */
            {
                return @"invoice\invoices.json";
            }
            else /* Linux/macOS */
            {
                return "invoice/invoices.json";
            }
        }

        public string GetUsersPath()
        {
            return userData;
        }

        public string GetCouriersPath()
        {
            return couriserData;
        }

        public string GetInvoicePath()
        {
            return invoiceData;
        }
    }
}
