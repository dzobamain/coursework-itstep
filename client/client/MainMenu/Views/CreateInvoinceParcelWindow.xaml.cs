// CreateInvoinceParcelWindow.xaml.cs

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

        private async void CreateParcelButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(shipmentsDescriptionTextBox.Text) ||
                parcelSizeComboBox.SelectedItem == null ||
                (payerSenderRadioButton.IsChecked == false && payerRecieverRadioButton.IsChecked == false) ||
                (cashRadioButton.IsChecked == false && cardRadioButton.IsChecked == false))
            {
                await MarkInvalidFields();
                return;
            }

            GlobalData.invoice.ShipmentsDescription = shipmentsDescriptionTextBox.Text;

            if (payerSenderRadioButton.IsChecked == true)
                GlobalData.invoice.Payer = "sender";
            else if (payerRecieverRadioButton.IsChecked == true)
                GlobalData.invoice.Payer = "receiver";

            if (cashRadioButton.IsChecked == true)
                GlobalData.invoice.PaymentMethod = "cash";
            else if (cardRadioButton.IsChecked == true)
                GlobalData.invoice.PaymentMethod = "card";


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
    }
}
