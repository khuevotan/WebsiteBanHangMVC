using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.CSDL;
using WebsiteBanHang.Models;

namespace WebsiteBanHang.Controllers
{
    public class GioHangController : Controller
    {
        QLTPEntities1 objQLTPEntities = new QLTPEntities1();

        public ActionResult Index()
        {
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            return View(giohang);

        }

        public RedirectToRouteResult ThemVaoGio(String MaSP)
        {
            if (Session["giohang"] == null) // Nếu giỏ hàng chưa được khởi tạo
            {
                Session["giohang"] = new List<CartItem>();  // Khởi tạo Session["giohang"] là 1 List<CartItem>
            }

            List<CartItem> giohang = Session["giohang"] as List<CartItem>;  // Gán qua biến giohang dễ code

            // Kiểm tra xem sản phẩm khách đang chọn đã có trong giỏ hàng chưa

            if (giohang.FirstOrDefault(m => m.MaSP == MaSP) == null) // ko co sp nay trong gio hang
            {
                SanPham sp = objQLTPEntities.SanPhams.Find(MaSP);  // tim sp theo MaSP

                CartItem newItem = new CartItem()
                {
                    MaSP = MaSP,
                    TenSP = sp.TenSP,
                    SoLuong = 1,
                    HinhDD = sp.HinhDD,
                    DonGia = (float)sp.GiaBan

                };  // Tạo ra 1 CartItem mới

                giohang.Add(newItem);  // Thêm CartItem vào giỏ 
            }
            else
            {
                // Nếu sản phẩm khách chọn đã có trong giỏ hàng thì không thêm vào giỏ nữa mà tăng số lượng lên.
                CartItem cardItem = giohang.FirstOrDefault(m => m.MaSP == MaSP);
                cardItem.SoLuong++;
            }

            // Action này sẽ chuyển hướng về trang chi tiết sp khi khách hàng đặt vào giỏ thành công. Bạn có thể chuyển về chính trang khách hàng vừa đứng bằng lệnh return Redirect(Request.UrlReferrer.ToString()); nếu muốn.
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public RedirectToRouteResult ThemVaoGioCT(String MaSP, int SoLuong)
        {
            if (Session["giohang"] == null) // Nếu giỏ hàng chưa được khởi tạo
            {
                Session["giohang"] = new List<CartItem>();  // Khởi tạo Session["giohang"] là 1 List<CartItem>
            }

            List<CartItem> giohang = Session["giohang"] as List<CartItem>;  // Gán qua biến giohang dễ code

            // Kiểm tra xem sản phẩm khách đang chọn đã có trong giỏ hàng chưa

            if (giohang.FirstOrDefault(m => m.MaSP == MaSP) == null) // ko co sp nay trong gio hang
            {
                SanPham sp = objQLTPEntities.SanPhams.Find(MaSP);  // tim sp theo MaSP

                CartItem newItem = new CartItem()
                {
                    MaSP = MaSP,
                    TenSP = sp.TenSP,
                    SoLuong = SoLuong,
                    HinhDD = sp.HinhDD,
                    DonGia = (float)sp.GiaBan

                };  // Tạo ra 1 CartItem mới

                giohang.Add(newItem);  // Thêm CartItem vào giỏ 
            }
            else
            {
                // Nếu sản phẩm khách chọn đã có trong giỏ hàng thì không thêm vào giỏ nữa mà tăng số lượng lên.
                CartItem cardItem = giohang.FirstOrDefault(m => m.MaSP == MaSP);
                cardItem.SoLuong = cardItem.SoLuong + SoLuong;
            }

            // Action này sẽ chuyển hướng về trang chi tiết sp khi khách hàng đặt vào giỏ thành công. Bạn có thể chuyển về chính trang khách hàng vừa đứng bằng lệnh return Redirect(Request.UrlReferrer.ToString()); nếu muốn.
            return RedirectToAction("Detail", "Product", new { MaSP = MaSP });
        }


        public RedirectToRouteResult SuaSoLuong(String MaSP, int soluongmoi)
        {
            // tìm carditem muon sua
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            CartItem itemSua = giohang.FirstOrDefault(m => m.MaSP == MaSP);
            if (itemSua != null)
            {
                itemSua.SoLuong = soluongmoi;
            }
            return RedirectToAction("Index");

        }

        public RedirectToRouteResult XoaKhoiGio(String MaSP)
        {
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            CartItem itemXoa = giohang.FirstOrDefault(m => m.MaSP == MaSP);
            if (itemXoa != null)
            {
                giohang.Remove(itemXoa);
            }
            return RedirectToAction("Index");
        }

    }
}