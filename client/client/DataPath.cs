/*
 * DataPAth.cs
 */
using System;

namespace Client.Data.Path
{
    public class DataPath
    {
        private string userData;
        private string invoiceData;

        public DataPath()
        {
            userData = GetUserDataPath();
            invoiceData = GetInvoiceDataPath();
        }

        private static string GetUserDataPath()
        {
            if (OperatingSystem.IsWindows())
            {
                return @"user\userdata.json"; /* Windows */
            }
            else
            {
                return "user/userdata.json"; /* Linux/macOS */
            }
        }

        private static string GetInvoiceDataPath()
        {
            if (OperatingSystem.IsWindows())
            {
                return @"invoice\invoicedata.json"; /* Windows */
            }
            else
            {
                return "invoice/invoicedata.json"; /* Linux/macOS */
            }
        }

        public string GetUserPath()
        {
            return userData;
        }

        public string GetInvoicePath()
        {
            return invoiceData;
        }
    }
}
