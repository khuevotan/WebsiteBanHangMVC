using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebsiteBanHang.CSDL;

namespace WebsiteBanHang.Models
{
    public class HomeModel
    {
        public List<NhanVien> ListNhanVien { get; set; }
        public List<DanhMuc> ListDanhMuc { get; set; }
        public List<SanPham> ListSanPham { get; set; }
        public List<NhaCungCap> ListNhaCungCap { get; set; }
        public List<BaiViet> ListBaiViet { get; set; }

        public List<HoaDon> ListHoaDon { get; set; }
        public List<CTHoaDon> ListCTHoaDon { get; set; }

        public List<BaiViet> ListFullBaiViet { get; set; }

        public List<KhachHang> ListKhachHang { get; set; }
        public List<TrangThai> ListTrangThai { get; set; }


        public String HinhDD { get; set; }

        public String MaSP { get; set; }

        public String MaKH { get; set; }

     
        public String TenSP { get; set; }

        public int SoLuong { get; set; }

     
    

    }
}