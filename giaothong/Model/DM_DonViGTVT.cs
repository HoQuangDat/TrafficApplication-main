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
    
    public partial class DM_DonViGTVT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DM_DonViGTVT()
        {
            this.KhoaHocs = new HashSet<KhoaHoc>();
            this.KhoaHocs1 = new HashSet<KhoaHoc>();
            this.KySHes = new HashSet<KySH>();
            this.NguoiLX_GPLX = new HashSet<NguoiLX_GPLX>();
            this.NguoiLX_GPLX1 = new HashSet<NguoiLX_GPLX>();
            this.NguoiLX_GPLX2 = new HashSet<NguoiLX_GPLX>();
            this.NguoiLX_HoSo = new HashSet<NguoiLX_HoSo>();
            this.NguoiLX_HoSo1 = new HashSet<NguoiLX_HoSo>();
            this.NguoiLX_HoSo2 = new HashSet<NguoiLX_HoSo>();
        }
    
        public string MaDV { get; set; }
        public string MaDVQL { get; set; }
        public string LoaiDV { get; set; }
        public string TenDV { get; set; }
        public string CoQuanQL { get; set; }
        public Nullable<int> LoaiTTSH { get; set; }
        public string CacHangGPLX { get; set; }
        public string DienThoai { get; set; }
        public string Fax { get; set; }
        public string DiaChi { get; set; }
        public Nullable<int> LuuLuongDT { get; set; }
        public string SoGP { get; set; }
        public Nullable<System.DateTime> NgayGP { get; set; }
        public string LanhDao { get; set; }
        public string GhiChu { get; set; }
        public bool TrangThai { get; set; }
        public string NguoiTao { get; set; }
        public string NguoiSua { get; set; }
        public System.DateTime NgayTao { get; set; }
        public System.DateTime NgaySua { get; set; }
        public string Website { get; set; }
        public Nullable<int> DienTichSanTap { get; set; }
        public Nullable<System.DateTime> NgayHHGP { get; set; }
        public string DiaDiemDaoTao { get; set; }
        public string MaDvOld { get; set; }
        public byte[] ROW_VERSION { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KhoaHoc> KhoaHocs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KhoaHoc> KhoaHocs1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KySH> KySHes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NguoiLX_GPLX> NguoiLX_GPLX { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NguoiLX_GPLX> NguoiLX_GPLX1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NguoiLX_GPLX> NguoiLX_GPLX2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NguoiLX_HoSo> NguoiLX_HoSo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NguoiLX_HoSo> NguoiLX_HoSo1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NguoiLX_HoSo> NguoiLX_HoSo2 { get; set; }
    }
}
