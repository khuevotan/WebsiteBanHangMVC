using System;
using System.Web.Mvc;
using WebsiteBanHang.CSDL;


using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace WebsiteBanHang.Areas.Admin.Controllers
{
    public class MainController : Controller
    {
        private QLTP3Entities2 objQLTPEntities = new QLTP3Entities2();
        // GET: Admin/Main

        public ActionResult Index()
        {
            // Thiết lập Ngày bắt đầu (sẽ luôn là ngày 1 của tháng hiện tại)
            DateTime fromDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            // Thiết lập Ngày kết thúc (là ngày hiện tại)
            DateTime toDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            // Tính tổng doanh thu trong tháng với các đơn hàng có trạng thái khác Hủy
            ViewBag.DoanhThuThang = objQLTPEntities.HoaDons.Where(x => x.NgayDat >= fromDay && x.NgayDat <= toDay && x.MaTT != "TT5").Sum(x => (float?)x.ThanhToan) ?? 0;
            // Tính tổng doanh thu trong ngày với các đơn hàng có trạng thái khác Hủy
            ViewBag.DoanhThuNgay = objQLTPEntities.HoaDons.Where(x => x.NgayDat.Equals(DateTime.Today) && x.MaTT != "TT5").Sum(x => (float?)x.ThanhToan) ?? 0;
            // Số lượng đơn hàng trong ngày
            ViewBag.HoaDonNgay = objQLTPEntities.HoaDons.Where(x => x.NgayDat.Equals(DateTime.Today)).Count();
            // Số lượng sản phẩm còn tồn
            ViewBag.TongSoDongHo = objQLTPEntities.SanPhams.Where(x => x.SoLuong > 0).Count();
            return View();
        }


        public ActionResult ChangePassword(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = objQLTPEntities.NhanViens.Find(id);
            if (nhanVien == null)
            {
                return View("AdminError");
            }
            return View(nhanVien);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(FormCollection collection, string id)
        {
            NhanVien nv = objQLTPEntities.NhanViens.SingleOrDefault(x => x.TaiKhoan.Equals(id));

            if (string.IsNullOrWhiteSpace(collection["MatKhauCu"]))
            {
                ViewData["TrongMKCu"] = "Nhập mật khẩu cũ!";
            }
            else if (string.IsNullOrWhiteSpace(collection["MatKhauMoi"]))
            {
                ViewData["TrongMKMoi"] = "Nhập mật khẩu mới!";
            }
            else if (string.IsNullOrWhiteSpace(collection["MatKhauNhapLai"]))
            {
                ViewData["TrongMKNhapLai"] = "Nhập lại mật khẩu mới!";
            }
            else if (!collection["MatKhauCu"].Equals(nv.MatKhau))
            {
                ViewData["SaiMKCu"] = "Sai mật khẩu cũ!";
            }
            else if (collection["MatKhauMoi"].Equals(collection["MatKhauCu"]))
            {
                ViewData["TrungMKCu"] = "Trùng mật khẩu cũ!";
            }
            else if (!collection["MatKhauNhapLai"].Equals(collection["MatKhauMoi"]))
            {
                ViewData["KhacMKMoi"] = "Sai mật khẩu mới!";
            }
            else
            {
                if (ModelState.IsValid)
                {
                    nv.MatKhau = collection["MatKhauMoi"];
                    Session["NhanVien"] = nv;
                    objQLTPEntities.SaveChanges();
                    return RedirectToAction("GetProfile", new { id = nv.TaiKhoan });
                }
            }
            return View(nv);
        }

    }
}