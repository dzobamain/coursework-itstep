// CreateInvoinceParcelWindow.xaml.cs

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

namespace client.MainMenu.Views
{
    public partial class CreateInvoinceParcelWindow : UserControl
    {
        MainWindow main;
        public CreateInvoinceParcelWindow(MainWindow main)
        {
            InitializeComponent();
            this.main = main;
        }

        private void returnBackButton_Click(object sender, RoutedEventArgs e)
        {
            main.ShowMainMenu();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            shipmentsDescriptionTextBox.Text = string.Empty;
            valueTextBox.Text = string.Empty;
            parcelSizeComboBox.SelectedIndex = -1;
            packingInBoxCheckBox.IsChecked = false;
        }
    }
}
