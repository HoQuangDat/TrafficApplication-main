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
    
    public partial class GIAOVIEN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GIAOVIEN()
        {
            this.KhoaHoc_GiaoVien = new HashSet<KhoaHoc_GiaoVien>();
        }
    
        public string MaGV { get; set; }
        public string HoDem { get; set; }
        public string TenGV { get; set; }
        public Nullable<System.DateTime> NgaySinh { get; set; }
        public string SoCCCD { get; set; }
        public string NoiCT { get; set; }
        public Nullable<int> GioiTinh { get; set; }
        public string Phone { get; set; }
        public string TuyenDung { get; set; }
        public string TrinhDo_VH { get; set; }
        public string Nganh_CM { get; set; }
        public string TrinhDo_SP { get; set; }
        public Nullable<bool> GV_LT { get; set; }
        public Nullable<bool> GV_TH { get; set; }
        public string SoGCN { get; set; }
        public string MaSoGTVT { get; set; }
        public string MaCSDT { get; set; }
        public Nullable<bool> TrangThai { get; set; }
        public string AnhCD { get; set; }
        public string NguoiCapNhat { get; set; }
        public Nullable<System.DateTime> NgayCapNhat { get; set; }
        public string NguoiDuyet { get; set; }
        public Nullable<System.DateTime> NgayDuyet { get; set; }
        public string TrinhDo_CM { get; set; }
    
        public virtual GIAOVIEN_GCN GIAOVIEN_GCN { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KhoaHoc_GiaoVien> KhoaHoc_GiaoVien { get; set; }
        public string FullName { get;  set; }
        public string Training { get;  set; }
    }
}
