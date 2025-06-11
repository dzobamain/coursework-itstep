/*
 * AppWindow.xaml.cs
*/

using System.Configuration;
using System.Data;
using System.Windows;

using client.Login;
using client.Register;

namespace client
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            RegisterWindow registerWindow = new RegisterWindow();
            LoginWindow loginWindow = new LoginWindow();
            
            registerWindow.Show();
            loginWindow.Show();
        }
    }
}

