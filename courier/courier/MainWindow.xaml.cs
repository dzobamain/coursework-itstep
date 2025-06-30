/*
    MainWindow.xaml
*/
using System;
using System.Windows;

namespace Courier.MainMenu
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainContent.Content = new Views.MainMenuWindow(this);
        }

        public void ShowMainMenuWindow()
        {
            MainContent.Content = new Views.MainMenuWindow(this);
        }

        public void ShowAcceptedInvoicesWindow()
        {
            MainContent.Content = new Views.AcceptedInvoicesWindow(this);
        }
    }
}
