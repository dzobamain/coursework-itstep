/*
 * App.xaml.cs
 */
using System;
using System.IO;
using System.Windows;

using Courier.Models;
using Courier.MainMenu;
using Courier.Login;

namespace Courier
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

            DataFormatter dataFormatter = new DataFormatter();
            Data.Path.DataPath userDataPath = new();

            string jsonPath = userDataPath.GetCourierDataPath();

            if (!File.Exists(jsonPath))
            {
                new LoginWindow().Show();
                return;
            }

            CourierData user = dataFormatter.ReadCourierFromJson(jsonPath);

            if (dataFormatter.ValidateUserData(user))
            {
                string messageFromServer = await Network.Send.SendJsonAndReceiveAllAsync(jsonPath);
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
}
