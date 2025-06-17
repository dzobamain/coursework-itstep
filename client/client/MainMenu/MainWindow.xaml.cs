//MainWindow.xaml.cs

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using client.MainMenu.Views;

namespace client.MainMenu
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainContent.Content = new MainMenuWindow(this);
        }

        public void ShowMainMenu()
        {
            MainContent.Content = new MainMenuWindow(this);
        }

        public void CreateNewInvoinceInfoAboutSW()
        {
            MainContent.Content = new CreateInvoinceSRWindow(this);
        }

        public void CreateNewInvoinceInfoAboutParcel()
        {
            MainContent.Content = new CreateInvoinceParcelWindow(this);
        }
    }
}
