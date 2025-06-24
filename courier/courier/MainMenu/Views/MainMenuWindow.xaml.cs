/*
    MainMenuWindow.xaml
 */

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace courier.MainMenu.Views
{
    public partial class MainMenuWindow : UserControl
    {
        private MainWindow _main;
        private ObservableCollection<InvoiceDisplayItem> invoices = new ObservableCollection<InvoiceDisplayItem>();
        List<Invoice> listInvoice;


        public MainMenuWindow(MainWindow main)
        {
            InitializeComponent();
            _main = main;

            string path = new CourierDataPath().GetInvoicePath();
            JsonHandler handler = new JsonHandler();

            listInvoice = handler.ReadInvoiceFromJson(path);
            SetAllInvoiceDisplayItem(listInvoice);
            allInvoicesListBox.ItemsSource = invoices;

        }

        private void AcceptedInvoiceButton_Click(object sender, RoutedEventArgs e)
        {
            _main.ShowAcceptedInvoicesWindow();
        }

        private void AllInvoiceListBox_Click(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void SetAllInvoiceDisplayItem(List<Invoice> allInvoices)
        {
            invoices.Clear();

            foreach (var invoice in allInvoices)
            {
                invoices.Add(new InvoiceDisplayItem
                {
                    Description = string.IsNullOrWhiteSpace(invoice.ShipmentsDescription) ? "Без опису" : invoice.ShipmentsDescription,
                    Progress = string.IsNullOrWhiteSpace(invoice.progress) ? "Невідомо" : invoice.progress
                });
            }
        }
    }
}
