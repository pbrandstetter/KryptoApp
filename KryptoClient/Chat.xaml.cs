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
    /// Interaction logic for Chat.xaml
    /// </summary>
    public partial class Chat : Window
    {
        public Chat()
        {
            InitializeComponent();
            this.loadUsers();
        }

        public void loadUsers()
        {
            //TextEingabe.Text = "asd";
            AllUsers.DataContext = WebClient.WebClient.GetAllUserAsync();
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            string sendText = TextEingabe.Text;

        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
