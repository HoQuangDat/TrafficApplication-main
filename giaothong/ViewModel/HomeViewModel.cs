using giaothong.Model;
using QuanLyShop.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace giaothong.ViewModel
{
    public class HomeViewModel : BaseViewModel
    {
        public ICommand closeViewTeacher { get; set; }

        public ICommand closeViewVehicle { get; set; }
        public ICommand viewTeacherWindow { get; set; }

        public ICommand viewVehicleWindow { get; set; }

        public HomeViewModel()
        {
            closeViewVehicle = new RelayCommand<Window>((p) => { return true; }, (p) => {
                p.Close();   
            });

            closeViewTeacher = new RelayCommand<Window>((p) => { return true; }, (p) => {
                p.Close();
            });

            viewTeacherWindow = new RelayCommand<Window>((p) => { return true; }, (p) => {
                p.Hide();
                TeacherWindow viewTeacher = new TeacherWindow();
                viewTeacher.ShowDialog();
                p.ShowDialog();
            });

            viewVehicleWindow = new RelayCommand<Window>((p) => { return true; }, (p) => {
                p.Hide();
                VehicleWindow viewVehicle = new VehicleWindow();
                viewVehicle.ShowDialog();
                p.ShowDialog();
            });
        }
    }
}
