using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebsiteBanHang.CSDL;

namespace WebsiteBanHang.Models
{
    public class HomeModel
    {
        public List<DanhMuc> ListDanhMuc { get; set; }
        public List<SanPham> ListSanPham { get; set; }
        public List<NhaCungCap> ListNhaCungCap { get; set; }
        public List<BaiViet> ListBaiViet { get; set; }

        public List<BaiViet> ListFullBaiViet { get; set; }

        public String MaSP { get; set; }

        public int SoLuong { get; set; }
    }
}