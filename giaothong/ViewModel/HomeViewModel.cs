using giaothong.Model;
using LiveCharts;
using LiveCharts.Wpf;
using QuanLyShop.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace giaothong.ViewModel
{
    class HomeViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private USER _user;
        public USER User { get => _user; set { _user = value; OnPropertyChanged(); } }

        public ICommand LogoutCommand { get; set; }
        public ICommand OpenTeacherCommand { get; set; }
        public ICommand OpenVehicleCommand { get; set; }
        public ICommand miniWindow { get; set; }
        public ICommand mouseLeftButtonDowWindow { get; set; }
        public ICommand maximizeIconWindow { get; set; }
        public ICommand OpenManagerTrainCommand { get; set; }
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<string, string> Values { get; set; }

        public HomeViewModel()
        {
            User = MainViewModel.User;

            //thu nhỏ window
            miniWindow = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.WindowState = WindowState.Minimized;
            });


            //duy chuyển uiwndow
            mouseLeftButtonDowWindow = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.DragMove();
            });

            //Phong to thu nho
            maximizeIconWindow = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                if (p.WindowState == WindowState.Normal)
                {
                    p.WindowState = WindowState.Maximized;
                }
                else
                {
                    p.WindowState = WindowState.Normal;
                }
            });

            LogoutCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });

            OpenTeacherCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Hide();
                TeacherWindow teacher = new TeacherWindow();
                teacher.ShowDialog();
                p.ShowDialog();
            });

            OpenManagerTrainCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Hide();
                TrainingWindow train = new TrainingWindow();
                train.ShowDialog();
                p.ShowDialog();
            });

            OpenVehicleCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Hide();
                VehicleWindow vehicle = new VehicleWindow();
                vehicle.ShowDialog();
                p.ShowDialog();
            });

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

        }
    }
}
