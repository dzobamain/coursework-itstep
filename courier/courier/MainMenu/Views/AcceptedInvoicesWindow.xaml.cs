/*
    AcceptedInvoicesWindow.xaml.cs
*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using courier;

namespace courier.MainMenu.Views
{
    public partial class AcceptedInvoicesWindow : UserControl
    {
        private MainWindow _main;
        private ObservableCollection<InvoiceDisplayItem> invoices = new ObservableCollection<InvoiceDisplayItem>();
        List<Invoice> listInvoice;

        public AcceptedInvoicesWindow(MainWindow main)
        {
            InitializeComponent();
            _main = main;

            string path = new CourierDataPath().GetAcceptedInvoicePath();
            string acceptedPath = new CourierDataPath().GetAcceptedInvoicePath();

            JsonHandler jsonHandler = new JsonHandler();
            listInvoice = jsonHandler.ReadInvoiceFromJson(path);

            SetAllInvoiceDisplayItem(listInvoice);
            allInvoicesListBox.ItemsSource = invoices;
        }


        private void IncomingInvoiceButton_Click(object sender, RoutedEventArgs e)
        {
            _main.ShowMainMenuWindow();
        }

        private void AllInvoiceListBox_Click(object sender, MouseButtonEventArgs e)
        {
            if (allInvoicesListBox.SelectedIndex != -1)
            {
                InformationAboutInvoiceInAcceptedWindow informationAboutInvoiceInAcceptedWindow = new();

                informationAboutInvoiceInAcceptedWindow.SetAllData(listInvoice[allInvoicesListBox.SelectedIndex]);

                informationAboutInvoiceInAcceptedWindow.ShowDialog();
            }
        }

        private void SetAllInvoiceDisplayItem(List<Invoice> allInvoices)
        {
            invoices.Clear();

            foreach (var invoice in allInvoices)
            {
                invoices.Add(new InvoiceDisplayItem
                {
                    Description = string.IsNullOrWhiteSpace(invoice.ShipmentsDescription) ? "Без опису" : invoice.ShipmentsDescription,
                    Progress = string.IsNullOrWhiteSpace(invoice.progress) ? "Невідомо" : invoice.progress,
                    IsSelected = false,
                    OriginalInvoice = invoice
                });
            }
        }
    }
}
