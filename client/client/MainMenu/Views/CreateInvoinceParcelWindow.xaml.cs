/*
 * CreateInvoinceParcelWindow.xaml.cs
 */

using client.Register;
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

namespace client.MainMenu.Views
{
    public partial class CreateInvoinceParcelWindow : UserControl
    {
        MainWindow main;
        private Brush defaultBrush;

        public CreateInvoinceParcelWindow(MainWindow main)
        {
            InitializeComponent();
            this.main = main;
            defaultBrush = shipmentsDescriptionTextBox.BorderBrush;
        }

        private void returnBackButton_Click(object sender, RoutedEventArgs e)
        {
            main.ShowMainMenu();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            shipmentsDescriptionTextBox.Text = string.Empty;
            valueTextBox.Text = string.Empty;
            parcelSizeComboBox.SelectedIndex = -1;
            packingInBoxCheckBox.IsChecked = false;
        }

        private bool isFirstClick = true;
        private string previousDescription = string.Empty;
        private string previousSize = string.Empty;
        private string previousPayer = string.Empty;
        private string previousPayment = string.Empty;
        private bool previousBoxChecked = false;

        private async void CreateParcelButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(shipmentsDescriptionTextBox.Text) ||
                parcelSizeComboBox.SelectedItem == null ||
                (payerSenderRadioButton.IsChecked == false && payerRecieverRadioButton.IsChecked == false) ||
                (cashRadioButton.IsChecked == false && cardRadioButton.IsChecked == false))
            {
                isFirstClick = true;
                await MarkInvalidFields();
                return;
            }

            var selectedItem = parcelSizeComboBox.SelectedItem as ComboBoxItem;
            var selectedContent = selectedItem?.Content?.ToString();

            float value = selectedContent switch
            {
                "Документи" => 65f,
                "Мала(до 2кг)" => 80f,
                "Середня(до 10кг)" => 110f,
                "Велика(до 30кг)" => 160f,
                _ => 0f
            };

            float priceBox = 5f;
            float procent = 1.05f;

            string description = shipmentsDescriptionTextBox.Text;
            string size = selectedContent;
            string payer = payerSenderRadioButton.IsChecked == true ? "sender" : "receiver";
            string payment = cashRadioButton.IsChecked == true ? "cash" : "card";
            bool boxChecked = packingInBoxCheckBox.IsChecked == true;

            bool dataChanged = isFirstClick ||
                               description != previousDescription ||
                               size != previousSize ||
                               payer != previousPayer ||
                               payment != previousPayment ||
                               boxChecked != previousBoxChecked;

            if (dataChanged)
            {
                value *= procent;
                if (boxChecked)
                {
                    value += priceBox;
                }

                valueTextBox.Text = value.ToString("0.00");

                previousDescription = description;
                previousSize = size;
                previousPayer = payer;
                previousPayment = payment;
                previousBoxChecked = boxChecked;

                isFirstClick = false;
                return;
            }

            GlobalData.invoice.ShipmentsDescription = description;
            GlobalData.invoice.ParcelSize = selectedContent;
            GlobalData.invoice.Payer = payer;
            GlobalData.invoice.PaymentMethod = payment;
            GlobalData.invoice.Price = valueTextBox.Text;

            if(!SaveNewInvoice())
            {
                return;
            }

            main.ShowMainMenu();
        }

        private async Task MarkInvalidFields()
        {
            var redBrush = new SolidColorBrush(Colors.Red);

            if (string.IsNullOrWhiteSpace(shipmentsDescriptionTextBox.Text))
                shipmentsDescriptionTextBox.BorderBrush = redBrush;

            await Task.Delay(250);

            shipmentsDescriptionTextBox.BorderBrush = defaultBrush;
        }

        private bool SaveNewInvoice()
        {
            DataPath dataPath = new DataPath();
            JsonHandler jsonHandler = new JsonHandler();
            return jsonHandler.WriteNewInvoiceToJson(dataPath.GetInvoicePath(), GlobalData.invoice);
        }
    }
}
