﻿/*
 * LoginWindow.xaml.cs
*/

using System;
using System.Windows;
using System.Windows.Media;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Reflection;
using System.IO;

using Courier.Models;

namespace Courier.Login
{
    public partial class LoginWindow : Window
    {
        private Brush defaultBrush;

        public LoginWindow()
        {
            InitializeComponent();

            defaultBrush = telephoneNumberTextBox.BorderBrush;
        }

        private void ContinueButton_Click(object sender, RoutedEventArgs e)
        {
            CourierData user = new CourierData
            {
                status = "courier",
                phoneNumber = telephoneNumberTextBox.Text,
                password = passwordTextBox.Text
            };

            DataFormatter dataFormatter = new DataFormatter();

            if (!dataFormatter.ValidateUserData(user))
            {
                MarkInvalidField();
                return;
            }

            Data.Path.DataPath userDataPath = new();
            dataFormatter.WriteUserToJson(userDataPath.GetCourierDataPath(), user);

            string exePath = Path.ChangeExtension(Assembly.GetEntryAssembly()?.Location, ".exe");

            if (!string.IsNullOrEmpty(exePath) && File.Exists(exePath))
            {
                Process.Start(new ProcessStartInfo(exePath)
                {
                    UseShellExecute = true // важливо для WPF
                });
            }

            Application.Current.Shutdown();
        }

        private async void MarkInvalidField()
        {
            telephoneNumberTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
            passwordTextBox.BorderBrush = new SolidColorBrush(Colors.Red);

            await Task.Delay(250);

            telephoneNumberTextBox.BorderBrush = defaultBrush;
            passwordTextBox.BorderBrush = defaultBrush;
        }
    }
}
