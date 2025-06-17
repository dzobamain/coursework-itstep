/*
 * App.xaml.cs
*/

using System.Windows;
using client.Login;
using client.MainMenu;
using client.Register;

namespace client
{
    public partial class App : Application
    {
        public App()
        {
            this.ShutdownMode = ShutdownMode.OnLastWindowClose;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            DataFormatter dataFormatter = new DataFormatter();
            UserDataPath userDataPath = new UserDataPath();

            User user = dataFormatter.ReadUserFromJson(userDataPath.GetPath());

            if (user != null && dataFormatter.ValidateUserData(user))
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
            }
            else
            {
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.Show();
            }
        }
    }
}
