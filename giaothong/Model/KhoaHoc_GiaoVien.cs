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
    
    public partial class KhoaHoc_GiaoVien
    {
        public int ID { get; set; }
        public string MaKH { get; set; }
        public string MaGV { get; set; }
        public string BienSoXe { get; set; }
        public Nullable<int> SoHV { get; set; }
        public Nullable<bool> TrangThai { get; set; }
        public string NguoiTao { get; set; }
        public string NguoiSua { get; set; }
        public Nullable<System.DateTime> NgayTao { get; set; }
        public Nullable<System.DateTime> NgaySua { get; set; }
        public string MaPD { get; set; }
        public string MaCD { get; set; }
        public string MaST { get; set; }
    
        public virtual CaDay CaDay { get; set; }
        public virtual GIAOVIEN GIAOVIEN { get; set; }
        public virtual KhoaHoc KhoaHoc { get; set; }
        public virtual PhongDay PhongDay { get; set; }
        public virtual SanTap SanTap { get; set; }
    }
}
