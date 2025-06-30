/*
 * MainMenuWindow.xaml.cs
*/
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using Courier.Models;

namespace Courier.MainMenu.Views
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

            string path = new Data.Path.DataPath().GetInvoicePath();
            string acceptedPath = new Data.Path.DataPath().GetAcceptedInvoicePath();

            Data.Json.JsonHandler jsonHandler = new();

            listInvoice = jsonHandler.ReadInvoiceFromJson(path);

            List<Invoice> acceptedInvoices = jsonHandler.ReadInvoiceFromJson(acceptedPath);

            var filteredInvoices = listInvoice
                .Where(inv => !acceptedInvoices.Any(acc => acc.ShipmentsDescription == inv.ShipmentsDescription)) // порівняння по унікальному Id
                .ToList();

            SetAllInvoiceDisplayItem(filteredInvoices);
            allInvoicesListBox.ItemsSource = invoices;
        }


        private void AcceptedInvoiceButton_Click(object sender, RoutedEventArgs e)
        {
            _main.ShowAcceptedInvoicesWindow();
        }

        private void AllInvoiceListBox_Click(object sender, MouseButtonEventArgs e)
        {
            if (allInvoicesListBox.SelectedIndex != -1)
            {
                InformationAbouitInvoiceInMainMenuWindow informationAboutInvoiceWindow = new();

                if (!informationAboutInvoiceWindow.SetAllData(listInvoice[allInvoicesListBox.SelectedIndex]))
                {
                    return;
                }

                informationAboutInvoiceWindow.ShowDialog();
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

        private void SaveSelectedInvoices()
        {
            var selectedInvoices = invoices
                .Where(item => item.IsSelected)
                .Select(item => item.OriginalInvoice)
                .ToList();

            if (selectedInvoices.Count == 0)
            {
                MessageBox.Show("Виберіть накладні для збереження");
                return;
            }

            Data.Json.JsonHandler jsonHandler = new();
            string savePath = new Data.Path.DataPath().GetAcceptedInvoicePath();

            bool success = false;
            foreach (var invoice in selectedInvoices)
            {
                success = jsonHandler.WriteNewInvoiceToJson(savePath, invoice);
                if (!success)
                {
                    MessageBox.Show("Помилка під час збереження накладних");
                    return;
                }
            }

            if (success)
            {
                MessageBox.Show("Вибрані накладні збережено");

                var acceptedInvoices = jsonHandler.ReadInvoiceFromJson(savePath);

                var filteredInvoices = listInvoice
                    .Where(inv => !acceptedInvoices.Any(acc => acc.ShipmentsDescription == inv.ShipmentsDescription))
                    .ToList();

                listInvoice = filteredInvoices;

                invoices.Clear();
                SetAllInvoiceDisplayItem(filteredInvoices);
            }
        }


        private void SaveSelectedInvoicesButton_Click(object sender, RoutedEventArgs e)
        {
            SaveSelectedInvoices();
        }
    }

    public class InvoiceDisplayItem
    {
        public string Description { get; set; }
        public string Progress { get; set; }
        public bool IsSelected { get; set; }
        public Invoice OriginalInvoice { get; set; }
    }
}
