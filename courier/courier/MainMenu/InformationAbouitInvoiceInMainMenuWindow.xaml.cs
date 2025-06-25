/*
    InformationAbouitInvoiceInMainMenuWindow.xaml
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace courier.MainMenu
{
    public partial class InformationAbouitInvoiceInMainMenuWindow : Window
    {
        public InformationAbouitInvoiceInMainMenuWindow()
        {
            InitializeComponent();
        }

        public bool SetAllData(Invoice invoice)
        {
            if (!string.IsNullOrWhiteSpace(invoice.ShippingData))
                fullNameSenderLabel.Content = invoice.ShippingData;

            if (!string.IsNullOrWhiteSpace(invoice.OwnerPhoneNumber))
                phoneNumberRecieverLabel.Content = invoice.OwnerPhoneNumber;

            if (!string.IsNullOrWhiteSpace(invoice.Payer))
                shipmentDescriptionLabel.Content = invoice.ShipmentsDescription;

            string fullName = string.Join(" ", new[] {
                invoice.RecipientData?.FirstName,
                invoice.RecipientData?.LastName,
                invoice.RecipientData?.MiddleName
                }.Where(s => !string.IsNullOrWhiteSpace(s)));

            if (!string.IsNullOrWhiteSpace(fullName))
                fullNameRecieverLabel.Content = fullName;

            if (!string.IsNullOrWhiteSpace(invoice.RecipientData?.Phone))
                phoneNumberRecieverLabel.Content = invoice.RecipientData.Phone;

            if (!string.IsNullOrWhiteSpace(invoice.Price))
                deliveryPricaLabel.Content = invoice.Price;

            if (!string.IsNullOrWhiteSpace(invoice.Payer))
                deliveryPayer.Content = invoice.Payer;

            if (!string.IsNullOrWhiteSpace(invoice.PaymentMethod))
                deliveryPaymentFormLabel.Content = invoice.PaymentMethod;

            return true;
        }

        private void ReturnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
