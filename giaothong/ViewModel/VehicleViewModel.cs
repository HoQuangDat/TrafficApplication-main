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
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using System.Windows.Documents;
using System.Data;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using folder = System.Windows.Forms;



namespace giaothong.ViewModel
{
    public class VehicleViewModel : BaseViewModel
    {
        private giaothongEntities db;
        private ObservableCollection<XETAPLAI> _listVehicle;
        private ObservableCollection<XETAPLAI_GP> _listGPTL;

        private USER _user;
        public USER User { get => _user; set { _user = value; OnPropertyChanged(); } }
        private static bool _isEnable;
        public static bool IsEnable { get => _isEnable; set { _isEnable = value; } }

        private ObservableCollection<XETAPLAI_GP> _listAllGPXTL;
        public ObservableCollection<XETAPLAI_GP> ListAllGPXTL { get => _listAllGPXTL; set => _listAllGPXTL = value; }
        public ObservableCollection<XETAPLAI> ListVehicle { get => _listVehicle; set => _listVehicle = value; }
        public ObservableCollection<XETAPLAI_GP> ListGPTL { get => _listGPTL; set => _listGPTL = value; }

        private XETAPLAI _xeTapLai;
        public XETAPLAI XeTapLai { get => _xeTapLai; set { _xeTapLai = value; OnPropertyChanged(); } }

        public int pageSize = 10; // Số phần tử trên mỗi trang
        private int _currentPage; // Trang hiện tại
        public int CurrentPage { get => _currentPage; set { _currentPage = value; OnPropertyChanged(); } }
        private int _totalPages;
        public int TotalPages { get => _totalPages; set { _totalPages = value; OnPropertyChanged(); } }

