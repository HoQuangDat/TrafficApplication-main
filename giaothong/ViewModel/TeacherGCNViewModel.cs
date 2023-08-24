using giaothong.Model;
using Microsoft.Win32;
using QuanLyShop.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace giaothong.ViewModel
{
    public class TeacherGCNViewModel : BaseViewModel
    {
        private giaothongEntities db;

        private ObservableCollection<GIAOVIEN_GCN> _listGCN;
        public ObservableCollection<GIAOVIEN_GCN> ListGCN { get => _listGCN; set => _listGCN = value; }

        private ObservableCollection<HangXe> _listHangXe;
        public ObservableCollection<HangXe> ListHangXe { get => _listHangXe; set => _listHangXe = value; }


        private GIAOVIEN_GCN _giayCN;
        public GIAOVIEN_GCN GiayCN { get => _giayCN; set { _giayCN = value; OnPropertyChanged(); } }


        private string _searchGCN;
        public string SearchGCN { get => _searchGCN; set { _searchGCN = value; OnPropertyChanged(); } }

        public int pageSize = 10; // Số phần tử trên mỗi trang
        private int _currentPage; // Trang hiện tại
        public int CurrentPage { get => _currentPage; set { _currentPage = value; OnPropertyChanged(); } }

        private int _totalPages;
        public int TotalPages { get => _totalPages; set { _totalPages = value; OnPropertyChanged(); } }

        private string _selectedImage;
        public string SelectedImage { get => _selectedImage; set { _selectedImage = value; OnPropertyChanged(); } }

        private HangXe _selectedIndexHangXe;
        public HangXe SelectedIndexHangXe
        {
            get => _selectedIndexHangXe;
            set
            {
                _selectedIndexHangXe = value;
                OnPropertyChanged();

                if (SelectedIndexHangXe != null)
                {
                    GiayCN.HangXe = SelectedIndexHangXe.ten;
                }
            }
        }


        private GIAOVIEN_GCN _selectedItem;
        public GIAOVIEN_GCN SelectedItem
        {
            get => _selectedItem; set
            {
                _selectedItem = value;
                OnPropertyChanged();
                reset();

                if (SelectedItem != null)
                {
                    GiayCN.SoGCN = SelectedItem.SoGCN.Trim();
                    GiayCN.QDCap = SelectedItem.QDCap.Trim();
                    GiayCN.DonViCap = SelectedItem.DonViCap.Trim();
                    GiayCN.NgayCap = SelectedItem.NgayCap;

                    int index = 0;

                    foreach (var item in ListHangXe)
                    {
                        if (SelectedItem.HangXe != null)
                        {
                            if (item.ten.CompareTo(SelectedItem.HangXe.Trim()) == 0)
                            {
                                SelectedIndexHangXe = ListHangXe[index];
                                break;
                            }
                            index++;

                        }
                    }
                    if (SelectedItem.HangXe != null)
                    {
                        GiayCN.HangXe = SelectedItem.HangXe.Trim();
                    }

                    GiayCN.MaDonViTapHuan = SelectedItem.MaDonViTapHuan.Trim();
                    GiayCN.NgayKiemTra = SelectedItem.NgayKiemTra;
                    GiayCN.MaDVKiemTra = SelectedItem.MaDVKiemTra.Trim();
                    GiayCN.AnhGCN = SelectedItem.AnhGCN.Trim();
                    SelectedImage = SelectedItem.AnhGCN.Trim();
                }
            }
        }

        public ICommand closeTeacherWindow { get; set; }
        public ICommand textChanged { get; set; }
        public ICommand nextPage { get; set; }
        public ICommand previousPage { get; set; }
        public ICommand viewInsertGCN { get; set; }
        public ICommand insertGCN { get; set; }
        public ICommand editGCN { get; set; }
        public ICommand selectedImage { get; set; }
        public ICommand previewMouseLeftButtonUp { get; set; }
        public ICommand removeGCN { get; set; }

        public TeacherGCNViewModel()
        {
            ListGCN = new ObservableCollection<GIAOVIEN_GCN>();
            ListHangXe = new ObservableCollection<HangXe>();
            GiayCN = new GIAOVIEN_GCN();
            GiayCN.NgayCap = DateTime.Now;
            GiayCN.NgayKiemTra = DateTime.Now;
            listGCN();
            listHangXe();

            //close view teacher window
            closeTeacherWindow = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
                SelectedItem = null;
            });

            //view insert gcn
            viewInsertGCN = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Hide();
                getMaxMaGV();
                reset();
                getMaxMaGV();
                GiayCN.NgayCap = DateTime.Now;
                GiayCN.NgayKiemTra = DateTime.Now;
                SelectedImage = null;

                InsertTeacherGCNWindow teacherGCN = new InsertTeacherGCNWindow();
                teacherGCN.ShowDialog();
                p.ShowDialog();
            });

            //open window edit teacher
            previewMouseLeftButtonUp = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                if (SelectedItem != null)
                {
                    p.Hide();
                    EditTeacherGCNWindow teacherGCN = new EditTeacherGCNWindow();
                    teacherGCN.ShowDialog();
                    p.ShowDialog();
                }
            });

            //find GCN by ID
            textChanged = new RelayCommand<string>((p) => { return true; }, (p) =>
            {
                listGCN(SearchGCN);
            });

            //next page
            nextPage = new RelayCommand<string>((p) => { return true; }, (p) =>
            {
                if (CurrentPage < TotalPages)
                {
                    CurrentPage++;
                    listGCN(SearchGCN);
                }
            });

            //previous page
            previousPage = new RelayCommand<string>((p) => { return true; }, (p) =>
            {
                if (CurrentPage > 1)
                {
                    CurrentPage--;
                    listGCN(SearchGCN);
                }
            });

            //insert gcn 
            insertGCN = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                GiayCN.NgayCapNhat = DateTime.Now;

                bool check = validation();

                if (check)
                {
                    using (db = new giaothongEntities())
                    {
                        try
                        {
                            db.GIAOVIEN_GCN.Add(GiayCN);
                            db.SaveChanges();
                            listGCN();
                            p.Close();
                        }
                        catch
                        {
                            MessageBox.Show("Thêm giấy chứng nhận thất bại", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            });

            //edit gcn 
            editGCN = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {

                bool check = validation();

                if (check)
                {
                    using (db = new giaothongEntities())
                    {
                        try
                        {
                            var gcn = db.GIAOVIEN_GCN.Find(GiayCN.SoGCN);

                            var checkFile = GiayCN.AnhGCN.Contains("giaothong");

                            if (checkFile)
                            {
                                var index = GiayCN.AnhGCN.IndexOf("Images");
                                GiayCN.AnhGCN = GiayCN.AnhGCN.Remove(0, index - 1);
                            }

                            gcn.AnhGCN = GiayCN.AnhGCN;
                            gcn.QDCap = GiayCN.QDCap;
                            gcn.DonViCap = GiayCN.DonViCap;
                            gcn.NgayCap = GiayCN.NgayCap;
                            gcn.NgayKiemTra = GiayCN.NgayKiemTra;
                            gcn.HangXe = GiayCN.HangXe;
                            gcn.MaDonViTapHuan = GiayCN.MaDonViTapHuan;
                            gcn.MaDVKiemTra = GiayCN.MaDVKiemTra;
                            gcn.NgayCapNhat = DateTime.Now;

                            db.SaveChanges();
                            listGCN();
                            p.Close();

                        }
                        catch
                        {
                            MessageBox.Show("Chỉnh sửa giấy chứng nhận thất bại", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            });

            //remove gcn
            removeGCN = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                using (db = new giaothongEntities())
                {
                    try
                    {
                        var message = MessageBox.Show("Bạn có thật sự muốn xóa giáo viên này khỏi danh sách ?", "Thông Báo", MessageBoxButton.OKCancel, MessageBoxImage.Question);

                        if (MessageBoxResult.OK == message)
                        {
                            var gcn = db.GIAOVIEN_GCN.Find(GiayCN.SoGCN.Trim());
                            if (gcn != null)
                            {
                                db.GIAOVIEN_GCN.Remove(gcn);

                                var index = ListGCN.ToList().FindIndex(t => t.SoGCN.Trim() == GiayCN.SoGCN.Trim());
                                ListGCN.RemoveAt(index);

                                //find teacher has gcn update = null
                                var teacher = (from c in db.GIAOVIENs where c.SoGCN.Trim() == gcn.SoGCN.Trim() select c).FirstOrDefault();

                                if (teacher != null)
                                {
                                    teacher.SoGCN = null;
                                    teacher.NgayCapNhat = DateTime.Now;
                                }

                                db.SaveChanges();

                                MessageBox.Show("Giấy chứng nhận đã được xóa khỏi danh sách", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Information);
                                p.Close();
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Không thể xóa Giấy chứng nhận lúc này", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            });

            //selected image
            selectedImage = new RelayCommand<string>((p) => { return true; }, (p) =>
            {
                try
                {
                    var dateNow = DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second;
                    var url = getUrlSelectedImage();
                    var nameFile = Path.GetFileName(url);
                    var exFile = nameFile.Split('.')[1];
                    nameFile = dateNow + "." + exFile;

                    string currentDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
                    var index = currentDirectory.IndexOf("giaothong");
                    var newCurrentUrl = currentDirectory.Substring(0, index) + @"giaothong\giaothong\images\teacher_gcn";

                    string filePath = Path.Combine(newCurrentUrl, nameFile);

                    if (!File.Exists(filePath))
                    {
                        newCurrentUrl = newCurrentUrl + "\\" + nameFile;
                        File.Copy(url, newCurrentUrl);
                    }

                    var indexCurrentUrl = newCurrentUrl.IndexOf("images");

                    SelectedImage = newCurrentUrl;

                    GiayCN.AnhGCN = "/Images/teacher_gcn/" + nameFile;
                }
                catch { }
            });
        }

        //validate information teacher
        public bool validation()
        {
            bool check = true;

            if (!checkLength(GiayCN.QDCap, 15))
            {
                check = false;
                MessageBox.Show("Số quyết định cấp giấy chứng nhnaj không hợp lệ hoặc phải dưới 15 ký tự!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (!checkLength(GiayCN.DonViCap, 5))
            {
                check = false;
                MessageBox.Show("Mã sở GTVT không hợp lệ hoặc phải dưới 5 ký tự!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (GiayCN.NgayCap.Value == null)
            {
                check = false;
                MessageBox.Show("Ngày cấp giấy chứng nhận không hợp lệ!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (!checkLength(GiayCN.MaDonViTapHuan, 5))
            {
                check = false;
                MessageBox.Show("Mã đơn vị tập huấn không hợp lệ hoặc phải dưới 5 ký tự!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (!checkLength(GiayCN.MaDVKiemTra, 10))
            {
                check = false;
                MessageBox.Show("Mã đơn vị tổ chức kiểm tra không hợp lệ hoặc phải dưới 5 ký tự!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (SelectedImage == null)
            {
                check = false;
                MessageBox.Show("Vui lòng thêm ảnh giấy chứng nhận!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return check;
        }

        //reset property
        public void reset()
        {
            GiayCN = new GIAOVIEN_GCN();
        }

        //check length avaliable
        public bool checkLength(string value, int length)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            if (value.Length > length)
            {
                return false;
            }

            return true;
        }

        //selected image get url
        public string getUrlSelectedImage()
        {
            string url = "";

            OpenFileDialog file = new OpenFileDialog();

            if (file.ShowDialog() == true)
            {
                url = file.FileName;
            }
            else
            {
                MessageBox.Show("Đường Dẫn Không Hợp Lệ", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            return url;
        }

        //get GCN max
        public GIAOVIEN_GCN getMaxMaGV()
        {
            var yearNow = DateTime.Now.Year;

            if (ListGCN.Count > 0)
            {
                var maxGCN = ListGCN.Max(p => p.SoGCN).Trim();
                var arrGCN = maxGCN.Split('/');

                if (arrGCN.Length > 0)
                {
                    GiayCN.SoGCN = (Int32.Parse(arrGCN[0]) + 1) + "/" + yearNow + "/" + "CM";
                }
            }
            else
            {
                GiayCN.SoGCN = 1 + "/" + yearNow + "/" + "CM";
            }

            return GiayCN;
        }

        //get list hang xe
        public ObservableCollection<HangXe> listHangXe()
        {
            using (db = new giaothongEntities())
            {
                ListHangXe.Clear();

                try
                {
                    var hangxes = from c in db.HangXes select c;

                    hangxes.ToList().ForEach(p =>
                    {
                        ListHangXe.Add(p);
                    });
                }
                catch { }

                return ListHangXe;
            }
        }

        //get list teacher GCN
        public ObservableCollection<GIAOVIEN_GCN> listGCN(string searchKey = null)
        {
            using (db = new giaothongEntities())
            {
                ListGCN.Clear();

                try
                {
                    var gcn = from c in db.GIAOVIEN_GCN select c;

                    if (!string.IsNullOrEmpty(searchKey))
                    {
                        gcn = from c in db.GIAOVIEN_GCN where c.SoGCN.Equals(searchKey) select c;
                    }

                    addValueToListGCN(gcn.ToList());

                }
                catch { }

                return ListGCN;
            }
        }

        //add value to list gcn
        public void addValueToListGCN(List<GIAOVIEN_GCN> gcn)
        {
            TotalPages = (int)Math.Ceiling((double)gcn.Count / pageSize);

            var list = gcn.Skip((CurrentPage - 1) * pageSize).Take(pageSize);
            string currentDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            var index = currentDirectory.IndexOf("giaothong");
            var newCurrentUrl = currentDirectory.Substring(0, index) + @"giaothong\giaothong";

            list.ToList().ForEach(p =>
            {
                if (p != null)
                {
                    p.AnhGCN = newCurrentUrl + p.AnhGCN;
                    ListGCN.Add(p);
                }
            });
        }
    }
}
