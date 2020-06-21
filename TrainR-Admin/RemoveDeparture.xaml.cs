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
    public partial class RemoveDeparture : Window
    {
        private int departureId;
        public int DepartureId { get => departureId; }

        public RemoveDeparture()
        {
            InitializeComponent();

            using (var context = new TimeTable())
            {
                var connections = context.Departure
                   .Select(x => new { x.Id, start = x.Connection.Start.Name, destination = x.Connection.Destination.Name, time = x.Time, x.TravelTime })
                   .ToList();

                foreach (var connection in connections)
                {
                    TimeSpan arrival = connection.time.Add(TimeSpan.FromMinutes(connection.TravelTime));

                    var newComboItem = new ComboBoxItem
                    {
                        Content = $"{connection.start} => {connection.destination} :: {connection.time} -> {arrival}",
                        Tag = connection.Id
                    };

                    DepartureName.Items.Add(newComboItem);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var item = DepartureName.SelectedItem as ComboBoxItem;
            departureId = (int)item.Tag;

            DialogResult = true;
        }
    }
}
