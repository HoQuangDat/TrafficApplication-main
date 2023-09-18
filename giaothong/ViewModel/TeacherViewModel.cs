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
    public class TeacherViewModel : BaseViewModel
    {
        private giaothongEntities db;
        private ObservableCollection<GIAOVIEN> _listTeacher;
        public ObservableCollection<GIAOVIEN> ListTeacher { get => _listTeacher; set => _listTeacher = value; }

        private ObservableCollection<GIAOVIEN_GCN> _listGCN;
        public ObservableCollection<GIAOVIEN_GCN> ListGCN { get => _listGCN; set => _listGCN = value; }

        private ObservableCollection<GIAOVIEN_GCN> _listAllGCN;
        public ObservableCollection<GIAOVIEN_GCN> ListAllGCN { get => _listAllGCN; set => _listAllGCN = value; }

        private List<province_city> _listCity;
        public List<province_city> ListCity { get => _listCity; set => _listCity = value; }

        private GIAOVIEN _giaoVien;
        public GIAOVIEN GiaoVien { get => _giaoVien; set { _giaoVien = value; OnPropertyChanged(); } }

        private USER _user;
        public USER User { get => _user; set { _user = value; OnPropertyChanged(); } }

        private static bool _isEnable;
        public static bool IsEnable { get => _isEnable; set { _isEnable = value; } }

        public int pageSize = 10; // Số phần tử trên mỗi trang
        private int _currentPage; // Trang hiện tại
        public int CurrentPage { get => _currentPage; set { _currentPage = value; OnPropertyChanged(); } }

        private int _totalPages;
        public int TotalPages { get => _totalPages; set { _totalPages = value; OnPropertyChanged(); } }

        private bool _isCheckedGV_LT;
        public bool IsCheckedGV_LT
        {
            get => _isCheckedGV_LT;
            set
            {
                _isCheckedGV_LT = value;
                OnPropertyChanged();
                if (IsCheckedGV_LT)
                    GiaoVien.GV_LT = true;
                else
                    GiaoVien.GV_LT = false;
            }
        }

        private bool _isCheckedGV_TH;
        public bool IsCheckedGV_TH
        {
            get => _isCheckedGV_TH; set
            {
                _isCheckedGV_TH = value;
                OnPropertyChanged();
                if (IsCheckedGV_TH)
                    GiaoVien.GV_TH = true;
                else
                    GiaoVien.GV_TH = false;
            }
        }

        private int _selectedIndex;
        public int SelectedIndex { get => _selectedIndex; set => _selectedIndex = value; }

        private string _searchTeacher;
        public string SearchTeacher { get => _searchTeacher; set => _searchTeacher = value; }

        private string _selectedImage;
        public string SelectedImage { get => _selectedImage; set { _selectedImage = value; OnPropertyChanged(); } }

        private bool _currentWindow;
        public bool CurrentWindow { get => _currentWindow; set { _currentWindow = value; OnPropertyChanged(); } }


        private int _selectedIndexStatus;
        public int SelectedIndexStatus
        {
            get => _selectedIndexStatus;
            set
            {
                _selectedIndexStatus = value;
                OnPropertyChanged();

                if (SelectedIndexStatus == 0)
                {
                    GiaoVien.TrangThai = true;
                }
                else
                {
                    GiaoVien.TrangThai = false;
                };
            }
        }


        private bool _isCheckedGender;
        public bool IsCheckedGender
        {
            get => _isCheckedGender;
            set
            {
                _isCheckedGender = value;
                OnPropertyChanged();

                if (IsCheckedGender)
                {
                    GiaoVien.GioiTinh = 0;
                }
                else
                {
                    GiaoVien.GioiTinh = 1;
                }
            }
        }


        private int _selectedIndexNoiCT;
        public int SelectedIndexNoiCT
        {
            get => _selectedIndexNoiCT;
            set
            {
                _selectedIndexNoiCT = value;
                OnPropertyChanged();

                int index = 0;

                for (index = 0; index < ListCity.Count(); index++)
                {
                    if (index == SelectedIndexNoiCT)
                    {
                        GiaoVien.NoiCT = ListCity[index].name;
                        break;
                    }
                }
            }
        }

        private int _selectedIndexGCN;
        public int SelectedIndexGCN
        {
            get => _selectedIndexGCN;
            set
            {
                _selectedIndexGCN = value;
                OnPropertyChanged();


                int index = 0;

                for (index = 0; index < ListGCN.Count(); index++)
                {
                    if (index == SelectedIndexGCN)
                    {
                        GiaoVien.SoGCN = ListGCN[index].SoGCN.Trim();
                        break;
                    }
                }


            }
        }

        private int _selectedIndexTD;
        public int SelectedIndexTD
        {
            get => _selectedIndexTD;
            set
            {
                _selectedIndexTD = value;
                OnPropertyChanged();

                if (SelectedIndexTD == 0)
                {
                    GiaoVien.TuyenDung = "Hợp đồng";
                }
                else if (SelectedIndexTD == 1)
                {
                    GiaoVien.TuyenDung = "Biên chế";
                }
                else if (SelectedIndexTD == 2)
                {
                    GiaoVien.TuyenDung = "Thời vụ";
                }
            }
        }


        private GIAOVIEN _selectedItem;
        public GIAOVIEN SelectedItem
        {
            get => _selectedItem; set
            {
                _selectedItem = value;
                OnPropertyChanged();
                reset();

                if (SelectedItem != null)
                {
                    GiaoVien.MaGV = SelectedItem.MaGV;
                    GiaoVien.HoDem = SelectedItem.HoDem;
                    GiaoVien.TenGV = SelectedItem.TenGV;
                    GiaoVien.NgaySinh = SelectedItem.NgaySinh.Value;
                    GiaoVien.SoCCCD = SelectedItem.SoCCCD;
                    GiaoVien.NoiCT = SelectedItem.NoiCT;
                    GiaoVien.GioiTinh = SelectedItem.GioiTinh.Value;
                    GiaoVien.Phone = SelectedItem.Phone;
                    GiaoVien.TuyenDung = SelectedItem.TuyenDung;

                    int index = 0;

                    foreach (var item in ListCity)
                    {
                        if (item.name.CompareTo(GiaoVien.NoiCT) == 0)
                        {
                            SelectedIndexNoiCT = index;
                            GiaoVien.NoiCT = item.name;
                            break;
                        }

                        index++;
                    }

                    if (GiaoVien.TuyenDung.CompareTo("Hợp đồng") == 0)
                    {
                        SelectedIndexTD = 0;
                    }
                    else if (GiaoVien.TuyenDung.CompareTo("Biên chế") == 0)
                    {
                        SelectedIndexTD = 1;
                    }
                    else
                    {
                        SelectedIndexTD = 2;
                    }

                    GiaoVien.TrinhDo_VH = SelectedItem.TrinhDo_VH;
                    GiaoVien.TrinhDo_SP = SelectedItem.TrinhDo_SP;


                    if (!string.IsNullOrEmpty(SelectedItem.GV_LT.ToString()))
                    {
                        GiaoVien.GV_LT = SelectedItem.GV_LT.Value;
                        IsCheckedGV_LT = (Boolean)GiaoVien.GV_LT;
                    }

                    if (!string.IsNullOrEmpty(SelectedItem.GV_TH.ToString()))
                    {
                        GiaoVien.GV_TH = SelectedItem.GV_TH.Value;
                        IsCheckedGV_TH = (Boolean)GiaoVien.GV_TH;
                    }
                    GiaoVien.TrinhDo_CM = SelectedItem.TrinhDo_CM;
                    GiaoVien.Nganh_CM = SelectedItem.Nganh_CM;
                    GiaoVien.SoGCN = SelectedItem.SoGCN.Trim();
                    GiaoVien.MaSoGTVT = SelectedItem.MaSoGTVT;
                    GiaoVien.MaCSDT = SelectedItem.MaCSDT;
                    GiaoVien.TrangThai = SelectedItem.TrangThai.Value;

                    if (GiaoVien.TrangThai == true)
                    {
                        SelectedIndexStatus = 0;
                    }
                    else
                    {
                        SelectedIndexStatus = 1;
                    }

                    GiaoVien.AnhCD = SelectedItem.AnhCD;
                    SelectedImage = SelectedItem.AnhCD;
                    GiaoVien.NguoiCapNhat = User.Name;
                    listGCN();
                }
            }
        }

        public ICommand selectionChanged { get; set; }
        public ICommand textChanged { get; set; }
        public ICommand viewInsertTeacher { get; set; }
        public ICommand closeTeacherWindow { get; set; }
        public ICommand closeInsertTeacherWindow { get; set; }
        public ICommand selectedImage { get; set; }
        public ICommand insertTeacher { get; set; }
        public ICommand editTeacher { get; set; }
        public ICommand nextPage { get; set; }
        public ICommand previousPage { get; set; }
        public ICommand previewMouseLeftButtonUp { get; set; }
        public ICommand Checked { get; set; }
        public ICommand RemoveTeacher { get; set; }
        public ICommand viewGCN { get; set; }
        public ICommand ExportExcel { get; set; }
        public ICommand AgreeTeacherWindow { get; set; }
        public TeacherViewModel()
        {
            ListTeacher = new ObservableCollection<GIAOVIEN>();
            ListGCN = new ObservableCollection<GIAOVIEN_GCN>();
            ListAllGCN = new ObservableCollection<GIAOVIEN_GCN>();
            ListCity = new List<province_city>();
            User = MainViewModel.User;
            GiaoVien = new GIAOVIEN();

            GiaoVien.NgaySinh = DateTime.Now;
            GiaoVien.GioiTinh = 1;
            CurrentPage = 1;
            CurrentWindow = true;

            teachers();
            cities();
            listGCN();
            listAllGCN();

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
                        u.Workbook.Properties.Title = "DANH SÁCH TRÍCH NGANG GIÁO VIÊN DẠY LÁI XE";

                        //Tạo một sheet để làm việc trên đó
                        u.Workbook.Worksheets.Add("CTDV");

                        // lấy sheet vừa add ra để thao tác
                        ExcelWorksheet ws = u.Workbook.Worksheets[0];

                        // đặt tên cho sheet
                        ws.Name = "CTNH";
                        // fontsize mặc định cho cả sheet
                        ws.Cells.Style.Font.Size = 11;
                        // font family mặc định cho cả sheet
                        ws.Cells.Style.Font.Name = "Calibri";

                        // Tạo danh sách các column header
                        string[] arrColumnHeader = { "STT", "Họ và Tên", "Ngày tháng năm sinh", "Số giấy chứng minh nhân dân", "Biên chế (HTTD)", "Hợp đồng (HTTD)", "Văn hóa", "Chuyên môn", "Sư phạm", "Hạng giấy phép", "Ngày trúng tuyển", "Phân công giảng dạy", "Ghi chú" };

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


                        ws.Cells[4, 1].Value = "DANH SÁCH TRÍCH NGANG GIÁO VIÊN DẠY LÁI XE";
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

                        if (ListTeacher.Count > 0)
                        {
                            double? sum = 0.0d;


                            // với mỗi item trong danh sách sẽ ghi trên 1 dòng
                            List<GIAOVIEN> teachers = ListTeacher.Cast<GIAOVIEN>().ToList();
                            var stt = 1;

                            foreach (var item in teachers)
                            {
                                // bắt đầu ghi từ cột 1. Excel bắt đầu từ 1 không phải từ 0
                                colIndex = 1;

                                // rowIndex tương ứng từng dòng dữ liệu
                                rowIndex++;

                                //gán giá trị cho từng cell
                                ws.Cells[rowIndex, colIndex++].Value = stt;
                                ws.Cells[rowIndex, colIndex - 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                ws.Cells[rowIndex, colIndex - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Hair;
                                if (teachers.Count - stt == 0)
                                {
                                    ws.Cells[rowIndex, colIndex - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                }

                                ws.Cells[rowIndex, colIndex++].Value = item.HoDem + " " + item.TenGV;
                                ws.Cells[rowIndex, colIndex - 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                ws.Cells[rowIndex, colIndex - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Hair;
                                if (teachers.Count - stt == 0)
                                {
                                    ws.Cells[rowIndex, colIndex - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                }

                                ws.Cells[rowIndex, colIndex++].Value = item.NgaySinh.Value.Day + "/" + item.NgaySinh.Value.Month + "/" + item.NgaySinh.Value.Year;
                                ws.Cells[rowIndex, colIndex - 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                ws.Cells[rowIndex, colIndex - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Hair;
                                if (teachers.Count - stt == 0)
                                {
                                    ws.Cells[rowIndex, colIndex - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                }

                                ws.Cells[rowIndex, colIndex++].Value = item.SoCCCD;
                                ws.Cells[rowIndex, colIndex - 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                ws.Cells[rowIndex, colIndex - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Hair;
                                if (teachers.Count - stt == 0)
                                {
                                    ws.Cells[rowIndex, colIndex - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                }

                                if (item.TuyenDung.Equals("Biên chế"))
                                {
                                    ws.Cells[rowIndex, colIndex++].Value = item.TuyenDung;
                                    ws.Cells[rowIndex, colIndex - 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    ws.Cells[rowIndex, colIndex - 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                    ws.Cells[rowIndex, colIndex - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Hair;
                                    if (teachers.Count - stt == 0)
                                    {
                                        ws.Cells[rowIndex, colIndex - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                        ws.Cells[rowIndex, colIndex].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                    }
                                    colIndex++;
                                }
                                else
                                {
                                    colIndex++;

                                    ws.Cells[rowIndex, colIndex++].Value = item.TuyenDung;
                                    ws.Cells[rowIndex, colIndex - 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    ws.Cells[rowIndex, colIndex - 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                    ws.Cells[rowIndex, colIndex - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Hair;

                                    if (teachers.Count - stt == 0)
                                    {
                                        ws.Cells[rowIndex, colIndex].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                    }
                                }

                                ws.Cells[rowIndex, colIndex++].Value = item.TrinhDo_VH;
                                ws.Cells[rowIndex, colIndex - 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                ws.Cells[rowIndex, colIndex - 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                ws.Cells[rowIndex, colIndex - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Hair;
                                if (teachers.Count - stt == 0)
                                {
                                    ws.Cells[rowIndex, colIndex - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                }

                                ws.Cells[rowIndex, colIndex++].Value = item.TrinhDo_CM;
                                ws.Cells[rowIndex, colIndex - 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                ws.Cells[rowIndex, colIndex - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Hair;
                                if (teachers.Count - stt == 0)
                                {
                                    ws.Cells[rowIndex, colIndex - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                }

                                ws.Cells[rowIndex, colIndex++].Value = item.TrinhDo_SP;
                                ws.Cells[rowIndex, colIndex - 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                ws.Cells[rowIndex, colIndex - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Hair;
                                if (teachers.Count - stt == 0)
                                {
                                    ws.Cells[rowIndex, colIndex - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                }

                                foreach (var gcn in ListAllGCN)
                                {
                                    if (item.SoGCN != null)
                                    {
                                        if (gcn.SoGCN.Trim().Equals(item.SoGCN.Trim()))
                                        {
                                            ws.Cells[rowIndex, colIndex++].Value = gcn.HangXe;
                                            ws.Cells[rowIndex, colIndex - 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                            ws.Cells[rowIndex, colIndex - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Hair;
                                            if (teachers.Count - stt == 0)
                                            {
                                                ws.Cells[rowIndex, colIndex - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                            }

                                            ws.Cells[rowIndex, colIndex++].Value = gcn.NgayCap.Value.Day + "/" + gcn.NgayCap.Value.Month + "/" + gcn.NgayCap.Value.Year; ;
                                            ws.Cells[rowIndex, colIndex - 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                            ws.Cells[rowIndex, colIndex - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Hair;
                                            if (teachers.Count - stt == 0)
                                            {
                                                ws.Cells[rowIndex, colIndex - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                            }
                                        }
                                    }
                                }

                                if (item.GV_LT == true && item.GV_TH == false)
                                {
                                    ws.Cells[rowIndex, colIndex++].Value = "Dạy Lý Thuyết";
                                    ws.Cells[rowIndex, colIndex - 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    ws.Cells[rowIndex, colIndex - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Hair;
                                    ws.Cells[rowIndex, colIndex].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                }
                                else if (item.GV_TH == true && item.GV_LT == false)
                                {
                                    ws.Cells[rowIndex, colIndex++].Value = "Dạy Thực Hành";
                                    ws.Cells[rowIndex, colIndex - 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    ws.Cells[rowIndex, colIndex - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Hair;
                                    ws.Cells[rowIndex, colIndex].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                }
                                else
                                {
                                    ws.Cells[rowIndex, colIndex++].Value = "Dạy Lý thuyết và Thực Hành";
                                    ws.Cells[rowIndex, colIndex - 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    ws.Cells[rowIndex, colIndex - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Hair;
                                    ws.Cells[rowIndex, colIndex].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                }

                                if (teachers.Count - stt == 0)
                                {
                                    ws.Cells[rowIndex, colIndex - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                    ws.Cells[rowIndex, colIndex].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                }
                                // Tự căn độ dài cột
                                ws.Cells.AutoFitColumns();
                                // In đậm chữ cho hàng tiêu đề
                                ws.Cells[5,1,5,14].Style.Font.Bold = true;
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
                            string fileName = "giaovien.xlsx";
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


            //remove teacher get out list
            RemoveTeacher = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                using (db = new giaothongEntities())
                {
                    try
                    {
                        var message = MessageBox.Show("Bạn có thật sự muốn xóa giáo viên này khỏi danh sách ?", "Thông Báo", MessageBoxButton.OKCancel, MessageBoxImage.Question);

                        if (MessageBoxResult.OK == message)
                        {
                            var kh = db.GIAOVIENs.Find(GiaoVien.MaGV.Trim());
                            if (kh != null)
                            {
                                db.GIAOVIENs.Remove(kh);
                                db.SaveChanges();

                                var index = ListTeacher.ToList().FindIndex(t => t.MaGV == GiaoVien.MaGV);
                                ListTeacher.RemoveAt(index);
                                listGCN();

                                MessageBox.Show("Giáo viên đã được xóa khỏi danh sách", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Information);
                                p.Close();
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Không thể xóa giáo viên lúc này", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            });

            //open window edit teacher
            previewMouseLeftButtonUp = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                if (SelectedItem != null)
                {
                    CurrentWindow = true;
                    listGCN();
                    p.Hide();
                    EditTeacherWindow teacher = new EditTeacherWindow();
                    teacher.ShowDialog();
                    p.ShowDialog();
                }
            });

            //view gcn
            viewGCN = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Hide();
                TeacherGCNWindow teacherGCN = new TeacherGCNWindow();
                teacherGCN.ShowDialog();
                teachers();
                listGCN();
                p.ShowDialog();
            });

            //close view teacher window
            closeTeacherWindow = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });

            //close view insert teacher window
            closeInsertTeacherWindow = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
                SelectedItem = null;
            });

            //selectected change filter status teacher
            selectionChanged = new RelayCommand<string>((p) => { return true; }, (p) =>
            {
                var selectedValue = changeSelectedStatus(SelectedIndex);
                teachers(selectedValue);
            });

            //find teacher by ID or CCCD or TenGV
            textChanged = new RelayCommand<string>((p) => { return true; }, (p) =>
            {
                var selectedValue = changeSelectedStatus(SelectedIndex);
                teachers(selectedValue, SearchTeacher);
            });

            //load view insert teacher 
            viewInsertTeacher = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                getMaxMaGV();
                reset();
                

                if (ListGCN.Count > 0)
                {
                    GiaoVien.SoGCN = ListGCN[0].SoGCN.Trim();
                }

                if (ListCity.Count > 0)
                {
                    GiaoVien.NoiCT = ListCity[0].name.Trim();
                }

                CurrentWindow = false;
                SelectedIndexStatus = 1;
                listGCN();
                p.Hide();
                InsertTeacher inserTeacher = new InsertTeacher();
                inserTeacher.ShowDialog();
                p.ShowDialog();
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
                    var newCurrentUrl = currentDirectory.Substring(0, index) + @"giaothong\images\avatar";

                    string filePath = Path.Combine(newCurrentUrl, nameFile);

                    if (!File.Exists(filePath))
                    {
                        newCurrentUrl = newCurrentUrl + "\\" + nameFile;
                        File.Copy(url, newCurrentUrl);
                    }

                    var indexCurrentUrl = newCurrentUrl.IndexOf("images");

                    SelectedImage = newCurrentUrl;

                    GiaoVien.AnhCD = "/Images/avatar/" + nameFile;
                }
                catch { }
            });

            //insert teacher
            insertTeacher = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                GiaoVien.NgayCapNhat = DateTime.Now;

                bool check = validation();

                if (check)
                {
                    using (db = new giaothongEntities())
                    {
                        try
                        {
                            db.GIAOVIENs.Add(GiaoVien);
                            db.SaveChanges();
                            listGV();
                            p.Close();
                        }
                        catch
                        {
                            MessageBox.Show("Thêm giáo viên thất bại", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            });

            AgreeTeacherWindow = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                using (db = new giaothongEntities())
                {
                    try
                    {

                        var kh = db.GIAOVIENs.Find(GiaoVien.MaGV.Trim());

                        if (kh != null)
                        {
                            kh.TrangThai = true;
                            kh.NgayDuyet = DateTime.Now;
                            kh.NguoiDuyet = User.Name;

                            db.SaveChanges();
                            teachers();

                            p.Close();
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Cập nhật thông tin giáo viên thất bại", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            });

            //edit teacher 
            editTeacher = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {

                using (db = new giaothongEntities())
                {
                    try
                    {
                        var check = validation();

                        if (check)
                        {

                            var kh = db.GIAOVIENs.Find(GiaoVien.MaGV.Trim());

                            if (kh != null)
                            {
                                kh.HoDem = GiaoVien.HoDem.Trim();
                                kh.TenGV = GiaoVien.TenGV.Trim();
                                kh.NgaySinh = GiaoVien.NgaySinh;
                                kh.SoCCCD = GiaoVien.SoCCCD.Trim();
                                kh.NoiCT = GiaoVien.NoiCT.Trim();
                                kh.GioiTinh = GiaoVien.GioiTinh;
                                kh.Phone = GiaoVien.Phone.Trim();
                                kh.TuyenDung = GiaoVien.TuyenDung.Trim();
                                kh.TrinhDo_SP = GiaoVien.TrinhDo_SP.Trim();
                                kh.TrinhDo_CM = GiaoVien.TrinhDo_CM.Trim();
                                kh.TrinhDo_VH = GiaoVien.TrinhDo_VH.Trim();
                                kh.Nganh_CM = GiaoVien.Nganh_CM.Trim();
                                kh.GV_LT = GiaoVien.GV_LT;
                                kh.GV_TH = GiaoVien.GV_TH;
                                kh.SoGCN = GiaoVien.SoGCN.Trim();
                                kh.MaSoGTVT = GiaoVien.MaSoGTVT.Trim();
                                kh.MaCSDT = GiaoVien.MaCSDT.Trim();
                                kh.TrangThai = GiaoVien.TrangThai;

                                var checkFile = GiaoVien.AnhCD.Contains("giaothong");

                                if (checkFile)
                                {
                                    var index = GiaoVien.AnhCD.IndexOf("Images");
                                    GiaoVien.AnhCD = GiaoVien.AnhCD.Remove(0, index - 1);
                                }

                                kh.AnhCD = GiaoVien.AnhCD;
                                kh.NgayCapNhat = DateTime.Now;
                                kh.NguoiCapNhat = User.Name;

                                
                                db.SaveChanges();
                                teachers();
                                listGCN();

                                p.Close();
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Cập nhật thông tin giáo viên thất bại", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            });


            //next page
            nextPage = new RelayCommand<string>((p) => { return true; }, (p) =>
            {
                if (CurrentPage < TotalPages)
                {
                    CurrentPage++;
                    var selectedValue = changeSelectedStatus(SelectedIndex);
                    teachers(selectedValue, SearchTeacher);
                }
            });

            //previous page
            previousPage = new RelayCommand<string>((p) => { return true; }, (p) =>
            {
                if (CurrentPage > 1)
                {
                    CurrentPage--;
                    var selectedValue = changeSelectedStatus(SelectedIndex);
                    teachers(selectedValue, SearchTeacher);
                }
            });
        }

        //get list teacher GCN
        public ObservableCollection<GIAOVIEN_GCN> listAllGCN()
        {
            using (db = new giaothongEntities())
            {
                ListAllGCN.Clear();

                var gcn = from c in db.GIAOVIEN_GCN
                          select c;

                gcn.ToList().ForEach(p =>
                {
                    ListAllGCN.Add(p);
                });

                return ListAllGCN;
            }
        }

        //get list teacher GCN
        public ObservableCollection<GIAOVIEN_GCN> listGCN()
        {
            using (db = new giaothongEntities())
            {
                ListGCN.Clear();
                        var gcn = from c in db.GIAOVIEN_GCN
                                  where !db.GIAOVIENs.Any(v => v.SoGCN == c.SoGCN) || (GiaoVien.SoGCN == c.SoGCN && CurrentWindow == true)
                                  select c;
                        gcn.ToList().ForEach(p =>
                        {
                            ListGCN.Add(p);
                        });

                return ListGCN;
            }
        }

        //validate information teacher
        public bool validation()
        {
            bool check = true;

            var checkBirthDay = checkTypeDate(DateTime.Parse(GiaoVien.NgaySinh.ToString()));

            if (!checkBirthDay)
            {
                check = false;
                MessageBox.Show("Giáo viên phải đủ từ 15 tuổi trở lên!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (!checkLength(GiaoVien.HoDem, 25))
            {
                check = false;
                MessageBox.Show("Họ đệm không hợp lệ hoặc phải dưới 25 ký tự!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (!checkLength(GiaoVien.TenGV, 25))
            {
                check = false;
                MessageBox.Show("Tên đệm không hợp lệ hoặc phải dưới 15 ký tự!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (!checkLength(GiaoVien.Phone.Trim(), 14))
            {
                check = false;
                MessageBox.Show("Số điện thoại không hợp lệ!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (!checkLength(GiaoVien.TrinhDo_VH, 10))
            {
                check = false;
                MessageBox.Show("Trình độ văn hóa không hợp lệ hoặc phải dưới 10 ký tự!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (!checkLength(GiaoVien.TrinhDo_SP, 10))
            {
                check = false;
                MessageBox.Show("Trình độ sư phạm không hợp lệ hoặc phải dưới 10 ký tự!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (!checkLength(GiaoVien.TrinhDo_CM, 10))
            {
                check = false;
                MessageBox.Show("Trình độ chuyên môn không hợp lệ hoặc phải dưới 10 ký tự!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (!checkLength(GiaoVien.Nganh_CM, 20))
            {
                check = false;
                MessageBox.Show("Ngành chuyên môn không hợp lệ hoặc phải dưới 20 ký tự!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (!checkLength(GiaoVien.MaSoGTVT, 5))
            {
                check = false;
                MessageBox.Show("Mã sở giao thông vận tải không hợp lệ hoặc phải dưới 5 ký tự!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (!checkLength(GiaoVien.MaCSDT, 5))
            {
                check = false;
                MessageBox.Show("Mã cơ sở đào tạo không hợp lệ hoặc phải dưới 5 ký tự!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (GiaoVien.GV_LT == false && GiaoVien.GV_LT == false)
            {
                check = false;
                MessageBox.Show("Vui lòng chọn loại giáo viên dạy!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return check;
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

        //check type date
        public bool checkTypeDate(DateTime date)
        {
            DateTime dateNow = DateTime.Now;

            if (dateNow.Year - date.Year < 15)
            {
                return false;
            }

            return true;
        }

        //check number GCN exists
        public bool checkExistsSoGCN(string soGCN)
        {
            using (db = new giaothongEntities())
            {
                var query = (from gcn in db.GIAOVIENs where gcn.SoGCN == soGCN select gcn).Count();

                if (query == 1)
                {
                    return false;
                }
            }

            return true;
        }

        //check teacher is teaching csdt another
        public bool checkExists(string value)
        {
            using (db = new giaothongEntities())
            {
                var query = (from gv in db.GIAOVIENs where gv.MaGV == GiaoVien.MaGV && gv.MaCSDT == value || gv.SoCCCD == value select gv).Count();

                if (query > 0)
                {
                    return false;
                }
            }

            return true;
        }

        //get MaGV max
        public string getMaxMaGV()
        {
            var dateNow = DateTime.Now.ToString("yyyy/MM/dd").Replace("/", "");

            if (ListTeacher.Count > 0)
            {
                var maxMaGV = ListTeacher.Max(p => p.MaGV).Trim();
                var arrMaGV = maxMaGV.Split('-');

                if (arrMaGV.Length > 0)
                {
                    GiaoVien.MaGV = arrMaGV[0] + "-" + dateNow + "-" + (Int32.Parse(arrMaGV[2]) + 1);
                }
            }
            else
            {
                GiaoVien.MaGV = "52-" + dateNow + "-1";
            }
            return GiaoVien.MaGV;
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

        //get cities
        public List<province_city> cities()
        {
            using (db = new giaothongEntities())
            {
                try
                {
                    var teachers = from ct in db.province_city select ct;

                    teachers.ToList().ForEach(p =>
                    {
                        province_city ct = new province_city
                        {
                            matp = p.matp,
                            name = p.name,
                            type = p.type,
                            slug = p.slug,
                        };

                        ListCity.Add(ct);
                    });
                }
                catch { }
            }

            return ListCity;
        }


        //get value with index combobox
        public string changeSelectedStatus(int index)
        {
            var value = "";

            switch (index)
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
                default:
                    value = "Tất cả";
                    break;
            }

            return value;
        }

        //get list teacher

        public void teachers(string keyword = "tất cả", string searchKey = null)
        {
            using (db = new giaothongEntities())
            {
                try
                {
                    ListTeacher.Clear();

                    if (keyword.ToLower() == "tất cả")
                    {
                        var teachers = from gv in db.GIAOVIENs select gv;

                        if (!string.IsNullOrEmpty(searchKey))
                        {
                            teachers = from gv in db.GIAOVIENs where gv.MaGV == searchKey || gv.SoCCCD == searchKey || gv.TenGV == searchKey select gv;
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

        public void reset()
        {
            GiaoVien.TenGV = null;
            GiaoVien.HoDem = null;
            GiaoVien.SoCCCD = null;
            GiaoVien.NoiCT = null;
            GiaoVien.GioiTinh = 0;
            GiaoVien.Phone = null;
            GiaoVien.TuyenDung = null;
            GiaoVien.TrinhDo_VH = null;
            GiaoVien.TrinhDo_CM = null;
            GiaoVien.Nganh_CM = null;
            GiaoVien.TrinhDo_SP = null;
            GiaoVien.GV_LT = false;
            GiaoVien.GV_TH = false;
            GiaoVien.SoGCN = null;
            GiaoVien.MaSoGTVT = null;
            GiaoVien.MaCSDT = null;
            GiaoVien.TrangThai = false;
            GiaoVien.AnhCD = null;
            IsCheckedGV_LT = false;
            IsCheckedGV_TH = false;
            SelectedImage = null;
            SelectedIndexGCN = -1;
            SelectedIndexNoiCT = -1;
            SelectedIndexTD = -1;
        }

        //add value to list teacher
        public void addValueToListTeacher(List<GIAOVIEN> teachers)
        {
            TotalPages = (int)Math.Ceiling((double)teachers.Count / pageSize);

            var list = teachers.Skip((CurrentPage - 1) * pageSize).Take(pageSize);
            string currentDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            var index = currentDirectory.IndexOf("giaothong");
            var newCurrentUrl = currentDirectory.Substring(0, index) + @"giaothong";

            list.ToList().ForEach(p =>
            {
                if (p != null)
                {
                    p.AnhCD = newCurrentUrl + p.AnhCD;
                    ListTeacher.Add(p);
                }
            });
        }


        public ObservableCollection<GIAOVIEN> listGV(string searchKey = null)
        {
            using (db = new giaothongEntities())
            {
                ListTeacher.Clear();

                try
                {
                    var gv = from c in db.GIAOVIENs select c;

                    if (!string.IsNullOrEmpty(searchKey))
                    {
                        gv = from c in db.GIAOVIENs where c.MaGV.Equals(searchKey) select c;
                    }

                    addValueToListTeacher(gv.ToList());

                }
                catch { }

                return ListTeacher;
            }
        }
    }
}
