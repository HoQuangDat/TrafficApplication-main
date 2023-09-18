using giaothong.Model;
using QuanLyShop.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace giaothong.ViewModel
{
    public class TrainingViewModel : BaseViewModel
    {
        private giaothongEntities db;

        private ObservableCollection<KhoaHoc> _listTrainingCourse;
        public ObservableCollection<KhoaHoc> ListTrainingCourse { get => _listTrainingCourse; set => _listTrainingCourse = value; }

        public int pageSize = 1; // Số phần tử trên mỗi trang

        private int _currentPage; // Trang hiện tại
        public int CurrentPage { get => _currentPage; set { _currentPage = value; OnPropertyChanged(); } }

        private int _totalPages;
        public int TotalPages { get => _totalPages; set { _totalPages = value; OnPropertyChanged(); } }

        private string _showPage;
        public string ShowPage { get => _showPage; set { _showPage = value; OnPropertyChanged(); } }

        private KhoaHoc _course;
        public KhoaHoc Course { get => _course; set => _course = value; }

        private string _nameDV;
        public string NameDV { get => _nameDV; set => _nameDV = value; }

        private string _keyword;
        public string Keyword { get => _keyword; set { _keyword = value; OnPropertyChanged(); } }

        private string _dateSearch;
        public string DateSearch
        {
            get => _dateSearch; set
            {
                _dateSearch = value; 
                OnPropertyChanged();
            }
        }

        private static KhoaHoc _selectedItem;
        public static KhoaHoc SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
            }
        }

        private bool _statusFilter;
        public bool StatusFilter { get => _statusFilter; set { _statusFilter = value; OnPropertyChanged(); } }

        private int _selectedStatusIndex;
        public int SelectedStatusIndex
        {
            get => _selectedStatusIndex; set
            {
                _selectedStatusIndex = value;
                OnPropertyChanged();

                StatusFilter = true;

                if (SelectedStatusIndex == 1)
                {
                    StatusFilter = false;
                }

                loadTrainingCourse();
            }
        }


        public ICommand previewMouseLeftButtonUp { get; set; }
        public ICommand nextPage { get; set; }
        public ICommand previousPage { get; set; }
        public ICommand closeCourseWindow { get; set; }
        public ICommand courseWindowLoaded { get; set; }
        public ICommand textChanged { get; set; }
        public ICommand dateChanged { get; set; }

        public TrainingViewModel()
        {
            courseWindowLoaded = new RelayCommand<string>((p) => { return true; }, (p) =>
            {
                Keyword = null;
                DateSearch = null;
                CurrentPage = 1;
                ListTrainingCourse = new ObservableCollection<KhoaHoc>();
                Course = new KhoaHoc();
                StatusFilter = true;

                if(TrainingCenterViewModel.SelectedItem != null)
                {
                    Course.MaCSDT = TrainingCenterViewModel.SelectedItem.MaDV;
                    NameDV = TrainingCenterViewModel.SelectedItem.TenDV;
                }    


                loadTrainingCourse();
            });

            //search course training by name or code
            textChanged = new RelayCommand<string>((p) => { return true; }, (p) =>
            {
                if (Keyword != null)
                {
                    if (Keyword.Length == 0)
                    {
                        Keyword = null;
                    }
                }

                loadTrainingCourse();
            });

            dateChanged = new RelayCommand<string>((p) => { return true; }, (p) =>
            {
                if (DateSearch != null)
                {
                    if (DateSearch.Length == 0)
                    {
                        DateSearch = null;
                    }
                }

                loadTrainingCourse();
            });

            //next page
            nextPage = new RelayCommand<string>((p) => { return true; }, (p) =>
            {
                if (CurrentPage < TotalPages)
                {
                    CurrentPage++;
                    loadTrainingCourse();
                }
            });

            //previous page
            previousPage = new RelayCommand<string>((p) => { return true; }, (p) =>
            {
                if (CurrentPage > 1)
                {
                    CurrentPage--;
                    loadTrainingCourse();
                }
            });

            closeCourseWindow = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });

            previewMouseLeftButtonUp = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                if(SelectedItem != null)
                {
                    p.Hide();
                    ArrangeTeacherWindow arrangeTeacher = new ArrangeTeacherWindow();
                    arrangeTeacher.ShowDialog();

                    p.ShowDialog();
                }    
            });
        }

        public ObservableCollection<KhoaHoc> loadTrainingCourse()
        {
            using (db = new giaothongEntities())
            {
                ListTrainingCourse.Clear();

                var courses = (from c in db.KhoaHocs where c.MaCSDT == Course.MaCSDT && c.TrangThai == StatusFilter select c).OrderBy(p => p.MaKH);

                if (Keyword != null)
                {
                    courses = courses.Where(c => c.TenKH.Contains(Keyword) || c.MaKH.Contains(Keyword)).OrderBy(p => p.MaKH);
                }

                if (DateSearch != null)
                {
                    string day = DateTime.Parse(DateSearch.ToString()).Day.ToString();
                    string month = DateTime.Parse(DateSearch.ToString()).Month.ToString();
                    string year = DateTime.Parse(DateSearch.ToString()).Year.ToString();

                    courses = courses.Where(c => day == c.NgayKG.Value.Day.ToString()
                                            && month == c.NgayKG.Value.Month.ToString()
                                            && year == c.NgayKG.Value.Year.ToString()).OrderBy(p => p.MaKH);
                }

                TotalPages = (int)Math.Ceiling((double)courses.ToList().Count / pageSize);

                if (TotalPages == 0)
                {
                    TotalPages = 1;
                }

                var coursesNew = courses.Skip((CurrentPage - 1) * pageSize).Take(pageSize);

                coursesNew.ToList().ForEach(p =>
                {
                    p.showTrangThai = "Đang giảng dạy";

                    if (!p.TrangThai)
                    {
                        p.showTrangThai = "Kết thúc";
                    }

                    ListTrainingCourse.Add(p);
                });

                getCurrentPage();
                return ListTrainingCourse;
            }
        }

        private string getCurrentPage()
        {
            ShowPage = "Trang " + CurrentPage + "/" + TotalPages;

            return ShowPage;
        }
    }
}
