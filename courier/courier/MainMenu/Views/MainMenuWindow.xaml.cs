/*
    MainMenuWindow.xaml
 */

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

namespace courier.MainMenu.Views
{
    public partial class MainMenuWindow : UserControl
    {
        private MainWindow _main;
        public MainMenuWindow(MainWindow main)
        {
            InitializeComponent();
            _main = main;
        }

        private void AcceptedInvoiceButton_Click(object sender, RoutedEventArgs e)
        {
            _main.ShowAcceptedInvoicesWindow();
        }
    }
}
