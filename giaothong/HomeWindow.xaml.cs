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
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Wpf;

namespace giaothong
{
    /// <summary>
    /// Interaction logic for HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set;}
        public Func<string, string> Values { get; set; }
        public HomeWindow()
        {
            InitializeComponent();

            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Venta 2021",
                    Values = new ChartValues<double>() {20, 15, 30 ,25, 6, 7, 2, 3 ,5 , 9 ,11 ,12}
                }
            };

            SeriesCollection.Add(new ColumnSeries
            {
                Title = "Venta 2021",
                Values = new ChartValues<double>() { 22, 9, 45, 88 }
            });

            SeriesCollection[1].Values.Add(48d);
            Labels = new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
            DataContext = this;
        }
    }
}
