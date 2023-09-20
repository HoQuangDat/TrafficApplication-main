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

        private ObservableCollection<CaDay> _listOfWorkTime;
        public ObservableCollection<CaDay> ListOfWorkTime { get => _listOfWorkTime; set => _listOfWorkTime = value; }

        private ObservableCollection<SanTap> _listPracticeGround;
        public ObservableCollection<SanTap> ListPracticeGround { get => _listPracticeGround; set => _listPracticeGround = value; }

        private ObservableCollection<XETAPLAI> _listVehicles;
        public ObservableCollection<XETAPLAI> ListVehicles { get => _listVehicles; set => _listVehicles = value; }

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

        private string _sumStudent;
        public string SumStudent { get => _sumStudent; set { _sumStudent = value; OnPropertyChanged(); } }

        private bool _selectedGround;
        public bool SelectedGround { get => _selectedGround; set { _selectedGround = value; OnPropertyChanged(); } }

        private GIAOVIEN _selectedItem;
        public GIAOVIEN SelectedItem { get => _selectedItem; set { _selectedItem = value; OnPropertyChanged(); } }

        private KhoaHoc_GiaoVien _selectedArrangeTeacherItem;
        public KhoaHoc_GiaoVien SelectedArrangeTeacherItem
        {
            get => _selectedArrangeTeacherItem; set
            {
                _selectedArrangeTeacherItem = value;

                if (SelectedArrangeTeacherItem != null)
                {
                    if (SelectedArrangeTeacherItem.SoHV == null)
                    {
                        SelectedArrangeTeacherItem.SoHV = 0;
                    }
                }


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

                if (SelectedWorkTimeItem != null)
                {
                    SelectedGround = true;
                }

                OnPropertyChanged();

            }
        }

        private SanTap _selectedGroundItem;
        public SanTap SelectedGroundItem
        {
            get => _selectedGroundItem;
            set
            {
                _selectedGroundItem = value;


                OnPropertyChanged();

            }
        }

        private XETAPLAI _selectedVehicleItem;
        public XETAPLAI SelectedVehicleItem
        {
            get => _selectedVehicleItem;
            set
            {
                _selectedVehicleItem = value;

                OnPropertyChanged();

            }
        }

        private KhoaHoc _course;
        public KhoaHoc Course { get => _course; set { _course = value; ; OnPropertyChanged(); } }

        public ICommand trainingPracticeWindowLoaded { get; set; }
        public ICommand closeInsertPracticeTeacherWindow { get; set; }
        public ICommand textChanged { get; set; }
        public ICommand previewMouseLeftButtonUp { get; set; }
        public ICommand nextPage { get; set; }
        public ICommand previousPage { get; set; }
        public ICommand previewMouseArrange { get; set; }
        public ICommand updateScheduleButton { get; set; }

        public TrainingPracticeTeacherViewModel()
        {
            updateScheduleButton = new RelayCommand<string>((p) => { return true; }, (p) =>
            {
                if (SelectedWorkTimeItem == null)
                {
                    MessageBox.Show("Vui lòng chọn ca dạy cho khóa học này: ", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                if (SelectedArrangeTeacherItem != null)
                {
                    if (SelectedArrangeTeacherItem.SoHV < 0)
                    {
                        MessageBox.Show("Số học viên không hợp lệ: ", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else if (SelectedArrangeTeacherItem.SoHV > sumStudentOfCourse())
                    {
                        MessageBox.Show("Số học viên vượt quá số lượng đăng ký khóa học: " + sumStudentOfCourse(), "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        updateSchedule();
                        SumStudent = "Số học viên (Còn lại: " + sumStudentOfCourse() + ")";
                    }
                }
            });

            previewMouseArrange = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                if (SelectedArrangeTeacherItem != null)
                {
                    p.Hide();

                    listOfWorkTime();
                    listPracticeGround();
                    listVehicles();

                    if (SelectedArrangeTeacherItem != null)
                    {
                        if (SelectedArrangeTeacherItem.GIAOVIEN.HoDem != null && SelectedArrangeTeacherItem.GIAOVIEN.TenGV != null)
                        {
                            SelectedArrangeTeacherItem.GIAOVIEN.FullName = SelectedArrangeTeacherItem.GIAOVIEN.HoDem + " " + SelectedArrangeTeacherItem.GIAOVIEN.TenGV;
                        }
                        else
                        {
                            SelectedArrangeTeacherItem.GIAOVIEN.FullName = SelectedArrangeTeacherItem.GIAOVIEN.TenGV != null ? SelectedArrangeTeacherItem.GIAOVIEN.TenGV : SelectedArrangeTeacherItem.GIAOVIEN.HoDem;
                        }

                        if (SelectedArrangeTeacherItem.MaCD != null)
                        {
                            SelectedWorkTimeItem = ListOfWorkTime.Where(c => c.MaCD.Trim() == SelectedArrangeTeacherItem.MaCD.Trim()).FirstOrDefault();
                        }

                        if (SelectedArrangeTeacherItem.MaST != null)
                        {
                            SelectedGroundItem = ListPracticeGround.Where(c => c.MaST.Trim() == SelectedArrangeTeacherItem.MaST.Trim()).FirstOrDefault();
                        }

                        if (SelectedArrangeTeacherItem.BienSoXe != null)
                        {
                            SelectedVehicleItem = ListVehicles.Where(c => c.SoDangKy.Trim() == SelectedArrangeTeacherItem.BienSoXe.Trim()).FirstOrDefault();
                        }
                    }

                    SumStudent = "Số học viên (Còn lại: " + sumStudentOfCourse() + ")";

                    ArrangePracticeTeacherWindow arrangePractice = new ArrangePracticeTeacherWindow();
                    arrangePractice.ShowDialog();

                    listTeacherTraining();

                    p.ShowDialog();
                }
            });

            trainingPracticeWindowLoaded = new RelayCommand<string>((p) => { return true; }, (p) =>
            {
                ListTeacher = new ObservableCollection<GIAOVIEN>();
                ListTeacherTraining = new ObservableCollection<KhoaHoc_GiaoVien>();
                ListOfWorkTime = new ObservableCollection<CaDay>();
                ListPracticeGround = new ObservableCollection<SanTap>();
                ListVehicles = new ObservableCollection<XETAPLAI>();

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

        private void updateSchedule()
        {
            using (db = new giaothongEntities())
            {
                try
                {
                    KhoaHoc_GiaoVien data = db.KhoaHoc_GiaoVien.Find(SelectedArrangeTeacherItem.ID);
                    data.MaST = SelectedGroundItem.MaST;
                    data.SoHV = SelectedArrangeTeacherItem.SoHV;
                    data.MaCD = SelectedWorkTimeItem.MaCD;
                    data.NgayBD = SelectedArrangeTeacherItem.NgayBD;
                    data.BienSoXe = SelectedVehicleItem.SoDangKy;

                    db.SaveChanges();

                    MessageBox.Show("Cập nhật thông tin lịch dạy cho giáo viên: " + SelectedArrangeTeacherItem.GIAOVIEN.FullName + " thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                catch
                {
                    MessageBox.Show("Có lỗi xảy ra trong quá trinh cập nhật thông tin lịch dạy cho giáo viên: " + SelectedArrangeTeacherItem.GIAOVIEN.FullName, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private int? sumStudentOfCourse()
        {
            using (db = new giaothongEntities())
            {
                int? sum = 0;

                try
                {
                    int? sumStudentOfCourse = (from c in db.KhoaHocs where SelectedArrangeTeacherItem.MaKH == c.MaKH select c).Sum(p => p.TongSoHV);
                    int? sumStudentLearning = (from c in db.KhoaHoc_GiaoVien where SelectedArrangeTeacherItem.MaKH == c.MaKH && c.GIAOVIEN.GV_TH == true select c).Sum(p => p.SoHV);

                    if (sumStudentLearning == null)
                    {
                        sumStudentLearning = 0;
                    }

                    sum = sumStudentOfCourse - sumStudentLearning;

                    return sum;
                }
                catch
                {
                    return sum;
                }
            }
        }

        private ObservableCollection<XETAPLAI> listVehicles()
        {
            using (db = new giaothongEntities())
            {
                ListVehicles.Clear();

                //get all time work
                var teachs = (from c in db.XETAPLAIs
                              where SelectedArrangeTeacherItem.BienSoXe == db.KhoaHoc_GiaoVien.Where(p => p.BienSoXe == c.SoDangKy
                              && p.MaKH == SelectedArrangeTeacherItem.MaKH).FirstOrDefault().BienSoXe ||
                              (c.SoDangKy != db.KhoaHoc_GiaoVien.Where(p => p.BienSoXe == c.SoDangKy && p.MaKH == SelectedArrangeTeacherItem.MaKH 
                              && p.GIAOVIEN.GV_TH == true).FirstOrDefault().BienSoXe)
                              select c).OrderBy(p => p.SoDangKy);

                teachs.ToList().ForEach(p =>
                {
                    ListVehicles.Add(p);
                });

                return ListVehicles;
            }
        }

        private ObservableCollection<SanTap> listPracticeGround()
        {
            using (db = new giaothongEntities())
            {
                ListPracticeGround.Clear();

                //get all time work
                var teachs = (from c in db.SanTaps
                              where SelectedArrangeTeacherItem.MaST == db.KhoaHoc_GiaoVien.Where(p => p.MaST == c.MaST
                              && p.MaKH == SelectedArrangeTeacherItem.MaKH).FirstOrDefault().MaST ||
                              (c.MaST != db.KhoaHoc_GiaoVien.Where(p => p.MaST == c.MaST && p.MaKH == SelectedArrangeTeacherItem.MaKH && p.GIAOVIEN.GV_TH == true).FirstOrDefault().MaCD)
                              select c).OrderBy(p => p.MaST);

                teachs.ToList().ForEach(p =>
                {
                    ListPracticeGround.Add(p);
                });

                return ListPracticeGround;
            }
        }

        //load list work time
        private ObservableCollection<CaDay> listOfWorkTime()
        {
            using (db = new giaothongEntities())
            {
                ListOfWorkTime.Clear();

                //get all time work
                var teachs = (from c in db.CaDays
                              where SelectedArrangeTeacherItem.MaCD == db.KhoaHoc_GiaoVien.Where(p => p.MaCD == c.MaCD
                              && p.MaKH == SelectedArrangeTeacherItem.MaKH).FirstOrDefault().MaCD ||
                              (c.MaCD != db.KhoaHoc_GiaoVien.Where(p => p.MaCD == c.MaCD && p.MaKH == SelectedArrangeTeacherItem.MaKH && p.GIAOVIEN.GV_TH == true).FirstOrDefault().MaCD)
                              select c).OrderBy(p => p.MaCD);

                teachs.ToList().ForEach(p =>
                {
                    ListOfWorkTime.Add(p);
                });

                return ListOfWorkTime;
            }
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
                    p.CaDay = db.CaDays.Find(p.MaCD);
                    p.SanTap = db.SanTaps.Find(p.MaST);

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
                    if (SelectedItem != null)
                    {
                        data.MaGV = SelectedItem.MaGV;
                    }

                    if (Course != null)
                    {
                        data.MaKH = Course.MaKH;
                    }

                    data.NgayTao = DateTime.Now.Date;
                    data.NgaySua = DateTime.Now.Date;
                    data.TrangThai = true;
                    db.KhoaHoc_GiaoVien.Add(data);
                    db.SaveChanges();
                }
                catch
                {

                }
            }
        }

        public string getCurrentPage()
        {
            ShowPage = "Trang " + CurrentPage + "/" + TotalPages;

            return ShowPage;
        }
    }
}
