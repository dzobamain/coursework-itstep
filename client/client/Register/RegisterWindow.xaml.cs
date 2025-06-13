/*
 * RegisterWindow.xaml.cs
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace client.Register
{
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void CreateAccountButton_Click(object sender, RoutedEventArgs e)
        {
            User newUser = new User
            {
                firstName = nameTextBox.Text,
                lastName = lastNameTextBox.Text,
                middleName = fatherlyTextBox.Text,
                phoneNumber = telephoneNumberTextBox.Text,
                password = passwordTextBox.Text
            };

            DataFormatter dataFormatter = new DataFormatter();
            dataFormatter.SetUser(newUser);

            if (!dataFormatter.ValidateUserData() || newUser.password != repeatPasswordTextBox.Text)
            {
                ToDefault();
            }
        }

        private void ToDefault()
        {
            nameTextBox.Text = string.Empty;
            lastNameTextBox.Text = string.Empty;
            fatherlyTextBox.Text = string.Empty;
            telephoneNumberTextBox.Text = string.Empty;
            passwordTextBox.Text = string.Empty;
            repeatPasswordTextBox.Text = string.Empty;
        }
    }
}
