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

        private bool _selectedWorkTime;
        public bool SelectedWorkTime { get => _selectedWorkTime; set { _selectedWorkTime = value; OnPropertyChanged(); } }

        private string _sumStudent;
        public string SumStudent { get => _sumStudent; set { _sumStudent = value; OnPropertyChanged(); } }

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
                    SelectedWorkTime = true;
                }

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
        public ICommand updateScheduleButton { get; set; }

        public TrainingTheoryTeacherViewModel()
        {
            SelectedWorkTime = false;

            updateScheduleButton = new RelayCommand<string>((p) => { return true; }, (p) =>
            {
                updateSchedule();
                sumStudentOfCourse();
            });

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
                SelectedWorkTime = false;

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
                        if (checkCourseFullTeacher() == false)
                        {
                            MessageBox.Show("Khóa học: " + Course.TenKH + " đã đủ giáo viên dạy lý thuyết", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        else if(checkTeacherExistsCourse() == false)
                        {
                            MessageBox.Show("Giáo viên: " + SelectedItem.FullName + " đã dạy 2 buổi trong khóa học: " + Course.TenKH, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        else
                        {
                            insertTeacherCourse();
                            listTeacherTraining();
                        }    
                    }

                    SelectedItem = null;
                }
            });

            previewMouseArrange = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                if (SelectedArrangeTeacherItem != null)
                {
                    p.Hide();

                    listOfWorkTime();
                    listRoomWork();

                    if (SelectedArrangeTeacherItem != null)
                    {
                        if (SelectedArrangeTeacherItem.MaCD != null)
                        {
                            SelectedWorkTimeItem = ListOfWorkTime.Where(c => c.MaCD.Trim() == SelectedArrangeTeacherItem.MaCD.Trim()).FirstOrDefault();
                        }

                        if (SelectedArrangeTeacherItem.MaPD != null)
                        {
                            SelectedRoomWorkItem = ListRoomWorkTime.Where(c => c.MaPD.Trim() == SelectedArrangeTeacherItem.MaPD.Trim()).FirstOrDefault();
                        }
                    }

                    sumStudentOfCourse();

                    ArrangeTheoryTeacherWindow arrangeTheory = new ArrangeTheoryTeacherWindow();
                    arrangeTheory.ShowDialog();

                    listTeacherTraining();

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

        private string sumStudentOfCourse()
        {
            using (db = new giaothongEntities())
            {
                try
                {
                    int? sumStudentOfCourse = (from c in db.KhoaHocs where SelectedArrangeTeacherItem.MaKH == c.MaKH select c).Sum(p => p.TongSoHV);
                    int? sumStudentLearning = (from c in db.KhoaHoc_GiaoVien where SelectedArrangeTeacherItem.MaKH == c.MaKH select c).Sum(p => p.SoHV);

                    if(sumStudentLearning == null)
                    {
                        sumStudentLearning = 0;
                    }    

                    int? sum = sumStudentOfCourse - sumStudentLearning;

                    SumStudent = "Số học viên (Còn lại: " + sum + ")";
                }
                catch
                {
                }

                return SumStudent;
            }
        }

        private bool checkCourseFullTeacher()
        {
            using (db = new giaothongEntities())
            {
                try
                {
                    var count = (from c in db.KhoaHoc_GiaoVien where c.MaKH == Course.MaKH && c.GIAOVIEN.GV_LT == true select c).Count();

                    if (count >= 2)
                    {
                        return false;
                    }
                }
                catch
                {
                    return false;
                }

                return true;
            }
        }

        private bool checkTeacherExistsCourse()
        {
            using (db = new giaothongEntities())
            {
                try
                {
                    var count = (from c in db.KhoaHoc_GiaoVien where c.MaGV == SelectedItem.MaGV select c).Count();

                    if (count >= 2)
                    {
                        return false;
                    }
                }
                catch
                {
                    return false;
                }

                return true;
            }
        }

        //update information schedule of teacher
        private void updateSchedule()
        {
            using (db = new giaothongEntities())
            {
                try
                {
                    KhoaHoc_GiaoVien data = db.KhoaHoc_GiaoVien.Find(SelectedArrangeTeacherItem.ID);
                    data.SoHV = SelectedArrangeTeacherItem.SoHV;
                    data.MaCD = SelectedWorkTimeItem.MaCD;
                    data.MaPD = SelectedRoomWorkItem.MaPD;
                    data.NgayBD = SelectedArrangeTeacherItem.NgayBD;

                    db.SaveChanges();

                    MessageBox.Show("Cập nhật thông tin lịch dạy cho giáo viên: " + SelectedArrangeTeacherItem.GIAOVIEN.FullName + " thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch
                {
                    MessageBox.Show("Có lỗi xảy ra trong quá trinh cập nhật thông tin lịch dạy cho giáo viên: " + SelectedArrangeTeacherItem.GIAOVIEN.FullName, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        //get list room
        private ObservableCollection<PhongDay> listRoomWork()
        {
            using (db = new giaothongEntities())
            {
                ListRoomWorkTime.Clear();

                if (SelectedArrangeTeacherItem != null)
                {
                    var rooms = (from c in db.PhongDays
                                 where SelectedArrangeTeacherItem.MaPD == db.KhoaHoc_GiaoVien.Where(p => p.MaKH == SelectedArrangeTeacherItem.MaKH  
                                           && p.MaCD == SelectedArrangeTeacherItem.MaCD
                                           && p.MaPD == p.MaPD).FirstOrDefault().MaPD ||
                                 c.MaPD != db.KhoaHoc_GiaoVien.Where(p => p.MaKH == SelectedArrangeTeacherItem.MaKH
                                            && p.MaGV == SelectedArrangeTeacherItem.MaGV
                                            && p.MaCD == SelectedArrangeTeacherItem.MaCD
                                            && p.MaPD == p.MaPD).FirstOrDefault().MaPD
                                 select c).OrderBy(p => p.MaPD);

                    rooms.ToList().ForEach(p =>
                    {
                        ListRoomWorkTime.Add(p);
                    });
                }

                return ListRoomWorkTime;
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
                              (c.MaCD != db.KhoaHoc_GiaoVien.Where(p => p.MaCD == c.MaCD  && p.MaKH == SelectedArrangeTeacherItem.MaKH).FirstOrDefault().MaCD)
                              select c).OrderBy(p => p.MaCD);

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
                try
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
                        p.CaDay = db.CaDays.Find(p.MaCD);
                        p.PhongDay = db.PhongDays.Find(p.MaPD);
                        ListTeacherTraining.Add(p);
                    });

                    getCurrentPage();
                }
                catch { }

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
