//CreateInvoinceSRWindow.xaml.cs

using GMap.NET.MapProviders;
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
    public partial class CreateInvoinceSRWindow : UserControl
    {
        MainWindow main;
        public CreateInvoinceSRWindow(MainWindow main)
        {
            InitializeComponent();
            this.main = main;
        }

        private void ContinueButton_Click(object sender, RoutedEventArgs e)
        {
            main.CreateNewInvoinceInfoAboutParcel();
        }

        private void returnBackButton_Click(object sender, RoutedEventArgs e)
        {
            main.ShowMainMenu();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            receivingAddressTextBox.Text = string.Empty;
            senderAddressTextBox.Text = string.Empty;
            fullNameRecieverBox.Text = string.Empty;
            phoneNumberRecieverBox.Text = string.Empty;
            commentTextBox.Text = string.Empty;
        }

        private void RecieverInformation_Click(object sender, RoutedEventArgs e)
        {
            AddRecieverInCreatingInvoinceWindow addRecieverWindow = new AddRecieverInCreatingInvoinceWindow();
            addRecieverWindow.ShowDialog();
        }
    }
}
