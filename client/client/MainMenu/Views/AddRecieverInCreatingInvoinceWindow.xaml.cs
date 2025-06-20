///AddRecieverInCreatingInvoinceWindow.xaml.cs

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
