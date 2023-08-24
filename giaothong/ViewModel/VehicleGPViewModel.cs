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
using System.Windows.Controls;
using System.Windows.Input;

namespace giaothong.ViewModel
{
    public class VehicleGPViewModel : BaseViewModel
    {
        private giaothongEntities db;
        private ObservableCollection<XETAPLAI_GP> _listGPXTL;
        public ObservableCollection<XETAPLAI_GP> ListGPXTL { get => _listGPXTL; set => _listGPXTL = value; }

        private XETAPLAI_GP _giayPXTL;
        public XETAPLAI_GP GiayPXTL { get => _giayPXTL; set { _giayPXTL = value; OnPropertyChanged(); } }

        private string _searchGPXTL;
        public string SearchGPXTL { get => _searchGPXTL; set { _searchGPXTL = value; OnPropertyChanged(); } }

        public int pageSize = 10; // Số phần tử trên mỗi trang
        private int _currentPage; // Trang hiện tại
        public int CurrentPage { get => _currentPage; set { _currentPage = value; OnPropertyChanged(); } }
        private int _totalPages;
        public int TotalPages { get => _totalPages; set { _totalPages = value; OnPropertyChanged(); } }

        private string _selectedAnhGP;
        public string SelectedAnhGP { get => _selectedAnhGP; set { _selectedAnhGP = value; OnPropertyChanged(); } }

        private string _selectedAnhDKiem;
        public string SelectedAnhDKiem { get => _selectedAnhDKiem; set { _selectedAnhDKiem = value; OnPropertyChanged(); } }

        private string _selectedAnhDKy;
        public string SelectedAnhDKy { get => _selectedAnhDKy; set { _selectedAnhDKy = value; OnPropertyChanged(); } }

        private XETAPLAI_GP _selectedItem;
        public XETAPLAI_GP SelectedItem
        {
            get => _selectedItem; set
            {
                _selectedItem = value;
                OnPropertyChanged();
                reset();

                if (SelectedItem != null)
                {
                    GiayPXTL.SoDangKy = SelectedItem.SoDangKy.Trim();
                    GiayPXTL.SoGPXTL = SelectedItem.SoGPXTL.Trim();
                    GiayPXTL.MaDonViCap = SelectedItem.MaDonViCap.Trim();
                    GiayPXTL.MaCSDT = SelectedItem.MaCSDT.Trim();
                    GiayPXTL.XeHopDong = SelectedItem.XeHopDong.Value;
                    GiayPXTL.NgayCap = SelectedItem.NgayCap;
                    GiayPXTL.NgayHH = SelectedItem.NgayHH;
                    GiayPXTL.DuongTapLai = SelectedItem.DuongTapLai.Trim();
                    GiayPXTL.AnhGP = SelectedItem.AnhGP.Trim();
                    SelectedAnhGP = SelectedItem.AnhGP.Trim();
                    GiayPXTL.AnhDKiem = SelectedItem.AnhDKiem.Trim();
                    SelectedAnhDKiem = SelectedItem.AnhDKiem.Trim();
                    GiayPXTL.AnhDky = SelectedItem.AnhDky.Trim();
                    SelectedAnhDKy = SelectedItem.AnhDky.Trim();
                }
            }
        }

        private bool _isCheckedXeHopDong;
        public bool IsCheckedXeHopDong
        {
            get => _isCheckedXeHopDong;
            set
            {
                _isCheckedXeHopDong = value;
                OnPropertyChanged();

                if (IsCheckedXeHopDong)
                {
                    GiayPXTL.XeHopDong = true;
                }
                else
                {
                    GiayPXTL.XeHopDong = false;
                }
            }
        }


        public ICommand closeVehiceGPWindow { get; set; }
        public ICommand closeInsertVehiceGPWindow { get; set; }
        public ICommand viewInsertGPXTL { get; set; }
        public ICommand previewMouseLeftButtonUp { get; set; }
        public ICommand textChanged { get; set; }
        public ICommand nextPage { get; set; }
        public ICommand previousPage { get; set; }
        public ICommand insertGPXTL { get; set; }
        public ICommand removeGPXTL { get; set; }
        public ICommand selectedAnhGP { get; set; }
        public ICommand selectedAnhDKiem { get; set; }
        public ICommand selectedAnhDKy { get; set; }
        public ICommand editGPXTL { get; set; }
        

