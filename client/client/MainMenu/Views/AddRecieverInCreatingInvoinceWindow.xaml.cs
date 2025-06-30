/*
 * AddRecieverInCreatingInvoinceWindow.xaml.cs
 */
using System;
using System.Windows;
using Client.Models;

namespace client.MainMenu.Views
{
    public partial class AddRecieverInCreatingInvoinceWindow : Window
    {
        public RecipientData Recipient { get; private set; }

        public AddRecieverInCreatingInvoinceWindow()
        {
            InitializeComponent();
        }
        private void returnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            phoneNumberTextBox.Text = string.Empty;
            lastNameTextBox.Text = string.Empty;
            firstNameTextBox.Text = string.Empty;
            middleNameTextBox.Text = string.Empty;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Recipient = new RecipientData
            {
                Phone = phoneNumberTextBox.Text,
                FirstName = firstNameTextBox.Text,
                LastName = lastNameTextBox.Text,
                MiddleName = middleNameTextBox.Text
            };

            this.DialogResult = true;
            this.Close();
        }
    }
}
