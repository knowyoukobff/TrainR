using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TrainR_Admin
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public bool LoggedIn;

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new Users())
            {
                try
                {
                    var result = context.User
                        .Where(x => x.Login == LoginBox.Text)
                        .Where(x => x.Password == PasswordHash.Encrypt(PasswordBox.Password))
                        .Single();

                    LoggedIn = true;

                    DialogResult = true;
                }
                catch (InvalidOperationException)
                {
                    MessageBox.Show("Invalid credentials.");
                    LoggedIn = false;
                    return;
                }
            }
        }
    }
}
