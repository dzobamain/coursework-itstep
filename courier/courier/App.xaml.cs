using courier.MainMenu.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace courier
{
    public partial class App : Application
    {
        public App()
        {
            this.ShutdownMode = ShutdownMode.OnLastWindowClose;
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            MainMenu.MainWindow mainWindow = new MainMenu.MainWindow();
            mainWindow.Show();
        }
    }
}
