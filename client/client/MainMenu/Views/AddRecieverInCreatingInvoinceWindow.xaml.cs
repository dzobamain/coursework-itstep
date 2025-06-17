///AddRecieverInCreatingInvoinceWindow.xaml.cs

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
    public partial class AddRecieverInCreatingInvoinceWindow : Window
    {
        public AddRecieverInCreatingInvoinceWindow()
        {
            InitializeComponent();
        }
        private void returnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
