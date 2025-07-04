﻿/*
 * CreateInvoinceSRWindow.xaml.cs
 */
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using Client.Models;

namespace Client.MainMenu.Views
{
    public partial class CreateInvoinceSRWindow : UserControl
    {
        MainWindow main;
        private Brush defaultBrush;
        RecipientData recipient;
        
        public CreateInvoinceSRWindow(MainWindow main)
        {
            InitializeComponent();
            this.main = main;
            defaultBrush = receivingAddressTextBox.BorderBrush;
        }

        private async void ContinueButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(receivingAddressTextBox.Text) ||
                string.IsNullOrWhiteSpace(senderAddressTextBox.Text) ||
                string.IsNullOrWhiteSpace(fullNameSenderBox.Text) ||
                string.IsNullOrWhiteSpace(commentTextBox.Text))
            {
                await MarkInvalidFields();
                return;
            }

            GlobalData.invoice.status = "package";
            GlobalData.invoice.progress = "created";

            Client.Data.Path.DataPath dataPath = new();
            Client.Validation.DataFormatter dataFormatter = new();
            User user = dataFormatter.ReadUserFromJson(dataPath.GetUserPath());
            GlobalData.invoice.OwnerPhoneNumber = user.phoneNumber;

            GlobalData.invoice.PecipientAddress = receivingAddressTextBox.Text;
            GlobalData.invoice.RecipientData = recipient;
            GlobalData.invoice.ShippingAddress = senderAddressTextBox.Text;
            GlobalData.invoice.ShippingData = fullNameSenderBox.Text;
            GlobalData.invoice.ParcelComment = commentTextBox.Text;

            main.CreateNewInvoinceInfoAboutParcel();
        }

        private async Task MarkInvalidFields()
        {
            var redBrush = new SolidColorBrush(Colors.Red);

            if (string.IsNullOrWhiteSpace(receivingAddressTextBox.Text))
                receivingAddressTextBox.BorderBrush = redBrush;

            if (string.IsNullOrWhiteSpace(senderAddressTextBox.Text))
                senderAddressTextBox.BorderBrush = redBrush;

            if (string.IsNullOrWhiteSpace(fullNameSenderBox.Text))
                fullNameSenderBox.BorderBrush = redBrush;

            if (string.IsNullOrWhiteSpace(commentTextBox.Text))
                commentTextBox.BorderBrush = redBrush;

            await Task.Delay(250);

            receivingAddressTextBox.BorderBrush = defaultBrush;
            senderAddressTextBox.BorderBrush = defaultBrush;
            fullNameSenderBox.BorderBrush = defaultBrush;
            commentTextBox.BorderBrush = defaultBrush;
        }

        private void returnBackButton_Click(object sender, RoutedEventArgs e)
        {
            main.ShowMainMenu();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            receivingAddressTextBox.Text = string.Empty;
            senderAddressTextBox.Text = string.Empty;
            fullNameRecieverBox.Text = string.Empty;
            phoneNumberRecieverBox.Text = string.Empty;
            commentTextBox.Text = string.Empty;
        }

        private void RecieverInformation_Click(object sender, RoutedEventArgs e)
        {
            var addRecieverWindow = new AddRecieverInCreatingInvoinceWindow();
            bool? result = addRecieverWindow.ShowDialog();

            if (result == true)
            {
                recipient = addRecieverWindow.Recipient;
            }
        }
    }
}
