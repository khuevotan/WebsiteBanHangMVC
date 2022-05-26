using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteBanHang.Models
{
    public class CartItem
    {
        public String HinhDD { get; set; }
        public String MaSP { get; set; }
        public String TenSP { get; set; }

        public String SDT{ get; set; }

        public String DiaChi { get; set; }

        public String GhiChu { get; set; }

        public float DonGia { get; set; }
        public int SoLuong { get; set; }
        public float ThanhTien
        {
            get
            {
                return SoLuong * DonGia;
            }
        }


    }
}