using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteBanHang.Models
{
    public class CartItem
    {
        public string HinhDD { get; set; }
        public String MaSP { get; set; }
        public string TenSP { get; set; }
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