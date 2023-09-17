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
    class TrainingPracticeTeacherViewModel : BaseViewModel
    {
        private giaothongEntities db;

        private ObservableCollection<GIAOVIEN> _listTeacher;
        public ObservableCollection<GIAOVIEN> ListTeacher { get => _listTeacher; set => _listTeacher = value; }


        private ObservableCollection<KhoaHoc_GiaoVien> _listTeacherTrainin;
        public ObservableCollection<KhoaHoc_GiaoVien> ListTeacherTraining { get => _listTeacherTrainin; set => _listTeacherTrainin = value; }

        private string _title;
        public string Title { get => _title; set { _title = value; ; OnPropertyChanged(); } }

        private string _keyword;
        public string Keyword { get => _keyword; set { _keyword = value; OnPropertyChanged(); } }

        public int pageSize = 10; // Số phần tử trên mỗi trang

        private int _currentPage; // Trang hiện tại
        public int CurrentPage { get => _currentPage; set { _currentPage = value; OnPropertyChanged(); } }

        private int _totalPages;
        public int TotalPages { get => _totalPages; set { _totalPages = value; OnPropertyChanged(); } }

        private string _showPage;
        public string ShowPage { get => _showPage; set { _showPage = value; OnPropertyChanged(); } }

        private GIAOVIEN _selectedItem;
        public GIAOVIEN SelectedItem { get => _selectedItem; set { _selectedItem = value; OnPropertyChanged(); } }

        private KhoaHoc _course;
        public KhoaHoc Course { get => _course; set { _course = value; ; OnPropertyChanged(); } }
        

        public ICommand trainingPracticeWindowLoaded { get; set; }
        public ICommand closeInsertPracticeTeacherWindow { get; set; }
        public ICommand textChanged { get; set; }
        public ICommand previewMouseLeftButtonUp { get; set; }
        public ICommand nextPage { get; set; }
        public ICommand previousPage { get; set; }

        public TrainingPracticeTeacherViewModel()
        {
            trainingPracticeWindowLoaded = new RelayCommand<string>((p) => { return true; }, (p) =>
            {
                ListTeacher = new ObservableCollection<GIAOVIEN>();
                ListTeacherTraining = new ObservableCollection<KhoaHoc_GiaoVien>();

                Course = TrainingViewModel.SelectedItem;
                CurrentPage = 1;
                Title = "THÊM GIÁO VIÊN DẠY THỰC HÀNH KHÓA HỌC: " + Course.MaKH;

                listTeacher();
                listTeacherTraining();
            });

            closeInsertPracticeTeacherWindow = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });

            textChanged = new RelayCommand<string>((p) => { return true; }, (p) =>
            {
                if (Keyword != null)
                {
                    if (Keyword.Length == 0)
                    {
                        Keyword = null;
                    }
                }

                listTeacher();
            });

            previewMouseLeftButtonUp = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                if (SelectedItem != null)
                {
                    MessageBoxResult messageBox = MessageBox.Show("Bạn có muốn thêm giáo viên: " + SelectedItem.FullName + " giảng dạy khóa học: " + Course.TenKH, "Thông báo", MessageBoxButton.OKCancel, MessageBoxImage.Question);

                    if (MessageBoxResult.OK == messageBox)
                    {
                        insertTeacherCourse();
                        listTeacherTraining();
                    }
                }

            });

            //next page
            nextPage = new RelayCommand<string>((p) => { return true; }, (p) =>
            {
                if (CurrentPage < TotalPages)
                {
                    CurrentPage++;
                    listTeacherTraining();
                }
            });

            //previous page
            previousPage = new RelayCommand<string>((p) => { return true; }, (p) =>
            {
                if (CurrentPage > 1)
                {
                    CurrentPage--;
                    listTeacherTraining();
                }
            });
        }


        private ObservableCollection<GIAOVIEN> listTeacher()
        {
            using (db = new giaothongEntities())
            {
                ListTeacher.Clear();

                var teachers = (from c in db.GIAOVIENs where c.GV_TH == true select c).OrderBy(p => p.MaGV);

                if (Keyword != null)
                {
                    teachers = teachers.Where(p => p.TenGV.Trim().Contains(Keyword.Trim()) || p.MaGV.Trim().Contains(Keyword.Trim())).OrderBy(p => p.MaGV);
                }

                teachers.ToList().ForEach(p =>  
                {
                    p.FullName = p.HoDem + " " + p.TenGV;
                    p.Training = "Thực hành";

                    ListTeacher.Add(p);
                });

                return ListTeacher;
            }
        }

        private ObservableCollection<KhoaHoc_GiaoVien> listTeacherTraining()
        {
            using (db = new giaothongEntities())
            {
                ListTeacherTraining.Clear();

                var teachers = (from c in db.KhoaHoc_GiaoVien where c.MaKH == Course.MaKH && c.GIAOVIEN.GV_TH == true select c).OrderBy(p => p.ID).ToList();

                TotalPages = (int)Math.Ceiling((double)teachers.Count / pageSize);

                if (TotalPages == 0)
                {
                    TotalPages = 1;
                }

                var listcenter = teachers.Skip((CurrentPage - 1) * pageSize).Take(pageSize);

                listcenter.ToList().ForEach(p =>
                {
                    p.GIAOVIEN.FullName = p.GIAOVIEN.HoDem + " " + p.GIAOVIEN.TenGV;
                    p.KhoaHoc.HangDT = Course.HangDT;
                    ListTeacherTraining.Add(p);
                });

                getCurrentPage();

                return ListTeacherTraining;
            }
        }

        private void insertTeacherCourse()
        {
            using (db = new giaothongEntities())
            {
                try
                {
                    KhoaHoc_GiaoVien data = new KhoaHoc_GiaoVien();
                    data.MaGV = SelectedItem.MaGV;
                    data.MaKH = Course.MaKH;
                    data.NgayTao = DateTime.Now.Date;
                    data.NgaySua = DateTime.Now.Date;
                    data.TrangThai = true;
                    db.KhoaHoc_GiaoVien.Add(data);
                    db.SaveChanges();
                }
                catch { }
            }
        }

        public string getCurrentPage()
        {
            ShowPage = "Trang " + CurrentPage + "/" + TotalPages;

            return ShowPage;
        }
    }
}
