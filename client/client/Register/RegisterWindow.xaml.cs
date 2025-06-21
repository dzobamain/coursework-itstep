/*
 * RegisterWindow.xaml.cs
*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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
using System.IO;

namespace client.Register
{
    public partial class RegisterWindow : Window
    {
        private Brush defaultBrush;
        public RegisterWindow()
        {
            InitializeComponent();
            defaultBrush = lastNameTextBox.BorderBrush;
        }

        private void CreateAccountButton_Click(object sender, RoutedEventArgs e)
        {
            User newUser = new User
            {
                status = "client",
                regOrLog = "reg",
                firstName = nameTextBox.Text,
                lastName = lastNameTextBox.Text,
                middleName = fatherlyTextBox.Text,
                phoneNumber = telephoneNumberTextBox.Text,
                password = passwordTextBox.Text
            };

            DataFormatter dataFormatter = new DataFormatter();

            if (!dataFormatter.ValidateUserData(newUser) || newUser.password != repeatPasswordTextBox.Text)
            {
                MarkInvalidField();
                return;
            }

            DataPath userDataPath = new DataPath();
            dataFormatter.WriteUserToJson(userDataPath.GetUserPath(), newUser);

            string exePath = System.IO.Path.ChangeExtension(Assembly.GetEntryAssembly()?.Location, ".exe");

            if (!string.IsNullOrEmpty(exePath) && File.Exists(exePath))
            {
                Process.Start(new ProcessStartInfo(exePath)
                {
                    UseShellExecute = true
                });
            }

            Application.Current.Shutdown();
        }

        private async void MarkInvalidField()
        {
            lastNameTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
            nameTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
            fatherlyTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
            telephoneNumberTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
            passwordTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
            repeatPasswordTextBox.BorderBrush = new SolidColorBrush(Colors.Red);

            await Task.Delay(250);

            lastNameTextBox.BorderBrush = defaultBrush;
            nameTextBox.BorderBrush = defaultBrush;
            fatherlyTextBox.BorderBrush = defaultBrush;
            telephoneNumberTextBox.BorderBrush = defaultBrush;
            passwordTextBox.BorderBrush = defaultBrush;
            repeatPasswordTextBox.BorderBrush = defaultBrush;
        }
    }
}
