using courier.MainMenu.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using courier.MainMenu;
using client.Login;
using GMap.NET.MapProviders;

namespace courier
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
            CourierDataPath userDataPath = new CourierDataPath();

            string jsonPath = userDataPath.GetCourierDataPath();

            if (!File.Exists(jsonPath))
            {
                new LoginWindow().Show();
                return;
            }

            Courier user = dataFormatter.ReadCourierFromJson(jsonPath);

            if (dataFormatter.ValidateUserData(user))
            {
                string messageFromServer = await Send.SendJsonAsync(jsonPath);
                bool result = !string.IsNullOrWhiteSpace(messageFromServer) && bool.TryParse(messageFromServer, out bool parsed) && parsed;

                result = true; //TEST

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
