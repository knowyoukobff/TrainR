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

namespace TrainR_Admin
{
    /// <summary>
    /// Interaction logic for AddUser.xaml
    /// </summary>
    public partial class AddUser : Window
    {
        public AddUser()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string login = Login.Text;
            string password = Password.Password;

            if (login != "" && password != "")
            {
                var newUser = new User { Id = null, Login = login, Password = PasswordHash.Encrypt(password) };

                using (var context = new Users())
                {
                    context.Add(newUser);
                    context.SaveChanges();
                }

                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Not all fields are provided.");
                return;
            }
        }
    } 
}