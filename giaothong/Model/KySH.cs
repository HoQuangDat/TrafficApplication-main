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
    
    public partial class KySH
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KySH()
        {
            this.NguoiLX_HoSo = new HashSet<NguoiLX_HoSo>();
        }
    
        public string MaKySH { get; set; }
        public string MaTTSH { get; set; }
        public System.DateTime NgaySH { get; set; }
        public Nullable<int> GioSH { get; set; }
        public string SoQD { get; set; }
        public Nullable<System.DateTime> NgayQD { get; set; }
        public string NguoiQD { get; set; }
        public string ChuTich_HDSH { get; set; }
        public string PhoChuTich_HDSH { get; set; }
        public string UV_GD_TTSH { get; set; }
        public string UV_ToTruong { get; set; }
        public string UV_ThuKy { get; set; }
        public Nullable<int> TongSoDK { get; set; }
        public Nullable<int> SoDuSH { get; set; }
        public Nullable<int> SoDat { get; set; }
        public Nullable<int> SoKhongDat { get; set; }
        public Nullable<int> SoVang { get; set; }
        public Nullable<int> SoVangThiHinh { get; set; }
        public Nullable<int> SoVangThiDuong { get; set; }
        public Nullable<int> SoDuSHLanDau { get; set; }
        public Nullable<int> SoDatSHLanDau { get; set; }
        public Nullable<int> TyLeDat { get; set; }
        public string SoViPham { get; set; }
        public Nullable<int> SHHaiLan { get; set; }
        public string NX_ThucHien_QuyChe { get; set; }
        public string NX_TrinhDo_CBSH { get; set; }
        public string NX_CoSo_VCKT { get; set; }
        public string NX_DamBao_AnToan { get; set; }
        public string NX_VanDe_Khac { get; set; }
        public string NhanXet { get; set; }
        public Nullable<int> LePhi_LyThuyet { get; set; }
        public Nullable<int> LePhi_ThiHinh { get; set; }
        public Nullable<int> LePhi_ThiDuong { get; set; }
        public Nullable<int> LePhi_CapGPLX { get; set; }
        public string GhiChu { get; set; }
        public Nullable<int> TrangThai { get; set; }
        public string NguoiTao { get; set; }
        public string NguoiSua { get; set; }
        public System.DateTime NgayTao { get; set; }
        public System.DateTime NgaySua { get; set; }
        public string SoQDTT { get; set; }
        public Nullable<System.DateTime> NgayQDTT { get; set; }
        public string TenKySH { get; set; }
        public Nullable<int> TT_Xuly { get; set; }
    
        public virtual DM_DonViGTVT DM_DonViGTVT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NguoiLX_HoSo> NguoiLX_HoSo { get; set; }
    }
}
