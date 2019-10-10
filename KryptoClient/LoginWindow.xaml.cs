using KrypoLibrary.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KryptoClient
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            User user = new User();
            user.Username = UsernameInput.Text;
            user.Password = UsernameInput.Text;
            var register = WebClient.WebClient.RegisterAsync(user);
            var newForm = new Chat(); //create your new form.
            newForm.Show(); //show the new form.
            this.Close();

        }
    }
}
