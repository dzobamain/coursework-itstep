/*
 MainMenuWindow.xaml.cs
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Controls;

namespace client.MainMenu.Views
{
    public partial class MainMenuWindow : UserControl
    {
        private MainWindow _main;
        public MainMenuWindow(MainWindow main)
        {
            InitializeComponent();
            _main = main;
        }

        private void CreateInvoiceButton_Click(object sender, RoutedEventArgs e)
        {
            _main.CreateNewInvoinceInfoAboutSW();
        }
    }
}
