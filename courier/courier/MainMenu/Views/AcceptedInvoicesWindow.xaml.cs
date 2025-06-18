/*
    AcceptedInvoicesWindow.xaml
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
    public partial class AcceptedInvoicesWindow : UserControl
    {
        private MainWindow _main;
        public AcceptedInvoicesWindow(MainWindow main)
        {
            InitializeComponent();
            _main = main;
        }

        private void IncomingInvoiceButton_Click(object sender, RoutedEventArgs e)
        {
            _main.ShowMainMenuWindow();
        }
    }
}
