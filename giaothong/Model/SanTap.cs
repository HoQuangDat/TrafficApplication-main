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
    
    public partial class SanTap
    {
        public string MaST { get; set; }
        public string MaKH { get; set; }
        public string SanTap1 { get; set; }
    
        public virtual KhoaHoc KhoaHoc { get; set; }
    }
}