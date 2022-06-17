using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.CSDL;
using WebsiteBanHang.Models;

namespace WebsiteBanHang.Controllers
{
    public class ThanhToanController : Controller
    {
        QLTP3Entities2 objQLTPEntities = new QLTP3Entities2();

        // GET: ThanhToan

        public ActionResult NhapThongTin(String SDT, String DiaChiNhan, String GhiChu)
        {
            if (Session["MaKH"] == null)
            {
                return RedirectToAction("DangNhapKH", "Home");
            }
            else
            {
                // lay thong tin doi tuong tu bien session
                var lstCart = (List<CartItem>)Session["giohang"];
                // gan du lieu cho Order
                HoaDon objOrder = new HoaDon();
                objOrder.MaHD = "DH" + DateTime.Now.ToString("yyyyMMddHHmmss");
                objOrder.MaKH = Session["MaKH"].ToString();
                objOrder.NgayDat = DateTime.Now;
                objOrder.NgayGiao = DateTime.Now;
                objOrder.SoDT = SDT;
                objOrder.DiaChi = DiaChiNhan;
                objOrder.GhiChu = GhiChu;
                objOrder.MaNV = "NV02";
                objOrder.MaTT = "TT1";
                foreach (var item in lstCart)
                {
                    objOrder.ThanhToan = item.DonGia * item.SoLuong;
                }
                objQLTPEntities.HoaDons.Add(objOrder);
                // luu thong tin du lieu vao bang HoaDon
                objQLTPEntities.SaveChanges();

                // lay OrderID vua moi tao de luu vao bang Chi tiet hoa don
                String StringHoaDonId = objOrder.MaHD;

                List<CTHoaDon> lstCTHoaDon = new List<CTHoaDon>();

                foreach (var item in lstCart)
                {
                    CTHoaDon obj = new CTHoaDon();
                    obj.SoLuong = item.SoLuong;
                    obj.MaHD = StringHoaDonId;
                    obj.MaSP = item.MaSP;
                    obj.GiaTien = item.DonGia * item.SoLuong;
                    lstCTHoaDon.Add(obj);
                }
                objQLTPEntities.CTHoaDons.AddRange(lstCTHoaDon);
                objQLTPEntities.SaveChanges();
            }
            return RedirectToAction("ThanhCong", "ThanhToan");
        }

        public ActionResult ThanhCong()
        {
            return View();
        }

    }
}