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
    public partial class RemoveCity : Window
    {
        private int cityId;
        public int CityId { get => cityId; }

        public RemoveCity()
        {
            InitializeComponent();

            using (var context = new TimeTable())
            {
                var cities = context.City
                            .Select(x => new { x.Id, x.Name })
                            .ToList();

                foreach (var city in cities)
                {
                    var newComboItem = new ComboBoxItem
                    {
                        Content = city.Name,
                        Tag = city.Id
                    };

                    CityName.Items.Add(newComboItem);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var item = CityName.SelectedItem as ComboBoxItem;
            cityId = (int)item.Tag;

            DialogResult = true;
        }
    }
}
