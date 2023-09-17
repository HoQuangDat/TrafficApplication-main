using giaothong.DTO;
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
    class TrainingCenterViewModel : BaseViewModel
    {
        private giaothongEntities db;

        private ObservableCollection<TrainingDTO> _listCenterTraining;
        public ObservableCollection<TrainingDTO> ListCenterTraining { get => _listCenterTraining; set => _listCenterTraining = value; }

        public int pageSize = 13; // Số phần tử trên mỗi trang

        private int _currentPage; // Trang hiện tại
        public int CurrentPage { get => _currentPage; set { _currentPage = value; OnPropertyChanged(); } }

        private int _totalPages;
        public int TotalPages { get => _totalPages; set { _totalPages = value; OnPropertyChanged(); } }

        private bool _statusSelected;
        public bool StatusSelected { get => _statusSelected; set { _statusSelected = value; OnPropertyChanged(); } }

        private string _showPage;
        public string ShowPage { get => _showPage; set { _showPage = value; OnPropertyChanged(); } }

        private string _keyword;
        public string Keyword { get => _keyword; set { _keyword = value; OnPropertyChanged(); } }

        private int _selectedStatusIndex;
        public int SelectedStatusIndex
        {
            get => _selectedStatusIndex;
            set
            {
                _selectedStatusIndex = value;
                OnPropertyChanged();

                if (SelectedStatusIndex == 0)
                {
                    StatusSelected = true;
                    loadCenterTraining(StatusSelected);
                }
                else
                {
                    StatusSelected = false;
                    loadCenterTraining(StatusSelected);
                }
            }
        }

        private static TrainingDTO _selectedItem;
        public static TrainingDTO SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value; 
            }
        }

        public ICommand nextPage { get; set; }
        public ICommand previousPage { get; set; }
        public ICommand textChanged { get; set; }
        public ICommand previewMouseLeftButtonUp { get; set; }
        public ICommand closeTrainingCenterWindow { get; set; }
        public ICommand trainingWindowLoaded { get; set; }

        public TrainingCenterViewModel()
        {
            trainingWindowLoaded = new RelayCommand<string>((p) => { return true; }, (p) =>
            {
                ListCenterTraining = new ObservableCollection<TrainingDTO>();

                StatusSelected = true;
                Keyword = null;
                CurrentPage = 1;

                loadCenterTraining(StatusSelected);
            });

            closeTrainingCenterWindow = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });

            //next page
            nextPage = new RelayCommand<string>((p) => { return true; }, (p) =>
            {
                if (CurrentPage < TotalPages)
                {
                    CurrentPage++;
                    loadCenterTraining(StatusSelected);
                }
            });

            //previous page
            previousPage = new RelayCommand<string>((p) => { return true; }, (p) =>
            {
                if (CurrentPage > 1)
                {
                    CurrentPage--;
                    loadCenterTraining(StatusSelected);
                }
            });

            //search center training
            textChanged = new RelayCommand<string>((p) => { return true; }, (p) =>
            {
            
                if(Keyword.Length == 0)
                {
                    Keyword = null;
                }

                loadCenterTraining(StatusSelected);
            });

            previewMouseLeftButtonUp = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                if(SelectedItem != null)
                {
                    p.Hide();

                    TrainingWindow train = new TrainingWindow();
                    train.ShowDialog();

                    p.ShowDialog();
                }    
            });

        }

        private ObservableCollection<TrainingDTO> loadCenterTraining(bool selected)
        {
            using (db = new giaothongEntities())
            {
                ListCenterTraining.Clear();

                var centers = (from dv in db.DM_DonViGTVT select dv).Where(i => i.TrangThai == selected).OrderBy(p => p.MaDV).ToList();

                if (Keyword != null)
                {
                    centers = (from dv in db.DM_DonViGTVT select dv).Where(i => i.MaDV == Keyword || i.TenDV == Keyword).OrderBy(p => p.MaDV).ToList();
                }

                var course = (from kh in db.KhoaHocs
                              group kh by kh.MaCSDT into k
                              select new
                              {
                                  MaDV = k.Key,
                                  TongSoHV = k.Sum(i => i.TongSoHV),
                              }).ToList();

                TotalPages = (int)Math.Ceiling((double)centers.Count / pageSize);
                if(TotalPages == 0)
                {
                    TotalPages = 1;
                }
                var listcenter = centers.Skip((CurrentPage - 1) * pageSize).Take(pageSize);

                listcenter.ToList().ForEach(p =>
                {
                    TrainingDTO train = new TrainingDTO();
                    train.MaDV = p.MaDV;
                    train.TenDV = p.TenDV != null ? p.TenDV : "";
                    train.DiaChi = p.DiaChi != null ? p.DiaChi : "";
                    train.LuuLuongDT = p.LuuLuongDT != null ? Int32.Parse(p.LuuLuongDT.ToString()) : 0;

                    course.ForEach(i =>
                    {
                        if (i.MaDV == p.MaDV)
                        {
                            train.TongSoHV = Int32.Parse(i.TongSoHV.ToString());
                        }
                    });

                    if (train.TongSoHV <= train.LuuLuongDT && p.TrangThai)
                    {
                        train.TrangThai = "Được phép mở";
                    }
                    else
                    {
                        train.TrangThai = "Không được phép";
                    }

                    ListCenterTraining.Add(train);
                });

                getCurrentPage();
                return ListCenterTraining;
            }
        }

        public string getCurrentPage()
        {
            ShowPage = "Trang " + CurrentPage + "/" + TotalPages;

            return ShowPage;
        }
    }
}
