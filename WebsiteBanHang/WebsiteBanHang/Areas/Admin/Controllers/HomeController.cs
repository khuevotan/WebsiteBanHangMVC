using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Areas.Admin.Data;
using WebsiteBanHang.CSDL;

namespace WebsiteBanHang.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        QLTPEntities1 objQLTPEntities = new QLTPEntities1();

        // GET: Admin/Home
        public ActionResult Index()
        {
            var model = new ThongKeDG()
            {
                DonHang = ThongKeDonHang(),
                SanPham = ThongKeSanPham(),
                KhachHang = ThongKeKhachHang(),
                NhanVien = ThongKeNhanVien(),
            };
            return View(model);
        }

        public int ThongKeDonHang()
        {
            int ddh = objQLTPEntities.HoaDons.Count();
            return ddh;
        }

        public int ThongKeSanPham()
        {
            int ddh = objQLTPEntities.SanPhams.Count();
            return ddh;
        }

        public int ThongKeKhachHang()
        {
            int ddh = objQLTPEntities.KhachHangs.Count();
            return ddh;
        }

        public int ThongKeNhanVien()
        {
            int ddh = objQLTPEntities.NhanViens.Count();
            return ddh;
        }



        // GET: NhanViens
        string LayMaNV()
        {
            var maMax = objQLTPEntities.NhanViens.ToList().Select(n => n.MaNV).Max();
            int maNV = int.Parse(maMax.Substring(2)) + 1;
            string NV = String.Concat("00", maNV.ToString());
            return "NV" + NV.Substring(maNV.ToString().Length - 1);
        }

        public ActionResult DangKyNV()
        {
            ViewBag.MaKH = LayMaNV();
            ViewBag.MaNhom = new SelectList(objQLTPEntities.Nhoms, "MaNhom", "TenNhom");
            return View();
        }
        // POST: NhanViens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangKyNV([Bind(Include = "MaNV,TaiKhoan,MatKhau,HoNV,TenNV,SoDT,DiaChi,Email,Luong,NgaySinh,GioiTinh,HinhDD,MaNhom")] NhanVien nhanVien)
        {
            //System.Web.HttpPostedFileBase Avatar;
            var imgNV = Request.Files["Avatar"];
            //Lấy thông tin từ input type=file có tên Avatar
            string postedFileName = System.IO.Path.GetFileName(imgNV.FileName);
            //Lưu hình đại diện về Server
            var path = Server.MapPath("/Content/images/avatars/nv/" + postedFileName);
            imgNV.SaveAs(path);

            if (ModelState.IsValid)
            {
                nhanVien.MaNV = LayMaNV();
                nhanVien.HinhDD = postedFileName;
                objQLTPEntities.NhanViens.Add(nhanVien);
                objQLTPEntities.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nhanVien);
        }


        [HttpGet]
        public ActionResult DangNhapNV()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangNhapNV(string taiKhoan, string matKhau)
        {
            if (ModelState.IsValid)
            {
                var data = objQLTPEntities.NhanViens.Where(s => s.TaiKhoan.Equals(taiKhoan) && s.MatKhau.Equals(matKhau)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["FullName"] = data.FirstOrDefault().HoNV + " " + data.FirstOrDefault().TenNV;
                    Session["MaNV"] = data.FirstOrDefault().MaNV;
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        //Logout
        public ActionResult DangXuat()
        {
            Session.Clear();//remove session
            return RedirectToAction("DangNhapNV");
        }


        public ActionResult LienHe()
        {
            return View();
        }

    }
}