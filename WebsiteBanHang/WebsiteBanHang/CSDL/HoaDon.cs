//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebsiteBanHang.CSDL
{
    using System;
    using System.Collections.Generic;
    
    public partial class HoaDon
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HoaDon()
        {
            this.CTHoaDons = new HashSet<CTHoaDon>();
        }
    
        public string MaHD { get; set; }
        public System.DateTime NgayDat { get; set; }
        public System.DateTime NgayGiao { get; set; }
        public string SoDT { get; set; }
        public string DiaChi { get; set; }
        public string GhiChu { get; set; }
        public string MaTT { get; set; }
        public string MaNV { get; set; }
        public string MaKH { get; set; }
        public double ThanhToan { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTHoaDon> CTHoaDons { get; set; }
        public virtual KhachHang KhachHang { get; set; }
        public virtual NhanVien NhanVien { get; set; }
        public virtual TrangThai TrangThai { get; set; }
    }
}
