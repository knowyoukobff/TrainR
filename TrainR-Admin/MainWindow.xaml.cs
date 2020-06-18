using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Dictionary<string, Type> entitiesDictionary = new Dictionary<string, Type>()
        {
            { "Cities", typeof(City) },
            { "Connections", typeof(Connection) },
            { "Departures", typeof(Departure) },
            { "Trains", typeof(Train) }
        };

        public MainWindow()
        {
            InitializeComponent();
            using (var context = new TimeTable())
            {
                var cities = context.City.Select(q => new { q.Id, q.Name }).ToList();

                var connections = context.Connection
                                            .Include(b => b.Start)
                                            .Include(c => c.Destination)
                                            .Include(t => t.Train)
                                            .Select(q => new { q.Id, Start = q.Start.Name, Destination = q.Destination.Name, train = q.Train.Name })
                                            .ToList();

                var departures = context.Departure
                                            .Include(c => c.Connection)
                                            .Select(q => new { q.Id, q.Time, q.TravelTime, Start = q.Connection.Start.Name, Destination = q.Connection.Destination.Name })
                                            .ToList();

                var trains = context.Train.Select(q => new { q.Id, q.Name }).ToList();

                CitiesGrid.ItemsSource = cities;
                ConnectionsGrid.ItemsSource = connections;
                DeparturesGrid.ItemsSource = departures;
                TrainsGrid.ItemsSource = trains;
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var item = sender as MenuItem;
            var parent = item.Parent as MenuItem;
            string header = parent.Header as string;


        }
    }
}
