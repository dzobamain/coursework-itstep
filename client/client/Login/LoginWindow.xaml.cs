/*
 * LoginWindow.xaml.cs
*/

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
using client.Register;

namespace client.Login
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();

            this.Close();
        }

        private void ContinueButto_Click(object sender, RoutedEventArgs e)
        {
            User newUser = new User
            {
                phoneNumber = telephoneNumberTextBox.Text,
                password = passwordTextBox.Text
            };
        }
    }
}
