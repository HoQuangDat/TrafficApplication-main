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
    
    public partial class DM_HangGPLX
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DM_HangGPLX()
        {
            this.KhoaHocs = new HashSet<KhoaHoc>();
            this.NguoiLX_GPLX = new HashSet<NguoiLX_GPLX>();
            this.NguoiLX_HoSo = new HashSet<NguoiLX_HoSo>();
        }
    
        public string MaHang { get; set; }
        public string TenHang { get; set; }
        public int HanSuDung { get; set; }
        public string MoTaVN { get; set; }
        public string MoTaEN { get; set; }
        public string GhiChu { get; set; }
        public bool TrangThai { get; set; }
        public string NguoiTao { get; set; }
        public string NguoiSua { get; set; }
        public System.DateTime NgayTao { get; set; }
        public System.DateTime NgaySua { get; set; }
        public string HangDuocLai { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KhoaHoc> KhoaHocs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NguoiLX_GPLX> NguoiLX_GPLX { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NguoiLX_HoSo> NguoiLX_HoSo { get; set; }
    }
}
