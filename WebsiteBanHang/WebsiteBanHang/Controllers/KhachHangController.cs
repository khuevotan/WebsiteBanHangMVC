using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.CSDL;
using WebsiteBanHang.Models;

namespace WebsiteBanHang.Controllers
{
    public class KhachHangController : Controller
    {
        private QLTPEntities1 db = new QLTPEntities1();

        // GET: KhachHang
        public ActionResult TrangCaNhan()
        {
            
            string MaKH = Session["MaKH"].ToString();

            HomeModel objHomeModel2 = new HomeModel();
            objHomeModel2.ListKhachHang = db.KhachHangs.Where(n => n.MaKH == MaKH).ToList();

            return View(objHomeModel2);            
        }

        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHangs.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }

        // POST: Admin/KhachHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaKH,TaiKhoan,MatKhau,HoKH,TenKH,NgaySinh,SoDT,DiaChi,Email,GioiTinh,HinhDD")] KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(khachHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(khachHang);
        }



        public ActionResult LichSuMuaHang()
        {

            string MaKH = Session["MaKH"].ToString();

            HomeModel objHomeModel2 = new HomeModel();
            objHomeModel2.ListKhachHang = db.KhachHangs.Where(n => n.MaKH == MaKH).ToList();

            return View(objHomeModel2);
        }


        public ActionResult HoaDon()
        {
            string MaKH = Session["MaKH"].ToString();

            HomeModel objHomeModel2 = new HomeModel();
            objHomeModel2.ListHoaDon = db.HoaDons.Where(n => n.MaKH == MaKH).ToList();
            objHomeModel2.ListKhachHang = db.KhachHangs.Where(n => n.MaKH == MaKH).ToList();
            return View(objHomeModel2);
        }

        public ActionResult HoaDonChiTiet()
        {
            string MaKH = Session["MaKH"].ToString();

            HomeModel objHomeModel2 = new HomeModel();
            objHomeModel2.ListHoaDon = db.HoaDons.Where(n => n.MaKH == MaKH).ToList();
            return View(objHomeModel2);
        }

    }
}