/*
 * App.xaml.cs
*/

using System.Configuration;
using System.Data;
using System.Windows;

using client.Login;
using client.Register;
using client.MainMenu;

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

            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
        }
    }
}

