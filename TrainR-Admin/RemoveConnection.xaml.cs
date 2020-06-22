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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TrainR_Admin
{
    /// <summary>
    /// Interaction logic for AddCiti.xaml
    /// </summary>
    public partial class RemoveConnection : Window
    {
        private int connectionId;
        public int ConnectionId { get => connectionId; }

        public RemoveConnection()
        {
            InitializeComponent();

            using (var context = new TimeTable())
            {
                var connections = context.Connection
                   .Select(x => new { x.Id, start = x.Start.Name, destination = x.Destination.Name, train = x.Train.Name })
                   .ToList();

                foreach (var connection in connections)
                {
                    var newComboItem = new ComboBoxItem
                    {
                        Content = $"{connection.start} => {connection.destination} : {connection.train}",
                        Tag = connection.Id
                    };

                    ConnectionName.Items.Add(newComboItem);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var item = ConnectionName.SelectedItem as ComboBoxItem;
            connectionId = (int)item.Tag;

            DialogResult = true;
        }
    }
}
