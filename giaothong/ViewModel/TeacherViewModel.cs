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
    public class TeacherViewModel : BaseViewModel
    {
        private giaothongEntities db;
        private ObservableCollection<GIAOVIEN> _listTeacher;
        public ObservableCollection<GIAOVIEN> ListTeacher { get => _listTeacher; set => _listTeacher = value; }

        private int _selectedIndex;
        public int SelectedIndex { get => _selectedIndex; set => _selectedIndex = value; }

        private string _searchTeacher;
        public string SearchTeacher { get => _searchTeacher; set => _searchTeacher = value; }

        private string _selectedImage;
        public string SelectedImage { get => _selectedImage; set => _selectedImage = value; }

        public ICommand selectionChanged { get; set; }
        public ICommand textChanged { get; set; }
        public ICommand viewInsertTeacher { get; set; }
        public ICommand closeTeacherWindow { get; set; }
        public ICommand closeInsertTeacherWindow { get; set; }
        public ICommand selectedImage { get; set; }

        public TeacherViewModel()
        {
            ListTeacher = new ObservableCollection<GIAOVIEN>();

            teachers();

            //close view teacher window
            closeTeacherWindow = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });

            //close view insert teacher window
            closeInsertTeacherWindow = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });

            //selectected change filter status teacher
            selectionChanged = new RelayCommand<string>((p) => { return true; }, (p) =>
            {
                var selectedValue = changeSelectedStatus(SelectedIndex);
                teachers(selectedValue);
            });

            //find teacher by ID or CCCD
            textChanged = new RelayCommand<string>((p) => { return true; }, (p) =>
            {
                var selectedValue = changeSelectedStatus(SelectedIndex);
                teachers(selectedValue, SearchTeacher);
            });

            //load view insert teacher 
            viewInsertTeacher = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Hide();
                InsertTeacher inserTeacher = new InsertTeacher();
                inserTeacher.ShowDialog();
                p.ShowDialog();
            });

            //selected image
            selectedImage = new RelayCommand<string>((p) => { return true; }, (p) =>
            {

            });
        }

        //get value with index combobox
        public string changeSelectedStatus(int index)
        {
            var value = "";

            switch(index)
            {
                case 1:
                    value = "Hợp đồng";
                    break;
                case 2:
                    value = "Biên chế";
                    break;
                case 3:
                    value = "Thời vụ";
                    break;
                case 4:
                    value = "Lý thuyết";
                    break;
                case 5:
                    value = "Thực hành";
                    break;
                default: value = "Tất cả";
                    break;
            }

            return value;
        }

        //get list teacher

        public void teachers(string keyword = "tất cả", string searchKey = null)
        {
            using(db = new giaothongEntities())
            {
                try
                {
                    ListTeacher.Clear();

                    if (keyword.ToLower() == "tất cả")
                    {
                        var teachers = from gv in db.GIAOVIENs select gv;

                        if(!string.IsNullOrEmpty(searchKey))
                        {
                            teachers = from gv in db.GIAOVIENs where gv.MaGV == searchKey || gv.SoCCCD == searchKey select gv;
                        }

                        addValueToListTeacher(teachers.ToList());
                    }
                    else if (keyword.ToLower() == "hợp đồng" || keyword.ToLower() == "biên chế" || keyword.ToLower() == "thời vụ")
                    {
                        var teachers = from gv in db.GIAOVIENs where gv.TuyenDung == keyword select gv;

                        if (!string.IsNullOrEmpty(searchKey))
                        {
                            teachers = from gv in db.GIAOVIENs where gv.TuyenDung == keyword && gv.MaGV == searchKey || gv.SoCCCD == searchKey select gv;
                        }

                        addValueToListTeacher(teachers.ToList());
                    }
                    else if (keyword.ToLower() == "lý thuyết")
                    {
                        var teachers = from gv in db.GIAOVIENs where gv.GV_LT == true select gv;

                        if (!string.IsNullOrEmpty(searchKey))
                        {
                            teachers = from gv in db.GIAOVIENs where gv.GV_LT == true && gv.TuyenDung == keyword && gv.MaGV == searchKey || gv.SoCCCD == searchKey select gv;
                        }

                        addValueToListTeacher(teachers.ToList());
                    }
                    else if (keyword.ToLower() == "thực hành")
                    {
                        var teachers = from gv in db.GIAOVIENs where gv.GV_TH == true select gv;

                        if (!string.IsNullOrEmpty(searchKey))
                        {
                            teachers = from gv in db.GIAOVIENs where gv.GV_TH == true && gv.GV_LT == true && gv.TuyenDung == keyword && gv.MaGV == searchKey || gv.SoCCCD == searchKey select gv;
                        }

                        addValueToListTeacher(teachers.ToList());
                    }
                }
                catch { };
            }
        }

        //add value to list teacher
        public void addValueToListTeacher(List<GIAOVIEN> teachers)
        {
            teachers.ToList().ForEach(p =>
            {
                if (p != null)
                {
                    ListTeacher.Add(p);
                }
            });
        }

        

    }
}
