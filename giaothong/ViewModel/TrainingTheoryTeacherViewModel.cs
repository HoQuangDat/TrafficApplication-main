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
    class TrainingTheoryTeacherViewModel : BaseViewModel
    {
        private giaothongEntities db;

        private ObservableCollection<GIAOVIEN> _listTeacher;
        public ObservableCollection<GIAOVIEN> ListTeacher { get => _listTeacher; set => _listTeacher = value; }

        private ObservableCollection<CaDay> _listOfWorkTime;
        public ObservableCollection<CaDay> ListOfWorkTime { get => _listOfWorkTime; set => _listOfWorkTime = value; }

        private ObservableCollection<PhongDay> _listRoomWorkTime;
        public ObservableCollection<PhongDay> ListRoomWorkTime { get => _listRoomWorkTime; set => _listRoomWorkTime = value; }

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


        private KhoaHoc _course;
        public KhoaHoc Course { get => _course; set { _course = value; ; OnPropertyChanged(); } }

        private GIAOVIEN _selectedItem;
        public GIAOVIEN SelectedItem { get => _selectedItem; set { _selectedItem = value; OnPropertyChanged(); } }

        private KhoaHoc_GiaoVien _selectedArrangeTeacherItem;
        public KhoaHoc_GiaoVien SelectedArrangeTeacherItem
        {
            get => _selectedArrangeTeacherItem; set
            {
                _selectedArrangeTeacherItem = value;
                OnPropertyChanged();
            }
        }

        private CaDay _selectedWorkTimeItem;
        public CaDay SelectedWorkTimeItem
        {
            get => _selectedWorkTimeItem;
            set
            {
                _selectedWorkTimeItem = value;
                OnPropertyChanged();

            }
        }

        private PhongDay _selectedRoomWorkItem;
        public PhongDay SelectedRoomWorkItem
        {
            get => _selectedRoomWorkItem;
            set
            {
                _selectedRoomWorkItem = value;
                OnPropertyChanged();

            }
        }

        public ICommand trainingTheoryWindowLoaded { get; set; }
        public ICommand closeInsertTheoryTeacherWindow { get; set; }
        public ICommand textChanged { get; set; }
        public ICommand previewMouseLeftButtonUp { get; set; }
        public ICommand previewMouseArrange { get; set; }
        public ICommand nextPage { get; set; }
        public ICommand previousPage { get; set; }

        public TrainingTheoryTeacherViewModel()
        {
            trainingTheoryWindowLoaded = new RelayCommand<string>((p) => { return true; }, (p) =>
            {
                ListTeacher = new ObservableCollection<GIAOVIEN>();
                ListTeacherTraining = new ObservableCollection<KhoaHoc_GiaoVien>();
                ListOfWorkTime = new ObservableCollection<CaDay>();
                ListRoomWorkTime = new ObservableCollection<PhongDay>();

                Course = TrainingViewModel.SelectedItem;
                CurrentPage = 1;
                Title = "THÊM GIÁO VIÊN DẠY LÝ THUYẾT KHÓA HỌC: " + Course.MaKH;

                listTeacher();
                listTeacherTraining();
            });

            closeInsertTheoryTeacherWindow = new RelayCommand<Window>((p) => { return true; }, (p) =>
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

            previewMouseArrange = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                if (SelectedArrangeTeacherItem != null)
                {
                    p.Hide();
                    listOfWorkTime();
                    listRoomWork();

                    ArrangeTheoryTeacherWindow arrangeTheory = new ArrangeTheoryTeacherWindow();
                    arrangeTheory.ShowDialog();

                    p.ShowDialog();
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

        private ObservableCollection<PhongDay> listRoomWork()
        {
            using (db = new giaothongEntities())
            {
                ListRoomWorkTime.Clear();

                var rooms = (from c in db.PhongDays where db.KhoaHoc_GiaoVien.Any(p => p.MaPD != c.MaPD && SelectedArrangeTeacherItem.MaGV != p.MaGV) select c).OrderBy(p => p.MaPD);

                rooms.ToList().ForEach(p =>
                {
                    ListRoomWorkTime.Add(p);
                });

                return ListRoomWorkTime;
            }
        }

        //load list work time
        private ObservableCollection<CaDay> listOfWorkTime()
        {
            using (db = new giaothongEntities())
            {
                ListOfWorkTime.Clear();

                var teachs = (from c in db.CaDays where db.KhoaHoc_GiaoVien.Any(p => p.MaCD != c.MaCD && SelectedArrangeTeacherItem.MaGV != p.MaGV) select c).OrderBy(p => p.MaCD);

                teachs.ToList().ForEach(p =>
                {
                    ListOfWorkTime.Add(p);
                });

                return ListOfWorkTime;
            }
        }

        //load all teachers in table
        private ObservableCollection<GIAOVIEN> listTeacher()
        {
            using (db = new giaothongEntities())
            {
                ListTeacher.Clear();

                var teachers = (from c in db.GIAOVIENs where c.GV_LT == true select c).OrderBy(p => p.MaGV);

                if (Keyword != null)
                {
                    teachers = teachers.Where(p => p.TenGV.Trim().Contains(Keyword.Trim()) || p.MaGV.Trim().Contains(Keyword.Trim())).OrderBy(p => p.MaGV);
                }

                teachers.ToList().ForEach(p =>
                {
                    p.FullName = p.HoDem + " " + p.TenGV;
                    p.Training = "Lý thuyết";

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

                var teachers = (from c in db.KhoaHoc_GiaoVien where c.MaKH == Course.MaKH && c.GIAOVIEN.GV_LT == true select c).OrderBy(p => p.ID).ToList();

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
