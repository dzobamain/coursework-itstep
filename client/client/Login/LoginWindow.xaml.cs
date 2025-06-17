/*
 * LoginWindow.xaml.cs
*/

using System;
using System.Windows;
using System.Windows.Media;
using System.Threading.Tasks;

using client.Register;
using client.MainMenu;

namespace client.Login
{
    public partial class LoginWindow : Window
    {
        private Brush defaultBrush;

        public LoginWindow()
        {
            InitializeComponent();

            

            defaultBrush = telephoneNumberTextBox.BorderBrush;
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();

            this.Close();
        }

        private void ContinueButton_Click(object sender, RoutedEventArgs e)
        {
            User user = new User
            {
                regOrLog = "log",
                phoneNumber = telephoneNumberTextBox.Text,
                password = passwordTextBox.Text
            };

            DataFormatter dataFormatter = new DataFormatter();

            if (!dataFormatter.ValidateUserData(user))
            {
                MarkInvalidField();
                return;
            }

            UserDataPath userDataPath = new UserDataPath();
            dataFormatter.WriteUserToJson(userDataPath.GetPath(), user);
        }

        private async void MarkInvalidField()
        {
            if (telephoneNumberTextBox != null && passwordTextBox != null)
            {
                defaultBrush = telephoneNumberTextBox.BorderBrush;
                telephoneNumberTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
                passwordTextBox.BorderBrush = new SolidColorBrush(Colors.Red);

                await Task.Delay(250);

                telephoneNumberTextBox.BorderBrush = defaultBrush;
                passwordTextBox.BorderBrush = defaultBrush;
            }
        }
    }
}
