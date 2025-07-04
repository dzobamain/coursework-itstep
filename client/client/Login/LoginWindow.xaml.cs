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

using Client.Models;
using Client.Register;

namespace Client.Login
{
    public partial class LoginWindow : Window
    {
        private Brush defaultBrush;

        public LoginWindow()
        {
            InitializeComponent();
            defaultBrush = telephoneNumberTextBox.BorderBrush;
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();

            this.Close();
        }

        private void ContinueButton_Click(object sender, RoutedEventArgs e)
        {
            User user = new User
            {
                status = "client",
                regOrLog = "log",
                phoneNumber = telephoneNumberTextBox.Text,
                password = passwordTextBox.Text
            };

            Validation.DataFormatter dataFormatter = new();

            if (!dataFormatter.ValidateUserData(user))
            {
                MarkInvalidField();
                return;
            }

            Data.Path.DataPath userDataPath = new();
            dataFormatter.WriteUserToJson(userDataPath.GetUserPath(), user);

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