        public VehicleGPViewModel()
        {
            ListGPXTL = new ObservableCollection<XETAPLAI_GP>();
            GiayPXTL = new XETAPLAI_GP();
            GiayPXTL.NgayCap = DateTime.Now;
            GiayPXTL.NgayHH = DateTime.Now;

            listGPXTL();

            //close view vehicleGP window
            closeVehiceGPWindow = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
                SelectedItem = null;
            });
            //close insertvehicleGP window
            closeInsertVehiceGPWindow = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });

            //open window edit vehicle
            previewMouseLeftButtonUp = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                if (SelectedItem != null)
                {
                    p.Hide();
                    EditVehicleGPWindow vehicleGPXTL = new EditVehicleGPWindow();
                    vehicleGPXTL.ShowDialog();
                    p.ShowDialog();
                }
            });

            //view insert gpxtl
            viewInsertGPXTL = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Hide();
                getMaxSoGPXTL();
                reset();
                GiayPXTL.NgayCap = DateTime.Now;
                GiayPXTL.NgayHH = DateTime.Now;
                SelectedAnhDKiem = null;
                SelectedAnhDKy = null;
                SelectedAnhGP = null;

                InsertVehicleGPWindow vehicleGP = new InsertVehicleGPWindow();
                vehicleGP.ShowDialog();
                p.ShowDialog();
            });

            //find GPXTL by ID
            textChanged = new RelayCommand<string>((p) => { return true; }, (p) =>
            {
                listGPXTL(SearchGPXTL);
            });

            //next page
            nextPage = new RelayCommand<string>((p) => { return true; }, (p) =>
            {
                if (CurrentPage < TotalPages)
                {
                    CurrentPage++;
                    listGPXTL(SearchGPXTL);
                }
            });

            //previous page
            previousPage = new RelayCommand<string>((p) => { return true; }, (p) =>
            {
                if (CurrentPage > 1)
                {
                    CurrentPage--;
                    listGPXTL(SearchGPXTL);
                }
            });

            //insert gpxtl 
            insertGPXTL = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                GiayPXTL.NgayCapNhat = DateTime.Now;

                bool check = validation();

                if (check)
                {
                    using (db = new giaothongEntities())
                    {
                        try
                        {
                            db.XETAPLAI_GP.Add(GiayPXTL);
                            ListGPXTL.Add(GiayPXTL);
                            db.SaveChanges();
                            p.Close();
                        }
                        catch
                        {
                            MessageBox.Show("Thêm giấy phép xe tập lái thất bại", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            });

            //edit gpxtl 
            editGPXTL = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {

                bool check = validation();

                if (check)
                {
                    using (db = new giaothongEntities())
                    {
                        try
                        {
                            var gpxtl = db.XETAPLAI_GP.Find(GiayPXTL.SoGPXTL);

                            var checkAnhGP = GiayPXTL.AnhGP.Contains("giaothong");
                            var checkAnhDKiem = GiayPXTL.AnhDKiem.Contains("giaothong");
                            var checkAnhDky = GiayPXTL.AnhDky.Contains("giaothong");


                            if (checkAnhGP)
                            {
                                var index = GiayPXTL.AnhGP.IndexOf("Images");
                                GiayPXTL.AnhGP = GiayPXTL.AnhGP.Remove(0, index - 1);
                            }
                            if (checkAnhDKiem)
                            {
                                var index = GiayPXTL.AnhDKiem.IndexOf("Images");
                                GiayPXTL.AnhDKiem = GiayPXTL.AnhDKiem.Remove(0, index - 1);
                            }
                            if (checkAnhDky)
                            {
                                var index = GiayPXTL.AnhDky.IndexOf("Images");
                                GiayPXTL.AnhDky = GiayPXTL.AnhDky.Remove(0, index - 1);
                            }


                            gpxtl.MaDonViCap = GiayPXTL.MaDonViCap;
                            gpxtl.MaCSDT = GiayPXTL.MaCSDT;
                            gpxtl.XeHopDong = GiayPXTL.XeHopDong;
                            gpxtl.NgayCap = GiayPXTL.NgayCap;
                            gpxtl.NgayHH = GiayPXTL.NgayHH;
                            gpxtl.AnhGP = GiayPXTL.AnhGP;
                            gpxtl.AnhDKiem = GiayPXTL.AnhDKiem;
                            gpxtl.AnhDky = GiayPXTL.AnhDky;
                            gpxtl.DuongTapLai = GiayPXTL.DuongTapLai;
                            gpxtl.NgayCapNhat = DateTime.Now;

                            db.SaveChanges();
                            listGPXTL();
                            p.Close();

                        }
                        catch
                        {
                            MessageBox.Show("Chỉnh sửa giấy phép xe tập lái thất bại", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            });

            //remove gpxtl
            removeGPXTL = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                using (db = new giaothongEntities())
                {
                    try
                    {
                        var message = MessageBox.Show("Bạn có thật sự muốn xóa giấy phép này khỏi danh sách ?", "Thông Báo", MessageBoxButton.OKCancel, MessageBoxImage.Question);

                        if (MessageBoxResult.OK == message)
                        {
                            var gpxtl = db.XETAPLAI_GP.Find(GiayPXTL.SoGPXTL.Trim());
                            if (gpxtl != null)
                            {
                                db.XETAPLAI_GP.Remove(gpxtl);

                                var index = ListGPXTL.ToList().FindIndex(t => t.SoGPXTL.Trim() == GiayPXTL.SoGPXTL.Trim());
                                ListGPXTL.RemoveAt(index);

                                //find vehicle has gpxtl update = null
                                var vehicle = (from c in db.XETAPLAIs where c.SoGPTL.Trim() == gpxtl.SoGPXTL.Trim() select c).FirstOrDefault();
                                if(vehicle != null)
                                {
                                    vehicle.SoGPTL = null;
                                    vehicle.NgayCapNhat = DateTime.Now;
                                }
                                db.SaveChanges();

                                MessageBox.Show("Giấy phép xe tập lái đã được xóa khỏi danh sách", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Information);
                                p.Close();
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Không thể xóa giấy phép xe tập lái lúc này", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            });
            //selected image
            selectedAnhGP = new RelayCommand<string>((p) => { return true; }, (p) =>
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
                    var newCurrentUrl = currentDirectory.Substring(0, index) + @"giaothong\images\vehicle_anhGP";

                    string filePath = Path.Combine(newCurrentUrl, nameFile);

                    if (!File.Exists(filePath))
                    {
                        newCurrentUrl = newCurrentUrl + "\\" + nameFile;
                        File.Copy(url, newCurrentUrl);
                    }

                    var indexCurrentUrl = newCurrentUrl.IndexOf("images");

                    SelectedAnhGP = newCurrentUrl;

                    GiayPXTL.AnhGP = "/Images/vehicle_anhGP/" + nameFile;
                }
                catch { }
            });
            //selected image
            selectedAnhDKiem = new RelayCommand<string>((p) => { return true; }, (p) =>
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
                    var newCurrentUrl = currentDirectory.Substring(0, index) + @"giaothong\images\vehicle_anhDKiem";

                    string filePath = Path.Combine(newCurrentUrl, nameFile);

                    if (!File.Exists(filePath))
                    {
                        newCurrentUrl = newCurrentUrl + "\\" + nameFile;
                        File.Copy(url, newCurrentUrl);
                    }

                    var indexCurrentUrl = newCurrentUrl.IndexOf("images");

                    SelectedAnhDKiem = newCurrentUrl;

                    GiayPXTL.AnhDKiem = "/Images/vehicle_anhDKiem/" + nameFile;
                }
                catch { }
            });
            //selected image
            selectedAnhDKy = new RelayCommand<string>((p) => { return true; }, (p) =>
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
                    var newCurrentUrl = currentDirectory.Substring(0, index) + @"giaothong\images\vehicle_anhDKy";

                    string filePath = Path.Combine(newCurrentUrl, nameFile);

                    if (!File.Exists(filePath))
                    {
                        newCurrentUrl = newCurrentUrl + "\\" + nameFile;
                        File.Copy(url, newCurrentUrl);
                    }

                    var indexCurrentUrl = newCurrentUrl.IndexOf("images");

                    SelectedAnhDKy = newCurrentUrl;

                    GiayPXTL.AnhDky = "/Images/vehicle_anhDKy/" + nameFile;
                }
                catch { }
            });
        }

        //validate information teacher
        public bool validation()
        {
            bool check = true;

            if (!checkLength(GiayPXTL.SoDangKy, 10))
            {
                check = false;
                MessageBox.Show("Số đăng ký không hợp hợp lệ hoặc phải dưới 10 ký tự!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (!checkLength(GiayPXTL.SoGPXTL, 15))
            {
                check = false;
                MessageBox.Show("Số giấy phép xe tập lái không hợp lệ hoặc phải dưới 15 ký tự!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (!checkLength(GiayPXTL.MaDonViCap, 15))
            {
                check = false;
                MessageBox.Show("Mã đơn vị cấp không hợp lệ hoặc phải dưới 5 kí tự!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (!checkLength(GiayPXTL.MaCSDT, 5))
            {
                check = false;
                MessageBox.Show("Mã cơ sở đào tạo không hợp lệ hoặc phải dưới 5 ký tự!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (GiayPXTL.NgayCap.Value == null)
            {
                check = false;
                MessageBox.Show("Ngày cấp không được để trống!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (GiayPXTL.NgayHH.Value == null)
            {
                check = false;
                MessageBox.Show("Ngày hết hạn không được để trống!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (SelectedAnhGP == null)
            {
                check = false;
                MessageBox.Show("Vui lòng thêm ảnh giấy phép!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (SelectedAnhDKiem == null)
            {
                check = false;
                MessageBox.Show("Vui lòng thêm ảnh giấy định kiểm !!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (SelectedAnhDKy == null)
            {
                check = false;
                MessageBox.Show("Vui lòng thêm ảnh giấy đăng ký !!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return check;
        }


        //reset property
        public void reset()
        {
            GiayPXTL = new XETAPLAI_GP();
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

        public XETAPLAI_GP getMaxSoGPXTL()
        {
            var yearNow = DateTime.Now.Year;

            if (ListGPXTL.Count > 0)
            {
                var maxGPXTL = ListGPXTL.Max(p => p.SoGPXTL).Trim();
                var arrGPXTL = maxGPXTL.Split('/');

                if (arrGPXTL.Length > 0)
                {
                    GiayPXTL.SoGPXTL = (Int32.Parse(arrGPXTL[0]) + 1) + "/" + yearNow + "/" + "CM";
                }
            }
            else
            {
                GiayPXTL.SoGPXTL = 1 + "/" + yearNow + "/" + "CM";
            }

            return GiayPXTL;
        }

        //get list vehicle GPXTL
        public ObservableCollection<XETAPLAI_GP> listGPXTL(string searchKey = null)
        {
            using (db = new giaothongEntities())
            {
                ListGPXTL.Clear();

                try
                {
                    var gpxtl = from gp in db.XETAPLAI_GP select gp;

                    if (!string.IsNullOrEmpty(searchKey))
                    {
                        gpxtl = from gp in db.XETAPLAI_GP where gp.SoGPXTL.Equals(searchKey) select gp;
                    }

                    addValueToListGPXTL(gpxtl.ToList());

                }
                catch { }

                return ListGPXTL;
            }
        }

        public void addValueToListGPXTL(List<XETAPLAI_GP> gpxtl)
        {
            TotalPages = (int)Math.Ceiling((double)gpxtl.Count / pageSize);

            var list = gpxtl.Skip((CurrentPage - 1) * pageSize).Take(pageSize);
            string currentDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            var index = currentDirectory.IndexOf("giaothong");
            var newCurrentUrl = currentDirectory.Substring(0, index) + @"giaothong";

            list.ToList().ForEach(p =>
            {
                if (p != null)
                {
                    p.AnhGP = newCurrentUrl + p.AnhGP;
                    p.AnhDKiem = newCurrentUrl + p.AnhDKiem;
                    p.AnhDky = newCurrentUrl + p.AnhDky;
                    ListGPXTL.Add(p);
                }
            });
        }

    }
}
