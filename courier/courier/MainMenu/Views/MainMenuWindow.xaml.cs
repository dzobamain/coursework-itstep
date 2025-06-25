/*
    MainMenuWindow.xaml.cs
*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
            JsonHandler jsonHandler = new JsonHandler();
            listInvoice = jsonHandler.ReadInvoiceFromJson(path);

            SetAllInvoiceDisplayItem(listInvoice);
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

            JsonHandler jsonHandler = new JsonHandler();
            string savePath = new CourierDataPath().GetAcceptedInvoicePath();

            bool success = jsonHandler.WriteInvoicesToJson(savePath, selectedInvoices);

            MessageBox.Show(success ? "Вибрані накладні збережено" : "Помилка під час збереження накладних");
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
