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

namespace TrainR
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<ShortName> sn = new List<ShortName>();
        public int DestId;
        public MainWindow()
        {
            InitializeComponent();
            using (var ct = new TimeTable())
            {
                var cityList = ct.City.Select(q => new {q.Id, q.Name}).ToList();
                foreach (var item in cityList)
                {
                    StartBox.Items.Add(item.Name);
                    sn.Add(new ShortName(item.Name, (int)item.Id));
                }
            }
        }

        private void DestinationBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = sender as ComboBox;
            var selection = item.SelectedItem as string;
            if(item.SelectedItem != null)
            {
                 ShortName selectedCity = sn.Single(s => s.Name == selection);
                 using (var ct = new TimeTable())
                 {
                    var cityList = ct.Departure
                                       .Include(b => b.Connection)
                                       .Where(q => q.Connection.StartId == DestId && q.Connection.DestinationId == selectedCity.Id)
                                       .Include(c => c.Connection.Destination)
                                       .Include(c => c.Connection.Start)
                                       .Include(c => c.Connection.Train)
                                       .Select(q => new { FROM = q.Connection.Start.Name, TO = q.Connection.Destination.Name, DEPARTURE = q.Time, TRAVEL_TIME = q.TravelTime, TRAIN = q.Connection.Train.Name }).ToList();
                    grid.ItemsSource = cityList;
                 }
            }
                
        }

        private void StartBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DestinationBox.Items.Clear();            
            var item = sender as ComboBox;
            var selection = item.SelectedItem as string;
            ShortName selectedCity = sn.Single(s => s.Name == selection);
            DestId = selectedCity.Id;

            using (var ct = new TimeTable())
            {
                var cityList = ct.Departure
                                        .Include(b => b.Connection)
                                        .Where(q => q.Connection.StartId == DestId)
                                        .Include(c => c.Connection.Destination)
                                        .Include(c => c.Connection.Start)
                                        .Include(c => c.Connection.Train)
                                        .Select(q => new { FROM = q.Connection.Start.Name, TO = q.Connection.Destination.Name, DEPARTURE = q.Time, TRAVEL_TIME = q.TravelTime, TRAIN = q.Connection.Train.Name }).ToList();
                grid.ItemsSource = cityList;
            }

            using (var ct = new TimeTable())
            {
                var cityList = ct.Connection
                                    .Include(c => c.Start)
                                    .Include(c => c.Destination)
                                    .Where(q => q.StartId == DestId)
                                    .Select(q => new { q.Id, q.Destination.Name })
                                    .ToList();
                foreach (var city in cityList)
                {
                    DestinationBox.Items.Add(city.Name);
                }
            }

        }
    }

    public class ShortName
    {
    public string Name;
    public int Id;

    public ShortName(string Name, int Id)
    {
        this.Name = Name;
        this.Id = Id;
    }
}
}
