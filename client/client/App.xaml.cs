using System.IO;
using System.Windows;
using client.Login;
using client.MainMenu;
using client.Register;
using Newtonsoft.Json;

namespace client
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
            UserDataPath userDataPath = new UserDataPath();

            string jsonPath = userDataPath.GetPath();

            if (!File.Exists(jsonPath))
            {
                new LoginWindow().Show();
                return;
            }

            User user = dataFormatter.ReadUserFromJson(jsonPath);

            if (dataFormatter.ValidateUserData(user))
            {
                //string jsonString = await File.ReadAllTextAsync(jsonPath);

                string messageFromServer = await Send.SendJsonAsync(jsonPath);
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
