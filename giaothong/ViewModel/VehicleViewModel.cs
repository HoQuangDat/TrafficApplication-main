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
    public class VehicleViewModel : BaseViewModel
    {
        private giaothongEntities db;
        private ObservableCollection<XETAPLAI> _listVehicle;
        private ObservableCollection<HangXe> _listHangXe;
        private ObservableCollection<XETAPLAI_GP> _listGPTL;
        private ObservableCollection<LoaiXe> _listLoaiXe;


        public ObservableCollection<XETAPLAI> ListVehicle { get => _listVehicle; set => _listVehicle = value; }
        public ObservableCollection<HangXe> ListHangXe { get => _listHangXe; set => _listHangXe = value; }

        public ObservableCollection<XETAPLAI_GP> ListGPTL { get => _listGPTL; set => _listGPTL = value; }
        public ObservableCollection<LoaiXe> ListLoaiXe { get => _listLoaiXe; set => _listLoaiXe = value; }

        private string _soDangKy;
        public string SoDangKy { get => _soDangKy; set { _soDangKy = value; OnPropertyChanged(); } }

        private string _tenChuSoHuu;
        public string TenChuSoHuu { get => _tenChuSoHuu; set { _tenChuSoHuu = value; OnPropertyChanged(); } }

        private string _soGPTL;
        public string SoGPTL { get => _soGPTL; set { _soGPTL = value; OnPropertyChanged(); } }

        private string _nhanHieu;
        public string NhanHieu { get => _nhanHieu; set { _nhanHieu = value; OnPropertyChanged(); } }

        private string _kieuLoai;
        public string KieuLoai { get => _kieuLoai; set { _kieuLoai = value; OnPropertyChanged(); } }

        private string _hangXe;
        public string HangXe { get => _hangXe; set { _hangXe = value; OnPropertyChanged(); } }

        private string _mauXe;
        public string MauXe { get => _mauXe; set { _mauXe = value; OnPropertyChanged(); } }

        private string _soDongCo;
        public string SoDongCo { get => _soDongCo; set { _soDongCo = value; OnPropertyChanged(); } }

        private string _soKhung;
        public string SoKhung { get => _soKhung; set { _soKhung = value; OnPropertyChanged(); } }

        private string _namSX;
        public string NamSX { get => _namSX; set { _namSX = value; OnPropertyChanged(); } }

        private string _loaiXe;
        public string LoaiXe { get => _loaiXe; set { _loaiXe = value; OnPropertyChanged(); } }

        private bool _xeSatHach;
        public bool XeSatHach { get => _xeSatHach; set { _xeSatHach = value; OnPropertyChanged(); } }

        private bool _trangThaiHD;
        public bool TrangThaiHD { get => _trangThaiHD; set { _trangThaiHD = value; OnPropertyChanged(); } }

        private DateTime _ngayCapNhat;
        public DateTime NgayCapNhat { get => _ngayCapNhat; set { _ngayCapNhat = value; OnPropertyChanged(); } }

        private string _nguoiCapNhat;
        public string NguoiCapNhat { get => _nguoiCapNhat; set { _nguoiCapNhat = value; OnPropertyChanged(); } }

        private int _selectedIndex;
        public int SelectedIndex { get => _selectedIndex; set => _selectedIndex = value; }

        private string _searchVehicle;
        public string SearchVehicle { get => _searchVehicle; set => _searchVehicle = value; }

        private bool _isCheckedXeSatHach;
        public bool IsCheckedXeSatHach
        {
            get => _isCheckedXeSatHach;
            set
            {
                _isCheckedXeSatHach = value;
                OnPropertyChanged();

                if (IsCheckedXeSatHach)
                {
                    XeSatHach = true;
                }
                else
                {
                    XeSatHach = false;
                }
            }
        }

        private bool _isCheckedTrangThaiHD;
        public bool IsCheckedTrangThaiHD
        {
            get => _isCheckedTrangThaiHD;
            set
            {
                _isCheckedTrangThaiHD = value;
                OnPropertyChanged();

                if (IsCheckedTrangThaiHD)
                {
                    TrangThaiHD = true;
                }
                else
                {
                    TrangThaiHD = false;
                }
            }
        }

        // select giay phep lai xe
        private int _selectedIndexGPLX;
        public int SelectedIndexGPLX
        {
            get => _selectedIndexGPLX;
            set
            {
                _selectedIndexGPLX = value;
                OnPropertyChanged();

                int index = 0;

                for (index = 0; index < ListGPTL.Count(); index++)
                {
                    if (index == SelectedIndexGPLX)
                    {
                        SoGPTL = ListGPTL[index].SoGPXTL.Trim();
                        break;
                    }
                }
            }
        }

        // Select hang xe
        private int _selectedIndexHX;
        public int SelectedIndexHX
        {
            get => _selectedIndexHX;
            set
            {
                _selectedIndexHX = value;
                OnPropertyChanged();

                int index = 0;

                for (index = 0; index < ListHangXe.Count(); index++)
                {
                    if (index == SelectedIndexHX)
                    {
                        HangXe = ListHangXe[index].ten.Trim();
                        break;
                    }
                }
            }
        }

        // Select loai xe
        private int _selectedIndexLX;
        public int SelectedIndexLX
        {
            get => _selectedIndexLX;
            set
            {
                _selectedIndexLX = value;
                OnPropertyChanged();

                int index = 0;

                for (index = 0; index < ListLoaiXe.Count(); index++)
                {
                    if (index == SelectedIndexLX)
                    {
                        LoaiXe = ListLoaiXe[index].Ten.Trim();
                        break;
                    }
                }
            }
        }

        private XETAPLAI _selectedItem;
        public XETAPLAI SelectedItem
        {
            get => _selectedItem; set
            {
                _selectedItem = value;
                OnPropertyChanged();
                reset();

                if (SelectedItem == null)
                {
                    SoDangKy = SelectedItem.SoDangKy;
                    TenChuSoHuu = SelectedItem.TenChuSoHuu;
                    SoGPTL = SelectedItem.SoGPTL;
                    NhanHieu = SelectedItem.NhanHieu;
                    KieuLoai = SelectedItem.KieuLoai;
                    HangXe = SelectedItem.HangXe;
                    MauXe = SelectedItem.MauXe;
                    SoDongCo = SelectedItem.SoDongCo;
                    SoKhung = SelectedItem.SoKhung;
                    NamSX = SelectedItem.NamSX;
                    LoaiXe = SelectedItem.LoaiXe;
                    XeSatHach = SelectedItem.XeSatHach.Value;
                    TrangThaiHD = SelectedItem.TrangThaiHD.Value;
                    NgayCapNhat = SelectedItem.NgayCapNhat.Value;
                }
            }
        }

        public ICommand viewInsertVehicle{ get; set; }
        public ICommand textChanged { get; set; }
        public ICommand selectionChanged { get; set; }
        public ICommand closeVehicleWindow { get; set; }
        public ICommand closeInsertVehicleWindow { get; set; }
        public ICommand insertVehicle { get; set; }
        public ICommand removeVehicle { get; set; }
        public ICommand editVehicle { get; set; }
        public ICommand previewMouseLeftButtonUp { get; set; }

        public VehicleViewModel()
        {
            ListVehicle = new ObservableCollection<XETAPLAI>();
            ListHangXe = new ObservableCollection<HangXe>();
            ListGPTL = new ObservableCollection<XETAPLAI_GP>();
            ListLoaiXe = new ObservableCollection<LoaiXe>();

            vehicles();
            listGPTL();
            listHangXe();
            listLoaiXe();

            // Đóng cửa sổ danh sách xe tập lái
            closeVehicleWindow = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });


            // Đóng cửa sổ thêm xe
            closeInsertVehicleWindow = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });

            // Load view insert vehicle
            viewInsertVehicle = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {

                if (ListGPTL.Count > 0)
                {
                    SoGPTL = ListGPTL[0].SoGPXTL.Trim();
                }

                if (ListHangXe.Count > 0)
                {
                    HangXe = ListHangXe[0].ten.Trim();
                }

                if (ListLoaiXe.Count > 0)
                {
                    LoaiXe = ListLoaiXe[0].Ten.Trim();
                }

                p.Hide();
                InsertVehicle inserVehicle = new InsertVehicle();
                inserVehicle.ShowDialog();
                p.ShowDialog();
            });

            //selectected change filter status vehicles
            selectionChanged = new RelayCommand<string>((p) => { return true; }, (p) =>
             {
                 var selectedValue = changeSelectedStatus(SelectedIndex);
                 vehicles(selectedValue);
             });

            //find vehicle by SoDangKy or HangXe
            textChanged = new RelayCommand<string>((p) => { return true; }, (p) =>
            {
                var selectedValue = changeSelectedStatus(SelectedIndex);
                vehicles(selectedValue, SearchVehicle);
            });

            //open window edit vehicle
            previewMouseLeftButtonUp = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                //MessageBox.Show("Da vao su kien");
                if (SelectedItem != null)
                {
                    p.Hide();
                    EditVehicleWindow editvehicle = new EditVehicleWindow();
                    editvehicle.ShowDialog();
                    p.ShowDialog();
                }
            });

            //remove teacher get out list
            removeVehicle = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                using (db = new giaothongEntities())
                {
                    try
                    {
                        var message = MessageBox.Show("Bạn có thật sự muốn xóa xe này khỏi danh sách ?", "Thông Báo", MessageBoxButton.OKCancel, MessageBoxImage.Question);

                        if (MessageBoxResult.OK == message)
                        {
                            var xe = db.XETAPLAIs.Find(SoDangKy.Trim());
                            if (xe != null)
                            {
                                db.XETAPLAIs.Remove(xe);
                                db.SaveChanges();

                                var index = ListVehicle.ToList().FindIndex(x => x.SoDangKy == SoDangKy);
                                ListVehicle.RemoveAt(index);

                                MessageBox.Show("Xe đã được xóa khỏi danh sách", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Information);
                                p.Close();
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Không thể xóa xe lúc này", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            });

            //insert vehicle
            insertVehicle = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                try
                {
                    var check = validation();

                    var checkGPTL = checkExistsGPTL(SoGPTL);

                    /*if (!checkGPTL)
                    {
                        check = false;
                        MessageBox.Show("Giấy phép tập lái không tồn tại !!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    if (SoGPTL != null)
                    {
                        check = false;
                        MessageBox.Show("Vui lòng chọn giấy phép lái xe phù hợp!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    if (LoaiXe != null)
                    {
                        check = false;
                        MessageBox.Show("Chọn loại xe không được để trống", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }*/

                    if (check)
                    {
                        XETAPLAI xe = new XETAPLAI()
                        {
                            SoDangKy = SoDangKy.Trim(),
                            TenChuSoHuu = TenChuSoHuu.Trim(),
                            SoGPTL = SoGPTL.Trim(),
                            NhanHieu = NhanHieu.Trim(),
                            KieuLoai = KieuLoai.Trim(),
                            HangXe = HangXe.Trim(),
                            MauXe = MauXe.Trim(),
                            SoDongCo = SoDongCo.Trim(),
                            SoKhung = SoKhung.Trim(),
                            NamSX = NamSX.Trim(),
                            LoaiXe = LoaiXe.Trim(),
                            XeSatHach = XeSatHach,
                            TrangThaiHD = TrangThaiHD,
                            NgayCapNhat = DateTime.Now,
                            NguoiCapNhat = "Quang Dat",
                        };

                        insert(xe);

                        p.Close();
                    }
                }
                catch
                {
                    MessageBox.Show("Thêm xe tập lái thất bại", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });

            // Chinh sua thong tin xe
            editVehicle = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                using (db = new giaothongEntities())
                {
                    try
                    {
                        var check = validation();

                        if (check)
                        {
                            var xe = db.XETAPLAIs.Find(SoDangKy.Trim());

                            if (xe != null)
                            {
                                xe.SoDangKy = SoDangKy.Trim();
                                xe.TenChuSoHuu = TenChuSoHuu.Trim();
                                xe.SoGPTL = SoGPTL.Trim();
                                xe.NhanHieu = NhanHieu.Trim();
                                xe.KieuLoai = KieuLoai.Trim();
                                xe.HangXe = HangXe.Trim();
                                xe.MauXe = MauXe.Trim();
                                xe.SoDongCo = SoDongCo.Trim();
                                xe.SoKhung = SoKhung.Trim();
                                xe.NamSX = NamSX.Trim();
                                xe.LoaiXe = LoaiXe.Trim();
                                xe.XeSatHach = XeSatHach;
                                xe.TrangThaiHD = TrangThaiHD;
                                xe.NgayCapNhat = DateTime.Now;

                                db.SaveChanges();
                                vehicles();

                                p.Close();
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Cập nhật thông tin xe thất bại", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            });

        }

        //validate information teacher
        public bool validation()
        {
            bool check = true;

            var checkSoDK = checkExists(SoDangKy);


            if (!checkSoDK && !checkLength(SoDangKy, 10))
            {
                check = false;
                MessageBox.Show("Số đăng ký không hợp lệ hoặc phải dưới 10 ký tự!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (!checkLength(TenChuSoHuu, 30))
            {
                check = false;
                MessageBox.Show("Tên chủ sở hữu không hợp lệ hoặc phải dưới 30 ký tự!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (!checkLength(NhanHieu, 15))
            {
                check = false;
                MessageBox.Show("Nhãn hiệu không hợp lệ hoặc phải dưới 15 ký tự!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (!checkLength(SoDongCo, 20))
            {
                check = false;
                MessageBox.Show("Số động cơ không hợp lệ hoặc phải dưới 20 ký tự!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (!checkLength(SoKhung, 20))
            {
                check = false;
                MessageBox.Show("Số khung không hợp lệ hoặc phải dưới 20 ký tự!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (!checkLength(NamSX, 5))
            {
                check = false;
                MessageBox.Show("Năm sản xuất không hợp lệ hoặc phải dưới 5 ký tự!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return check;
        }

        //check number Giay phep tap lai xe exists
        public bool checkExistsGPTL(string soGiayPhep)
        {
            using (db = new giaothongEntities())
            {
                var query = (from gptl in db.XETAPLAI_GP where gptl.SoGPXTL == soGiayPhep select gptl).Count();

                if (query == 1)
                {
                    return false;
                }
            }

            return true;
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

        //check number GiayPhepLaiXe exists
        public bool checkExistsSoGPLX(string soGPLX)
        {
            using (db = new giaothongEntities())
            {
                var query = (from gplx in db.XETAPLAIs where gplx.SoGPTL == soGPLX select gplx).Count();

                if (query == 1)
                {
                    return false;
                }
            }

            return true;
        }   

        // Kiem tra xem thong tin xe da ton tai trong db chua
        public bool checkExists(string value)
        {
            using (db = new giaothongEntities())
            {
                var query = (from xe in db.XETAPLAIs where xe.SoDangKy == value select xe).Count();

                if (query > 0)
                {
                    return false;
                }
            }

            return true;
        }


        //get value with index combobox
        public string changeSelectedStatus(int index)
        {
            var value = "";

            switch (index)
            {
                case 1:
                    value = "A1";
                    break;
                case 2:
                    value = "A2";
                    break;
                case 3:
                    value = "B";
                    break;
                case 4:
                    value = "C";
                    break;
                case 5:
                    value = "D";
                    break;
                case 6:
                    value = "FC";
                    break;
                default:
                    value = "Tất cả";
                    break;
            }

            return value;
        }

        //get list vehicles
        public void vehicles(string keyword = "tất cả", string searchKey = null)
        {
            using (db = new giaothongEntities())
            {
                try
                {
                    _listVehicle.Clear();

                    if (keyword.ToLower() == "tất cả")
                    {
                        var vehicles = from xe in db.XETAPLAIs select xe;

                        if (!string.IsNullOrEmpty(searchKey))
                        {
                            vehicles = from xe in db.XETAPLAIs where xe.TenChuSoHuu == searchKey || xe.NhanHieu == searchKey select xe;
                        }

                        addValueToListVehicle(vehicles.ToList());
                    }
                    else
                    {
                        var vehicles = from xe in db.XETAPLAIs where xe.HangXe == keyword select xe;

                        if (!string.IsNullOrEmpty(searchKey))
                        {
                            vehicles = from xe in db.XETAPLAIs where xe.HangXe == keyword && xe.TenChuSoHuu == searchKey || xe.HangXe == keyword && xe.NhanHieu == searchKey || xe.HangXe == keyword && xe.SoDangKy == searchKey select xe;
                        }

                        addValueToListVehicle(vehicles.ToList());
                    }
                }
                catch { };
            }
        }

        //add value to list vehicles
        public void addValueToListVehicle(List<XETAPLAI> vehicles)
        {
            vehicles.ToList().ForEach(p =>
            {
                if (p != null)
                {
                    ListVehicle.Add(p);
                }
            });

        }

        //Them xe tap lai
        public void insert(XETAPLAI vehicles)
        {
            using (db = new giaothongEntities())
            {
                db.XETAPLAIs.Add(vehicles);
                db.SaveChanges();

                ListVehicle.Add(vehicles);
            }
        }

        // Reset form
        public void reset()
        {
            SoDangKy = null;
            TenChuSoHuu = null;
            SoGPTL = null;
            NhanHieu = null;
            KieuLoai = null;
            HangXe = null;
            MauXe = null;
            SoDongCo = null;
            SoKhung = null;
            NamSX = null;
            LoaiXe = null;
            XeSatHach = false;
            TrangThaiHD = false;
            NgayCapNhat = DateTime.MinValue;
            NguoiCapNhat = null;
        }

        //get list giay phep tap lai
        public ObservableCollection<XETAPLAI_GP> listGPTL()
        {
            using (db = new giaothongEntities())
            {
                var gptl = from gp in db.XETAPLAI_GP
                           where !db.XETAPLAIs.Any(xe => xe.SoGPTL == gp.SoGPXTL)
                           select gp;

                gptl.ToList().ForEach(p =>
                {
                    ListGPTL.Add(p);
                });

                return ListGPTL;
            }
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

        //get list LoaiXe
        public ObservableCollection<LoaiXe> listLoaiXe()
        {
            using (db = new giaothongEntities())
            {
                ListLoaiXe.Clear();

                try
                {
                    var loaixe = from lx in db.LoaiXes select lx;

                    loaixe.ToList().ForEach(p =>
                    {
                        ListLoaiXe.Add(p);
                    });
                }
                catch { }

                return ListLoaiXe;
            }
        }


    }
}
