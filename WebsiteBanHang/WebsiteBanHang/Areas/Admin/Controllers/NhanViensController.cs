using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.CSDL;
using WebsiteBanHang.Models;

namespace WebsiteBanHang.Areas.Admin.Controllers
{
    public class NhanViensController : Controller
    {
        private QLTP3Entities2 db = new QLTP3Entities2();
        string LayMaNV()
        {
            var maMax = db.NhanViens.ToList().Select(n => n.MaNV).Max();
            int maNV = int.Parse(maMax.Substring(2)) + 1;
            string NV = String.Concat("0", maNV.ToString());
            return "NV" + NV.Substring(maNV.ToString().Length - 1);
        }
        // GET: Admin/NhanViens
        public ActionResult Index()
        {
            var nhanViens = db.NhanViens.Include(n => n.Nhom);
            return View(nhanViens.ToList());
        }

        // GET: Admin/NhanViens/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanViens.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            return View(nhanVien);
        }

        // GET: Admin/NhanViens/Create
        public ActionResult Create()
        {
            ViewBag.MaNV = LayMaNV();
            ViewBag.MaNhom = new SelectList(db.Nhoms, "MaNhom", "TenNhom");
            return View();
        }

        // POST: Admin/NhanViens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNV,TaiKhoan,MatKhau,HoNV,TenNV,SoDT,DiaChi,Email,Luong,NgaySinh,GioiTinh,HinhDD,MaNhom")] NhanVien nhanVien)
        {
            if (ModelState.IsValid)
            {
                db.NhanViens.Add(nhanVien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaNhom = new SelectList(db.Nhoms, "MaNhom", "TenNhom", nhanVien.MaNhom);
            return View(nhanVien);
        }

        // GET: Admin/NhanViens/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanViens.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaNhom = new SelectList(db.Nhoms, "MaNhom", "TenNhom", nhanVien.MaNhom);
            return View(nhanVien);
        }

        // POST: Admin/NhanViens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNV,TaiKhoan,MatKhau,HoNV,TenNV,SoDT,DiaChi,Email,Luong,NgaySinh,GioiTinh,HinhDD,MaNhom")] NhanVien nhanVien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nhanVien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaNhom = new SelectList(db.Nhoms, "MaNhom", "TenNhom", nhanVien.MaNhom);
            return View(nhanVien);
        }

        // GET: Admin/NhanViens/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanViens.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            return View(nhanVien);
        }

        // POST: Admin/NhanViens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            NhanVien nhanVien = db.NhanViens.Find(id);
            db.NhanViens.Remove(nhanVien);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult TimKiemNC(string MaNV = "", string TaiKhoan = "", string HoNV = "", string TenNV = "", string SoDT = "", string DiaChi = "", string Luong = "", string NgaySinh = "", string GioiTinh = "", string MaNhom = "" )
        {
            if (Session["MaNV"] == null)
            {
                return RedirectToAction("DangNhapNV", "Home");
            }
            else
            {
                ViewBag.MaNV = MaNV;
                ViewBag.TaiKhoan = TaiKhoan;
                ViewBag.HoNV = HoNV;
                ViewBag.TenNV = TenNV;
                ViewBag.SoDT = SoDT;
                ViewBag.Luong = Luong;
                ViewBag.DiaChi = DiaChi;
                ViewBag.NgaySinh = NgaySinh;
                ViewBag.GioiTinh = GioiTinh;
                ViewBag.MaNhom = new SelectList(db.Nhoms, "MaNhom", "MaNhom");
                var nhanViens = db.NhanViens.SqlQuery("NhanVien_TimKiemNC'" + MaNV + "','" + TaiKhoan + "','" + HoNV + "','" + TenNV + "','" + SoDT + "','" + DiaChi + "','" + Luong + "','" + NgaySinh + "','" + GioiTinh + "','" + MaNhom + "'");
                if (nhanViens.Count() == 0)
                    ViewBag.TB = "Không có thông tin tìm kiếm.";
                return View(nhanViens.ToList());
            }
        }



        public ActionResult TrangCaNhan()
        {
            if (Session["MaNV"] == null)
            {
                return RedirectToAction("DangNhapNV", "Home");
            }
            else
            {

                string manv = Session["MaNV"].ToString();

                HomeModel objHomeModel2 = new HomeModel();
                objHomeModel2.ListNhanVien = db.NhanViens.Where(n => n.MaNV == manv).ToList();

                return View(objHomeModel2);

            }
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
