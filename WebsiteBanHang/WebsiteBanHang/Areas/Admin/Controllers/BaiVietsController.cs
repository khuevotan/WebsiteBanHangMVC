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
    public class BaiVietsController : Controller
    {
        private QLTPEntities1 db = new QLTPEntities1();

        // GET: Admin/BaiViets
        public ActionResult Index()
        {
            if (Session["MaNV"] == null)
            {
                return RedirectToAction("LoiPhanQuyen", "Home");
            }
            else
            {
                var baiViets = db.BaiViets.Include(b => b.NhanVien);
                return View(baiViets.ToList());
            }    
                
        }

        // GET: Admin/BaiViets/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaiViet baiViet = db.BaiViets.Find(id);
            if (baiViet == null)
            {
                return HttpNotFound();
            }
            return View(baiViet);
        }

        // GET: Admin/BaiViets/Create
        public ActionResult Create()
        {
            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "TaiKhoan");
            return View();
        }

        // POST: Admin/BaiViets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaBV,TenBV,NoiDung,HinhDD,NgayDang,MaNV")] BaiViet baiViet)
        {
            if (ModelState.IsValid)
            {
                db.BaiViets.Add(baiViet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "TaiKhoan", baiViet.MaNV);
            return View(baiViet);
        }

        // GET: Admin/BaiViets/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaiViet baiViet = db.BaiViets.Find(id);
            if (baiViet == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "TaiKhoan", baiViet.MaNV);
            return View(baiViet);
        }

        // POST: Admin/BaiViets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaBV,TenBV,NoiDung,HinhDD,NgayDang,MaNV")] BaiViet baiViet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(baiViet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "TaiKhoan", baiViet.MaNV);
            return View(baiViet);
        }

        // GET: Admin/BaiViets/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaiViet baiViet = db.BaiViets.Find(id);
            if (baiViet == null)
            {
                return HttpNotFound();
            }
            return View(baiViet);
        }

        // POST: Admin/BaiViets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            BaiViet baiViet = db.BaiViets.Find(id);
            db.BaiViets.Remove(baiViet);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

   

       [HttpGet]
        public ActionResult TimKiemNC(string MaBV = "", string MaNV = "", string NgayDang = "", string TenBV = "", string HinhDD = "", string NoiDung = "")
        {
            if (Session["MaNV"] == null)
            {
                return RedirectToAction("DangNhapNV", "Home");
            }
            else
            {
                ViewBag.MaBV = MaBV;
                ViewBag.NgayDang = NgayDang;
                ViewBag.TenBV = TenBV;
                ViewBag.HinhDD = HinhDD;
                ViewBag.NoiDung = NoiDung;
                ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "MaNV");
                var baiViets = db.BaiViets.SqlQuery("BaiViet_TimKiemNC'" + MaBV + "','" + MaNV + "','" + NgayDang + "','" + TenBV + "','" + HinhDD + "','" + NoiDung + "'");
                if (baiViets.Count() == 0)
                    ViewBag.TB = "Không có thông tin tìm kiếm.";
                return View(baiViets.ToList());
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
