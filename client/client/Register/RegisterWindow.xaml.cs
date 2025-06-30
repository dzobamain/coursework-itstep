/*
 * RegisterWindow.xaml.cs
 */
using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using System.IO;

using Client.Models;

namespace Client.Register
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

            Validation.DataFormatter dataFormatter = new();

            if (!dataFormatter.ValidateUserData(newUser) || newUser.password != repeatPasswordTextBox.Text)
            {
                MarkInvalidField();
                return;
            }

            Data.Path.DataPath userDataPath = new();
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
