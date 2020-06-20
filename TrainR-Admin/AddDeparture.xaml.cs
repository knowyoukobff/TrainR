using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    /// Interaction logic for AddConnection.xaml
    /// </summary>
    public partial class AddDeparture : Window
    {
        public Departure NewDeparture;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int connectionId;
            TimeSpan time;
            int length;

            try
            {
                var connectionBox = Connection.SelectedItem as ComboBoxItem;
                connectionId= (int)connectionBox.Tag;
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("You did not choose connection.");
                return;
            }

            try
            {
                time = TimeSpan.Parse(Time.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Time format is incorrect.");
                return;
            }

            try
            {
                length = Int32.Parse(TravelTime.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Travel time must be a number.");
                return;
            }

            NewDeparture = new Departure { Id = null, ConnectionId = connectionId, Time = time, TravelTime = length };

            DialogResult = true;
        }

        public AddDeparture()
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

                    Connection.Items.Add(newComboItem);
                }
            }
        }
    }
}
