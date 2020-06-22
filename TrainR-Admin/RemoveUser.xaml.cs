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
    /// Interaction logic for AddUser.xaml
    /// </summary>
    public partial class RemoveUser : Window
    {
        public RemoveUser()
        {
            InitializeComponent();

            using (var context = new Users())
            {
                var users = context.User.Select(x => new { x.Id, x.Login }).Where(x => x.Login != "admin");

                foreach (var user in users)
                {
                    var newComboItem = new ComboBoxItem
                    {
                        Content = user.Login,
                        Tag = user.Id
                    };

                    Users.Items.Add(newComboItem);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var item = Users.SelectedItem as ComboBoxItem;
            int userId = (int)item.Tag;

            using (var context = new Users())
            {
                context.Remove(context.User.Single(x => x.Id == userId));
                context.SaveChanges();

                DialogResult = true;
            }
        }
    } 
}