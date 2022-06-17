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
    public class PhanHoisController : Controller
    {
        private QLTP3Entities2 db = new QLTP3Entities2();

        // GET: Admin/PhanHois
        public ActionResult Index()
        {
            if (Session["MaNV"] == null)
            {
                return RedirectToAction("LoiPhanQuyen", "Home");

            }
            else
            {
                var phanHois = db.PhanHois.Include(p => p.KhachHang);
                return View(phanHois.ToList());
            }
        }

        // GET: Admin/PhanHois/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhanHoi phanHoi = db.PhanHois.Find(id);
            if (phanHoi == null)
            {
                return HttpNotFound();
            }
            return View(phanHoi);
        }

        // GET: Admin/PhanHois/Create
        public ActionResult Create()
        {
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "TaiKhoan");
            return View();
        }

        // POST: Admin/PhanHois/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaPH,NgayGui,ChuDe,NoiDung,MaKH")] PhanHoi phanHoi)
        {
            if (ModelState.IsValid)
            {
                db.PhanHois.Add(phanHoi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "TaiKhoan", phanHoi.MaKH);
            return View(phanHoi);
        }

        // GET: Admin/PhanHois/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhanHoi phanHoi = db.PhanHois.Find(id);
            if (phanHoi == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "TaiKhoan", phanHoi.MaKH);
            return View(phanHoi);
        }

        // POST: Admin/PhanHois/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaPH,NgayGui,ChuDe,NoiDung,MaKH")] PhanHoi phanHoi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phanHoi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "TaiKhoan", phanHoi.MaKH);
            return View(phanHoi);
        }

        // GET: Admin/PhanHois/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhanHoi phanHoi = db.PhanHois.Find(id);
            if (phanHoi == null)
            {
                return HttpNotFound();
            }
            return View(phanHoi);
        }

        // POST: Admin/PhanHois/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            PhanHoi phanHoi = db.PhanHois.Find(id);
            db.PhanHois.Remove(phanHoi);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        [HttpGet]
        public ActionResult TimKiemNC(string MaPH = "", string NgayGui = "", string ChuDe = "", string NoiDung = "", string MaKH = "")
        {
            if (Session["MaNV"] == null)
            {
                return RedirectToAction("DangNhapNV", "Home");
            }
            else
            {
                ViewBag.MaPH = MaPH;
                ViewBag.NgayGui = NgayGui;
                ViewBag.ChuDe = ChuDe;
                ViewBag.NoiDung = NoiDung;
                ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "TenKH");
                var phanHois = db.PhanHois.SqlQuery("PhanHoi_TimKiemNC'" + MaPH + "','" + NgayGui + "','" + ChuDe + "','" + NoiDung + "','" + MaKH +  "'");
                if (phanHois.Count() == 0)
                    ViewBag.TB = "Không có thông tin tìm kiếm.";
                return View(phanHois.ToList());
            }


        }

    }
}