        private string _searchVehicle;
        public string SearchVehicle { get => _searchVehicle; set => _searchVehicle = value; }
        private bool _currentWindow;
        public bool CurrentWindow { get => _currentWindow; set { _currentWindow = value; OnPropertyChanged(); } }

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
                    XeTapLai.XeSatHach = true;
                }
                else
                {
                    XeTapLai.XeSatHach = false;
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
                    XeTapLai.TrangThaiHD = true;
                }
                else
                {
                    XeTapLai.TrangThaiHD = false;
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
                        XeTapLai.SoGPTL = ListGPTL[index].SoGPXTL.Trim();
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
                

                if (SelectedItem != null)
                {
                    XeTapLai.SoDangKy = SelectedItem.SoDangKy.Trim();
                    XeTapLai.TenChuSoHuu = SelectedItem.TenChuSoHuu.Trim();
                    XeTapLai.SoGPTL = SelectedItem.SoGPTL.Trim();
                    XeTapLai.NhanHieu = SelectedItem.NhanHieu.Trim();
                    XeTapLai.KieuLoai = SelectedItem.KieuLoai.Trim();
                    XeTapLai.HangXe = SelectedItem.HangXe.Trim();
                    XeTapLai.MauXe = SelectedItem.MauXe.Trim();
                    XeTapLai.SoDongCo = SelectedItem.SoDongCo.Trim();
                    XeTapLai.SoKhung = SelectedItem.SoKhung.Trim();
                    XeTapLai.NamSX = SelectedItem.NamSX.Trim();
                    XeTapLai.LoaiXe = SelectedItem.LoaiXe.Trim();
                    XeTapLai.XeSatHach = SelectedItem.XeSatHach.Value;
                    XeTapLai.TrangThaiHD = SelectedItem.TrangThaiHD.Value;
                    XeTapLai.NgayCapNhat = SelectedItem.NgayCapNhat;
                    listGPTL();
                }
            }
        }
        private int _selectedIndex;
        public int SelectedIndex { get => _selectedIndex; set => _selectedIndex = value; }

        public ICommand viewInsertVehicle{ get; set; }
        public ICommand textChanged { get; set; }
        public ICommand selectionChanged { get; set; }
        public ICommand closeVehicleWindow { get; set; }
        public ICommand closeInsertVehicleWindow { get; set; }
        public ICommand insertVehicle { get; set; }
        public ICommand removeVehicle { get; set; }
        public ICommand editVehicle { get; set; }
        public ICommand previewMouseLeftButtonUp { get; set; }
        public ICommand AgreeVehicleWindow { get; set; }
        public ICommand viewGPXTL { get; set; }
        public ICommand ExportExcel { get; set; }
        public ICommand nextPage { get; set; }
        public ICommand previousPage { get; set; }


        public VehicleViewModel()
        {
            ListVehicle = new ObservableCollection<XETAPLAI>();
            ListGPTL = new ObservableCollection<XETAPLAI_GP>();
            ListAllGPXTL = new ObservableCollection<XETAPLAI_GP>();
            XeTapLai = new XETAPLAI();
            CurrentWindow = true;

            vehicles();
            listGPTL();
            listAllGPXTL();

            XeTapLai.NgayCapNhat = DateTime.Now;
            //export file excel
            ExportExcel = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                //string filePath = "";
                // tạo SaveFileDialog để lưu file excel
                //SaveFileDialog dialog = new SaveFileDialog();
                folder.FolderBrowserDialog folderBrowserDialog = new folder.FolderBrowserDialog();

                // chỉ lọc ra các file có định dạng Excel
                //dialog.Filter = "Excel | *.xlsx | Excel 2003 | *.xls";

                // Nếu mở file và chọn nơi lưu file thành công sẽ lưu tên đường dẫn
                //if (dialog.ShowDialog() == true)
                //{
                //    filePath = dialog.FileName;
                //}

                string folderPath = "";

                if (folderBrowserDialog.ShowDialog() == folder.DialogResult.OK)
                {
                    folderPath = folderBrowserDialog.SelectedPath;
                }

                // nếu đường dẫn null hoặc rỗng thì báo không hợp lệ và return hàm
                //if (string.IsNullOrEmpty(filePath))
                //{
                //    MessageBox.Show("Đường dẫn không hợp lệ");
                //    return;
                //}

                //Không phải doanh nghiệp tránh bản quyền
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (ExcelPackage u = new ExcelPackage())
                {
                    try
                    {
                        // đặt tên người tạo file
                        //u.Workbook.Properties.Author = "Nguyễn Quốc Anh";

                        // đặt tiêu đề cho file
                        u.Workbook.Properties.Title = "DANH SÁCH XE TẬP LÁI";

                        //Tạo một sheet để làm việc trên đó
                        u.Workbook.Worksheets.Add("CTDV");

                        // lấy sheet vừa add ra để thao tác
                        ExcelWorksheet ws = u.Workbook.Worksheets[0];

                        // đặt tên cho sheet
                        ws.Name = "Xe";
                        // fontsize mặc định cho cả sheet
                        ws.Cells.Style.Font.Size = 11;
                        // font family mặc định cho cả sheet
                        ws.Cells.Style.Font.Name = "Calibri";

                        // Tạo danh sách các column header
                        string[] arrColumnHeader = { "STT", "Biển số", "Xe hợp đồng", "Xe chủ sở hữu", "Nhãn hiệu", "Kiểu loại", "Năm SX", "Loại xe tập lái", "Hạng xe dạy lái", "Mục đích sử dụng tập lái ( trong hình, trên đường, số tự động, xe sát hạch)", "Số imei thiết bị DAT", "Ghi chú", "Ghi chú" };

                        // lấy ra số lượng cột cần dùng dựa vào số lượng header
                        var countColHeader = arrColumnHeader.Count();

                        // merge gộp các column lại từ column 1 đến số column header
                        // đặt tên cho bảng excel
                        ws.Cells[1, 2].Value = "TRƯỜNG CAO ĐẲNG CĐ-XD&NL";
                        ws.Cells[1, 2, 1, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                        ws.Cells[1, 11].Value = "CỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM";
                        ws.Cells[1, 11, 1, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        ws.Cells[1, 11, 1, 14].Style.Font.Bold = true;

                        ws.Cells[2, 11].Value = "Độc lập - Tự do - Hạnh phúc";
                        ws.Cells[2, 11, 2, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        ws.Cells[2, 11, 2, 14].Style.Font.Bold = true;
                        ws.Cells[2, 11, 2, 14].Style.Font.UnderLine = true;

                        ws.Cells[2, 2].Value = "TRUNG BỘ";
                        ws.Cells[2, 2, 2, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                        ws.Cells[3, 2].Value = "TRUNG TÂM ĐT&SH LÁI XE";
                        ws.Cells[3, 2, 3, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        ws.Cells[3, 2, 3, 4].Style.Font.Bold = true;
                        ws.Cells[3, 2, 3, 4].Style.Font.UnderLine = true;


                        ws.Cells[4, 1].Value = "DANH SÁCH XE TẬP LÁI";
                        ws.Cells[4, 1, 4, countColHeader].Merge = true;
                        // in đậm
                        ws.Cells[4, 1, 4, countColHeader].Style.Font.Bold = true;
                        // căn giữa
                        ws.Cells[4, 1, 4, countColHeader].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                        int colIndex = 1;
                        int rowIndex = 5;

                        //tạo các cột excel từ mảng cột đã tạo từ bên trên
                        foreach (var item in arrColumnHeader)
                        {
                            var cell = ws.Cells[rowIndex, colIndex];

                            //set màu thành gray
                            var fill = cell.Style.Fill;
                            fill.PatternType = ExcelFillStyle.Solid;
                            fill.BackgroundColor.SetColor(System.Drawing.Color.Transparent);

                            if (colIndex == 2)
                            {
                                ws.Column(colIndex).Width = 25;
                            }
                            else if (colIndex >= 3 && colIndex <= 11 && colIndex != 7 && colIndex != 8 && colIndex != 9)
                            {
                                ws.Column(colIndex).Width = 15;
                            }
                            else if (colIndex == 7 || colIndex == 8 || colIndex == 9)
                            {
                                ws.Column(colIndex).Width = 13;
                            }
                            else if (colIndex == 12)
                            {
                                ws.Column(colIndex).Width = 25;
                            }

                            //căn chỉnh các border
                            var border = cell.Style.Border;
                            border.Bottom.Style = ExcelBorderStyle.Thin;
                            border.Top.Style = ExcelBorderStyle.Thin;
                            border.Left.Style = ExcelBorderStyle.Thin;
                            border.Right.Style = ExcelBorderStyle.Thin;

                            //gán giá trị
                            cell.Value = item;

                            colIndex++;
                        }

                        // lấy ra danh sách CTDV từ ItemSource của ListView

                        if (ListVehicle.Count > 0)
                        {
                            double? sum = 0.0d;


                            // với mỗi item trong danh sách sẽ ghi trên 1 dòng
                            List<XETAPLAI> vehicles = ListVehicle.Cast<XETAPLAI>().ToList();
                            var stt = 1;

                            foreach (var item in vehicles)
                            {
                                // bắt đầu ghi từ cột 1. Excel bắt đầu từ 1 không phải từ 0
                                colIndex = 1;

                                // rowIndex tương ứng từng dòng dữ liệu
                                rowIndex++;

                                //gán giá trị cho từng cell

                                // Stt
                                ws.Cells[rowIndex, colIndex++].Value = stt;
                                ws.Cells[rowIndex, colIndex - 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                ws.Cells[rowIndex, colIndex - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Hair;
                                if (vehicles.Count - stt == 0)
                                {
                                    ws.Cells[rowIndex, colIndex - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                }

                                // So Dang Ky
                                ws.Cells[rowIndex, colIndex++].Value = item.SoDangKy;
                                ws.Cells[rowIndex, colIndex - 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                ws.Cells[rowIndex, colIndex - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Hair;
                                if (vehicles.Count - stt == 0)
                                {
                                    ws.Cells[rowIndex, colIndex - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                }

                                foreach (var gpxtl in ListAllGPXTL)
                                {
                                    // Xe Hop Dong
                                    if(gpxtl.XeHopDong != null)
                                    {
                                        if (gpxtl.XeHopDong == true)
                                        {
                                            ws.Cells[rowIndex, colIndex++].Value = "X";
                                            ws.Cells[rowIndex, colIndex - 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                            ws.Cells[rowIndex, colIndex - 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            ws.Cells[rowIndex, colIndex - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Hair;
                                           
                                            colIndex++;
                                        }

                                        // Xe chu so huu
                                        else
                                        {
                                            colIndex++;
                                            ws.Cells[rowIndex, colIndex++].Value = "X";
                                            ws.Cells[rowIndex, colIndex - 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                            ws.Cells[rowIndex, colIndex - 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            ws.Cells[rowIndex, colIndex - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Hair;
                                        }
                                    }
                                    if (vehicles.Count - stt == 0)
                                    {
                                        ws.Cells[rowIndex, colIndex - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                    }
                                    break;
                                }

                                // Nhan hieu
                                ws.Cells[rowIndex, colIndex++].Value = item.NhanHieu;
                                ws.Cells[rowIndex, colIndex - 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                ws.Cells[rowIndex, colIndex - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Hair;
                                if (vehicles.Count - stt == 0)
                                {
                                    ws.Cells[rowIndex, colIndex - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                }

                                // Kieu Loai
                                ws.Cells[rowIndex, colIndex++].Value = item.KieuLoai;
                                ws.Cells[rowIndex, colIndex - 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                ws.Cells[rowIndex, colIndex - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Hair;
                                if (vehicles.Count - stt == 0)
                                {
                                    ws.Cells[rowIndex, colIndex - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                }


                                // Nam SX
                                ws.Cells[rowIndex, colIndex++].Value = item.NamSX;
                                ws.Cells[rowIndex, colIndex - 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                ws.Cells[rowIndex, colIndex - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Hair;
                                if (vehicles.Count - stt == 0)
                                {
                                    ws.Cells[rowIndex, colIndex - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                }

                                // Loai xe
                                ws.Cells[rowIndex, colIndex++].Value = item.LoaiXe;
                                ws.Cells[rowIndex, colIndex - 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                ws.Cells[rowIndex, colIndex - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Hair;
                                if (vehicles.Count - stt == 0)
                                {
                                    ws.Cells[rowIndex, colIndex - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                }

                                // Hang Loai
                                ws.Cells[rowIndex, colIndex++].Value = item.HangXe;
                                ws.Cells[rowIndex, colIndex - 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                ws.Cells[rowIndex, colIndex - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Hair;
                                if (vehicles.Count - stt == 0)
                                {
                                    ws.Cells[rowIndex, colIndex - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                }

                                // Muc dich su dung
                                ws.Cells[rowIndex, colIndex++].Value = "Tạm thời không có trong db";
                                ws.Cells[rowIndex, colIndex - 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                ws.Cells[rowIndex, colIndex - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Hair;
                                if (vehicles.Count - stt == 0)
                                {
                                    ws.Cells[rowIndex, colIndex - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                }

                                //So imei
                                ws.Cells[rowIndex, colIndex++].Value = "Tạm thời không có trong db";
                                ws.Cells[rowIndex, colIndex - 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                ws.Cells[rowIndex, colIndex - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Hair;
                                if (vehicles.Count - stt == 0)
                                {
                                    ws.Cells[rowIndex, colIndex - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                }

                                //Ghi chu
                                ws.Cells[rowIndex, colIndex++].Value = "";
                                ws.Cells[rowIndex, colIndex - 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                ws.Cells[rowIndex, colIndex - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Hair;
                                if (vehicles.Count - stt == 0)
                                {
                                    ws.Cells[rowIndex, colIndex - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                }

                                //Ghi chu ten chu so huu
                                ws.Cells[rowIndex, colIndex++].Value = item.TenChuSoHuu;
                                ws.Cells[rowIndex, colIndex - 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                ws.Cells[rowIndex, colIndex - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Hair;
                                if (vehicles.Count - stt == 0)
                                {
                                    ws.Cells[rowIndex, colIndex - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                }
                                ws.Cells[rowIndex, colIndex - 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                               
                                //Tự căn độ dài cột
                                ws.Cells.AutoFitColumns();
                                // In đậm chữ cho hàng tiêu đề
                                ws.Cells[5, 1, 5, 14].Style.Font.Bold = true;
                                stt++;

                            }

                            ws.Cells[rowIndex + 1, 11].Value = DateTime.Now;
                            ws.Cells[rowIndex + 1, 11].Style.Numberformat.Format = "\"Ngày\" dd\" tháng \"MM\" năm \"yyyy";
                            ws.Cells[rowIndex + 1, 11, rowIndex + 1, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                            ws.Cells[rowIndex + 2, 11].Value = "Trung tâm ĐT & SHLX";
                            ws.Cells[rowIndex + 2, 11, rowIndex + 2, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            ws.Cells[rowIndex + 2, 11, rowIndex + 2, 14].Style.Font.Bold = true;

                            ws.Cells[rowIndex + 2, 2].Value = "Hiệu trưởng";
                            ws.Cells[rowIndex + 2, 2, rowIndex + 2, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            ws.Cells[rowIndex + 2, 2, rowIndex + 2, 4].Style.Font.Bold = true;


                            //Lưu file lại
                            // Xây dựng đường dẫn tới file mới
                            string fileName = "dsxetaplai.xlsx";
                            string filePath = Path.Combine(folderPath, fileName);

                            Byte[] bin = u.GetAsByteArray();

                            File.WriteAllBytes(filePath, bin);
                        }
                        MessageBox.Show("Xuất File Excel Thành Công!", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Lỗi Xuất File!", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }

            });

            //next page
            nextPage = new RelayCommand<string>((p) => { return true; }, (p) =>
            {
                if (CurrentPage < TotalPages)
                {
                    CurrentPage++;
                    listVehicle(SearchVehicle);
                }
            });

            //previous page
            previousPage = new RelayCommand<string>((p) => { return true; }, (p) =>
            {
                if (CurrentPage > 1)
                {
                    CurrentPage--;
                    listVehicle(SearchVehicle);
                }
            });

            // Đóng cửa sổ danh sách xe tập lái
            closeVehicleWindow = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });


            // Đóng cửa sổ thêm xe
            closeInsertVehicleWindow = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
                SelectedItem = null;
            });

            // Load view insert vehicle
            viewInsertVehicle = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                reset();
                if (ListGPTL.Count > 0)
                {
                    XeTapLai.SoGPTL = ListGPTL[0].SoGPXTL.Trim();
                }

                p.Hide();
                InsertVehicle inserVehicle = new InsertVehicle();
                inserVehicle.ShowDialog();
                p.ShowDialog();
            });

            //view gpxtl
            viewGPXTL = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                CurrentWindow = false;
                listGPTL();
                p.Hide();
                VehicleGPWindow vehicleGP = new VehicleGPWindow();
                vehicleGP.ShowDialog();
                vehicles();
                listGPTL();
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
                    CurrentWindow = true;
                    listGPTL();
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
                            var xe = db.XETAPLAIs.Find(XeTapLai.SoDangKy.Trim());
                            if (xe != null)
                            {
                                db.XETAPLAIs.Remove(xe);
                                db.SaveChanges();

                                var index = ListVehicle.ToList().FindIndex(x => x.SoDangKy == XeTapLai.SoDangKy);
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

                    var checkGPTL = checkExistsGPTL(XeTapLai.SoGPTL);
                    using (db = new giaothongEntities())
                    {
                        if (check)
                        {
                            
                            db.XETAPLAIs.Add(XeTapLai);
                            ListVehicle.Add(XeTapLai);
                            db.SaveChanges();
                            p.Close();
                        }
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
                    try
                    {
                        var check = validation();
                        //MessageBox.Show("Show check" + check);
                        if (check)
                        {
                        using (db = new giaothongEntities())
                        {
                            var xe = db.XETAPLAIs.Find(XeTapLai.SoDangKy.Trim());
                            var user = (from c in db.USERS select c).FirstOrDefault();

                            if (xe != null)
                            {
                                xe.TenChuSoHuu = XeTapLai.TenChuSoHuu.Trim();
                                xe.SoGPTL = XeTapLai.SoGPTL.Trim();
                                xe.NhanHieu = XeTapLai.NhanHieu.Trim();
                                xe.KieuLoai = XeTapLai.KieuLoai.Trim();
                                xe.HangXe = XeTapLai.HangXe.Trim();
                                xe.MauXe = XeTapLai.MauXe.Trim();
                                xe.SoDongCo = XeTapLai.SoDongCo.Trim();
                                xe.SoKhung = XeTapLai.SoKhung.Trim();
                                xe.NamSX = XeTapLai.NamSX.Trim();
                                xe.LoaiXe = XeTapLai.LoaiXe.Trim();
                                xe.XeSatHach = XeTapLai.XeSatHach;
                                xe.TrangThaiHD = XeTapLai.TrangThaiHD;
                                xe.NgayCapNhat = DateTime.Now;
                                xe.NguoiCapNhat = user.Name;
                                db.SaveChanges();
                                vehicles();
                                CurrentWindow = true;
                                p.Close();
                                }
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Cập nhật thông tin xe thất bại", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
            });

            AgreeVehicleWindow = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                using (db = new giaothongEntities())
                {
                    try
                    {
                        var message = MessageBox.Show("Bạn có thật sự muốn xóa XE này khỏi danh sách ?", "Thông Báo", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                        var xe = db.XETAPLAIs.Find(XeTapLai.SoDangKy.Trim());

                        if (xe != null)
                        {
                            xe.TrangThaiHD = true;
                            xe.NgayCapNhat = DateTime.Now;
                            xe.NguoiCapNhat = User.Name;

                            db.SaveChanges();
                            vehicles();

                            p.Close();
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

            var checkSoDK = checkExists(XeTapLai.SoDangKy);


            if (!checkSoDK && !checkLength(XeTapLai.SoDangKy, 10))
            {
                check = false;
                MessageBox.Show("Số đăng ký không hợp lệ hoặc phải dưới 10 ký tự!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (!checkLength(XeTapLai.TenChuSoHuu, 30))
            {
                check = false;
                MessageBox.Show("Tên chủ sở hữu không hợp lệ hoặc phải dưới 30 ký tự!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (!checkLength(XeTapLai.NhanHieu, 15))
            {
                check = false;
                MessageBox.Show("Nhãn hiệu không hợp lệ hoặc phải dưới 15 ký tự!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (!checkLength(XeTapLai.SoDongCo, 20))
            {
                check = false;
                MessageBox.Show("Số động cơ không hợp lệ hoặc phải dưới 20 ký tự!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (!checkLength(XeTapLai.SoKhung, 20))
            {
                check = false;
                MessageBox.Show("Số khung không hợp lệ hoặc phải dưới 20 ký tự!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (!checkLength(XeTapLai.NamSX, 5))
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
                    value = "B1";
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
                    else if (keyword.ToLower() == "A1")
                    {
                        var vehicles = from xe in db.XETAPLAIs where xe.HangXe == "A1" select xe;

                        if (!string.IsNullOrEmpty(searchKey))
                        {
                            vehicles = from xe in db.XETAPLAIs where xe.SoDangKy == keyword || xe.TenChuSoHuu == keyword select xe;
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
            TotalPages = (int)Math.Ceiling((double)vehicles.Count / pageSize);
            var list = vehicles.Skip((CurrentPage - 1) * pageSize).Take(pageSize);
            list.ToList().ForEach(p =>
            {
                if (p != null)
                {
                    ListVehicle.Add(p);
                }
            });
        }

        // Reset form
        public void reset()
        {
            XeTapLai.SoDangKy = null;
            XeTapLai.TenChuSoHuu = null;
            XeTapLai.SoGPTL = null;
            XeTapLai.NhanHieu = null;
            XeTapLai.KieuLoai = null;
            XeTapLai.HangXe = null;
            XeTapLai.MauXe = null;
            XeTapLai.SoDongCo = null;
            XeTapLai.SoKhung = null;
            XeTapLai.NamSX = null;
            XeTapLai.LoaiXe = null;
            XeTapLai.XeSatHach = false;
            XeTapLai.TrangThaiHD = false;
            XeTapLai.NgayCapNhat = DateTime.MinValue;
            XeTapLai.NguoiCapNhat = null;
        }

        //get list vehicle GPXTL
        public ObservableCollection<XETAPLAI> listVehicle(string searchKey = null)
        {
            using (db = new giaothongEntities())
            {
                ListVehicle.Clear();

                try
                {
                    var xe = from x in db.XETAPLAIs select x;

                    if (!string.IsNullOrEmpty(searchKey))
                    {
                        xe = from x in db.XETAPLAIs where x.SoDangKy.Equals(searchKey) select x;
                    }

                    addValueToListVehicle(xe.ToList());

                }
                catch { }

                return ListVehicle;
            }
        }

        //get list giay phep tap lai
        public ObservableCollection<XETAPLAI_GP> listGPTL()
        {
            using (db = new giaothongEntities())
            {
                ListGPTL.Clear();
                try
                {
                    var gptl = from gp in db.XETAPLAI_GP
                               where !db.XETAPLAIs.Any(xe => xe.SoGPTL == gp.SoGPXTL) || (XeTapLai.SoGPTL == gp.SoGPXTL && CurrentWindow == true)
                               select gp;

                    gptl.ToList().ForEach(p =>
                    {
                        ListGPTL.Add(p);
                    });
                }
                catch
                {

                }
                

                return ListGPTL;
            }
        }

        //get list vehicle GPXTL
        public ObservableCollection<XETAPLAI_GP> listAllGPXTL()
        {
            using (db = new giaothongEntities())
            {
                ListAllGPXTL.Clear();

                var gpxtl = from c in db.XETAPLAI_GP
                          select c;

                gpxtl.ToList().ForEach(p =>
                {
                    ListAllGPXTL.Add(p);
                });

                return ListAllGPXTL;
            }
        }
    }
}
