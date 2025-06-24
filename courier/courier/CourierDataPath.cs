using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courier
{
    public class CourierDataPath
    {
        private string courierData;
        private string invoiceData;

        public CourierDataPath()
        {
            courierData = SetCourierDataPath();
            invoiceData = SetInvoiceDataPath();
        }

        private static string SetCourierDataPath()
        {
            if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows))
            {
                return @"courier\cdata.json"; /* Windows */
            }
            else
            {
                return "courier/cdata.json"; /* Linux/macOS */
            }
        }

        private static string SetInvoiceDataPath()
        {
            if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows))
            {
                return @"invoice\invoices.json"; /* Windows */
            }
            else
            {
                return "invoice/invoices.json"; /* Linux/macOS */
            }
        }

        public string GetCourierDataPath()
        {
            return courierData;
        }

        public string GetInvoicePath()
        {
            return invoiceData;
        }
    }

}
