/*
 * App.xaml.cs
 */
using System;
using System.IO;
using System.Windows;

using Client.Login;
using Client.MainMenu;
using Client.Models;

namespace Client
{
    public partial class App : Application
    {
        public App()
        {
            this.ShutdownMode = ShutdownMode.OnLastWindowClose;
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Validation.DataFormatter dataFormatter = new();
            Data.Path.DataPath userDataPath = new();

            string jsonPath = userDataPath.GetUserPath();

            if (!File.Exists(jsonPath))
            {
                new LoginWindow().Show();
                return;
            }

            User user = dataFormatter.ReadUserFromJson(jsonPath);

            if (dataFormatter.ValidateUserData(user))
            {
                string messageFromServer = await Network.Send.SendJsonAsync(jsonPath);  
                bool result = !string.IsNullOrWhiteSpace(messageFromServer) && bool.TryParse(messageFromServer, out bool parsed) && parsed;

                if (result)
                {
                    new MainWindow().Show();
                }
                else
                {
                    new LoginWindow().Show();
                }
            }
            else
            {
                new LoginWindow().Show();
            }
        }
    }

    public static class GlobalData
    {
        public static Invoice invoice { get; set; } = new Invoice();
    }
}
