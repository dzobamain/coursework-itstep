﻿/*
 * MainWindow.xaml.cs
 */
using System;
using System.Windows;

using Client.MainMenu.Views;

namespace Client.MainMenu
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
