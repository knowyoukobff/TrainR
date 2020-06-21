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
        public MainWindow()
        {
            InitializeComponent();

            RefreshTables();
        }

        private void Test_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new TimeTable())
            {
                context.Add(new Train {  });
                context.SaveChanges();
            }
        }

        private void AddCity_Click(object sender, RoutedEventArgs e)
        {
            var addCityDialog = new AddCity();

            if (addCityDialog.ShowDialog() == true)
            {
                using (var context = new TimeTable())
                {
                    context.Add(new City { Id = null, Name = addCityDialog.NewName });
                    context.SaveChanges();
                }

                RefreshTables();
            }
        }

        private void AddConnection_Click(object sender, RoutedEventArgs e)
        {
            var addConnectionDialog = new AddConnection();

            if (addConnectionDialog.ShowDialog() == true)
            {
                using (var context = new TimeTable())
                {
                    context.Add(addConnectionDialog.NewConnection);
                    context.SaveChanges();
                }

                RefreshTables();
            }
        }

        private void AddDeparture_Click(object sender, RoutedEventArgs e)
        {
            var addDepartureDialog = new AddDeparture();

            if (addDepartureDialog.ShowDialog() == true)
            {
                using (var context = new TimeTable())
                {
                    context.Add(addDepartureDialog.NewDeparture);
                    context.SaveChanges();
                }

                RefreshTables();
            }
        }


        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            var addUserDialog = new AddUser();

            if (addUserDialog.ShowDialog() == true)
            {
                MessageBox.Show("User added.");
            }
        }

        private void RemoveUser_Click(object sender, RoutedEventArgs e)
        {
            var removeUserDialog = new RemoveUser();

            if (removeUserDialog.ShowDialog() == true)
            {
                MessageBox.Show("User removed.");
            }
        }

        private void RemoveCity_Click(object sender, RoutedEventArgs e)
        {
            var removeCityDialog = new RemoveCity();

            if (removeCityDialog.ShowDialog() == true)
            {
                using (var context = new TimeTable())
                {
                    int id = removeCityDialog.CityId;

                    context.Remove(context.City.Single(x => x.Id == id));
                    context.SaveChanges();
                }
                
                RefreshTables();
            }
        }

        private void RemoveConnection_Click(object sender, RoutedEventArgs e)
        {
            var removeConnectionDialog = new RemoveConnection();

            if (removeConnectionDialog.ShowDialog() == true)
            {
                using (var context = new TimeTable())
                {
                    int id = removeConnectionDialog.ConnectionId;

                    context.Remove(context.Connection.Single(x => x.Id == id));
                    context.SaveChanges();
                }

                RefreshTables();
            }
        }

        private void RemoveDeparture_Click(object sender, RoutedEventArgs e)
        {
            var removeDepartureDialog = new RemoveDeparture();

            if (removeDepartureDialog.ShowDialog() == true)
            {
                using (var context = new TimeTable())
                {
                    int id = removeDepartureDialog.DepartureId;

                    context.Remove(context.Departure.Single(x => x.Id == id));
                    context.SaveChanges();
                }

                RefreshTables();
            }
        }

        private void RefreshTables()
        {
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
    }
}
