//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace giaothong.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class KhoaHoc
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KhoaHoc()
        {
            this.CaDays = new HashSet<CaDay>();
            this.KhoaHoc_GiaoVien = new HashSet<KhoaHoc_GiaoVien>();
            this.PhongDays = new HashSet<PhongDay>();
            this.SanTaps = new HashSet<SanTap>();
            this.LichHocs = new HashSet<LichHoc>();
        }
    
        public string MaKH { get; set; }
        public string MaCSDT { get; set; }
        public string MaSoGTVT { get; set; }
        public string TenKH { get; set; }
        public string HangGPLX { get; set; }
        public string HangDT { get; set; }
        public string SoQD_KhaiGiang { get; set; }
        public Nullable<System.DateTime> NgayQD_KhaiGiang { get; set; }
        public Nullable<System.DateTime> NgayKG { get; set; }
        public Nullable<System.DateTime> NgayBG { get; set; }
        public string MucTieuDT { get; set; }
        public Nullable<System.DateTime> NgayThi { get; set; }
        public Nullable<System.DateTime> NgaySH { get; set; }
        public Nullable<int> TongSoHV { get; set; }
        public Nullable<int> SoHVTotNghiep { get; set; }
        public Nullable<int> SoHVDuocCapGPLX { get; set; }
        public Nullable<int> ThoiGianDT { get; set; }
        public Nullable<int> SoNgayOnKT { get; set; }
        public Nullable<int> SoNgayThucHoc { get; set; }
        public Nullable<int> SoNgayNghiLe { get; set; }
        public Nullable<int> TongSoNgay { get; set; }
        public string GhiChu { get; set; }
        public bool TrangThai { get; set; }
        public string getTrangThai { get; set; }
        public string NguoiTao { get; set; }
        public string NguoiSua { get; set; }
        public System.DateTime NgayTao { get; set; }
        public System.DateTime NgaySua { get; set; }
        public Nullable<int> TT_Xuly { get; set; }
        public string getXuly { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CaDay> CaDays { get; set; }
        public virtual DM_DonViGTVT DM_DonViGTVT { get; set; }
        public virtual DM_DonViGTVT DM_DonViGTVT1 { get; set; }
        public virtual DM_HangDT DM_HangDT { get; set; }
        public virtual DM_HangGPLX DM_HangGPLX { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KhoaHoc_GiaoVien> KhoaHoc_GiaoVien { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhongDay> PhongDays { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SanTap> SanTaps { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LichHoc> LichHocs { get; set; }
    }
}