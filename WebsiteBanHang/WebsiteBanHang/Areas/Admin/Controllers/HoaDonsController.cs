using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.CSDL;

namespace WebsiteBanHang.Areas.Admin.Controllers
{
    public class HoaDonsController : Controller
    {
        private QLTPEntities1 db = new QLTPEntities1();

        // GET: Admin/HoaDons
        public ActionResult Index()
        {
            var hoaDons = db.HoaDons.Include(h => h.KhachHang).Include(h => h.NhanVien).Include(h => h.TrangThai);
            return View(hoaDons.ToList());
        }

        // GET: Admin/HoaDons/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDon hoaDon = db.HoaDons.Find(id);
            if (hoaDon == null)
            {
                return HttpNotFound();
            }
            return View(hoaDon);
        }

        // GET: Admin/HoaDons/Create
        public ActionResult Create()
        {
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "TaiKhoan");
            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "TaiKhoan");
            ViewBag.MaTT = new SelectList(db.TrangThais, "MaTT", "TenTT");
            return View();
        }

        // POST: Admin/HoaDons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaHD,NgayDat,NgayGiao,SoDT,DiaChi,GhiChu,MaTT,MaNV,MaKH")] HoaDon hoaDon)
        {
            if (ModelState.IsValid)
            {
                db.HoaDons.Add(hoaDon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "TaiKhoan", hoaDon.MaKH);
            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "TaiKhoan", hoaDon.MaNV);
            ViewBag.MaTT = new SelectList(db.TrangThais, "MaTT", "TenTT", hoaDon.MaTT);
            return View(hoaDon);
        }

        // GET: Admin/HoaDons/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDon hoaDon = db.HoaDons.Find(id);
            if (hoaDon == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "TaiKhoan", hoaDon.MaKH);
            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "TaiKhoan", hoaDon.MaNV);
            ViewBag.MaTT = new SelectList(db.TrangThais, "MaTT", "TenTT", hoaDon.MaTT);
            return View(hoaDon);
        }

        // POST: Admin/HoaDons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaHD,NgayDat,NgayGiao,SoDT,DiaChi,GhiChu,MaTT,MaNV,MaKH")] HoaDon hoaDon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hoaDon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "TaiKhoan", hoaDon.MaKH);
            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "TaiKhoan", hoaDon.MaNV);
            ViewBag.MaTT = new SelectList(db.TrangThais, "MaTT", "TenTT", hoaDon.MaTT);
            return View(hoaDon);
        }

        // GET: Admin/HoaDons/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDon hoaDon = db.HoaDons.Find(id);
            if (hoaDon == null)
            {
                return HttpNotFound();
            }
            return View(hoaDon);
        }

        // POST: Admin/HoaDons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            HoaDon hoaDon = db.HoaDons.Find(id);
            db.HoaDons.Remove(hoaDon);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult TimKiemNC(string MaHD = "", string NgayDat = "", string NgayGiao = "", string DiaChi = "", string MaTT = "", string MaNV = "")
        {
            if (Session["MaNV"] == null)
            {
                return RedirectToAction("DangNhapNV", "Home");
            }
            else
            {
                ViewBag.MaHD = MaHD;
                ViewBag.NgayDat = NgayDat;
                ViewBag.NgayGiao = NgayGiao;
                ViewBag.DiaChi = DiaChi;
                ViewBag.MaTT = new SelectList(db.TrangThais, "MaTT", "TenTT");
                ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "MaNV");
                var hoaDons = db.HoaDons.SqlQuery("HoaDon_TimKiemNC'" + MaHD + "','" + NgayDat + "','" + NgayGiao + "','" + DiaChi + "','" + MaTT + "','" + MaNV + "'");
                if (hoaDons.Count() == 0)
                    ViewBag.TB = "Không có thông tin tìm kiếm.";
                return View(hoaDons.ToList());
            }    

            
        }


        [HttpGet]
        public ActionResult XuatTest()
        {
            return View();


        }


        public ActionResult ChuaDuyet()
        {
            var lstDSDHCD = db.HoaDons.Where(n => n.MaTT == "TT1");
            return View(lstDSDHCD);
        }

        public ActionResult ChuaGiao()
        {
            var lstDSDHCG = db.HoaDons.Where(n => n.MaTT == "TT3");
            return View(lstDSDHCG);
        }

        public ActionResult DaGiaoDaThanhToan()
        {
            var lstDSDHDHT = db.HoaDons.Where(n => n.MaTT == "TT4");
            return View(lstDSDHDHT);
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
