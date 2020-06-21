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
    public partial class AddConnection : Window
    {
        public Connection NewConnection;

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                var startItem = Start.SelectedItem as ComboBoxItem;
                var destinationItem = Destination.SelectedItem as ComboBoxItem;
                var trainItem = Train.SelectedItem as ComboBoxItem;
                int startId = (int)startItem.Tag;
                int destinationId = (int)destinationItem.Tag;
                int trainId = (int)trainItem.Tag;
                NewConnection = new Connection { Id = null, StartId = startId, DestinationId = destinationId, TrainId = trainId };
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Not all fields selected.");
                return;
            }

            DialogResult = true;
        }

        private void Start_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var box = sender as ComboBox;
            var item = box.SelectedItem as ComboBoxItem;
            string selectedCity = item.Content as string;

            Destination.Items.Clear();

            using (var context = new TimeTable())
            {
                var destination = context.City
                            .Select(x => new { x.Id, x.Name })
                            .Where(x => x.Name != selectedCity)
                            .ToList();

                foreach (var city in destination)
                {
                    var newComboItem = new ComboBoxItem
                    {
                        Content = city.Name,
                        Tag = city.Id
                    };

                    Destination.Items.Add(newComboItem);
                }
            }
        }

        public AddConnection()
        {
            InitializeComponent();

            using (var context = new TimeTable())
            {
                var cities = context.City.Select(x => new { x.Id, x.Name }).ToList();

                foreach (var city in cities)
                {
                    var newComboItem = new ComboBoxItem
                    {
                        Content = city.Name,
                        Tag = city.Id
                    };

                    Start.Items.Add(newComboItem);
                }

                var trains = context.Train.Select(x => new { x.Id, x.Name }).ToList();

                foreach (var train in trains)
                {
                    var newComboItem = new ComboBoxItem
                    {
                        Content = train.Name,
                        Tag = train.Id
                    };

                    Train.Items.Add(newComboItem);
                }
            }
        }
    }
}
