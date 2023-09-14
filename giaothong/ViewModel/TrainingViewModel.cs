using giaothong.Model;
using QuanLyShop.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace giaothong.ViewModel
{
    public class TrainingViewModel : BaseViewModel
    {
        private ObservableCollection<KhoaHoc> _listTrainingCourse;
        public ObservableCollection<KhoaHoc> ListTrainingCourse { get => _listTrainingCourse; set => _listTrainingCourse = value; }

        private string _maDV;
        public string MaDV { get => _maDV; set { _maDV = value; OnPropertyChanged(); } }


        public TrainingViewModel()
        {
            this.MaDV = TrainingCenterViewModel.SelectedItem.MaDV;
        }
    }
}
