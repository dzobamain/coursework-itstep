/*
 MainMenuWindow.xaml.cs
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Collections.ObjectModel;

namespace client.MainMenu.Views
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

            DataPath dataPath = new DataPath();
            JsonHandler jsonHandler = new JsonHandler();
            listInvoice = jsonHandler.ReadInvoiceFromJson(dataPath.GetInvoicePath());

            SetAllInvoiceDisplayItem(listInvoice);

            allInvoicesListBox.ItemsSource = invoices;
        }

        private void SetAllInvoiceDisplayItem(List<Invoice> listInvoice)
        {
            foreach (var invoice in listInvoice)
            {
                invoices.Add(new InvoiceDisplayItem
                {
                    Description = invoice.ShipmentsDescription,
                    Progress = invoice.progress
                });
            }
        }

        private void CreateInvoiceButton_Click(object sender, RoutedEventArgs e)
        {
            _main.CreateNewInvoinceInfoAboutSW();
        }

        private void ShowInformationAboutInvoice_Click(object sender, RoutedEventArgs e)
        {
            if(allInvoicesListBox.SelectedIndex != -1)
            {
                InformationAboutInvoiceWindow informationAboutInvoiceWindow = new InformationAboutInvoiceWindow();

                if (!informationAboutInvoiceWindow.ShowTheWindow(listInvoice[allInvoicesListBox.SelectedIndex]))
                {
                    return;
                }

                informationAboutInvoiceWindow.ShowDialog();
            }
        }
    }
}
