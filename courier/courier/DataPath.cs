﻿/*
 * DataPath.cs
 */
using System.Runtime.InteropServices;

namespace Courier.Data.Path
{
    public class DataPath
    {
        private readonly string courierData;
        private readonly string invoiceData;
        private readonly string acceptedInvoiceData;

        public DataPath()
        {
            courierData = SetCourierDataPath();
            invoiceData = SetInvoiceDataPath();
            acceptedInvoiceData = SetAcceptedInvoicePath();
        }

        private static string SetCourierDataPath()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? @"courier\cdata.json"
                : "courier/cdata.json";
        }

        private static string SetInvoiceDataPath()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? @"invoice\invoices.json"
                : "invoice/invoices.json";
        }

        private static string SetAcceptedInvoicePath()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? @"invoice\accepted_invoices.json"
                : "invoice/accepted_invoices.json";
        }

        public string GetCourierDataPath() => courierData;
        public string GetInvoicePath() => invoiceData;
        public string GetAcceptedInvoicePath() => acceptedInvoiceData;
    }
}
