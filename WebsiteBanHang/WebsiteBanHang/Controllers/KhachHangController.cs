using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.CSDL;
using WebsiteBanHang.Models;
using static WebsiteBanHang.Models.HomeModel;

namespace WebsiteBanHang.Controllers
{
    public class KhachHangController : Controller
    {
        private QLTPEntities1 db = new QLTPEntities1();

        // GET: KhachHang
        public ActionResult TrangCaNhan()
        {
            if (Session["MaKH"] == null)
            {
                return RedirectToAction("DangNhapKH", "Home");
            }
            else
            {

                string MaKH = Session["MaKH"].ToString();

                HomeModel objHomeModel2 = new HomeModel();
                objHomeModel2.ListKhachHang = db.KhachHangs.Where(n => n.MaKH == MaKH).ToList();

                return View(objHomeModel2);

            }    
                      
        }

        public ActionResult SetPassword(string MaKH)
        {
            if (MaKH == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHangs.Find(MaKH);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetPassword([Bind(Include = "MatKhau")] KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(khachHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("TrangCaNhan", "KhachHang");
            }
            return View(khachHang);

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

      

        // POST: Tests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaKH,TaiKhoan,HoKH,TenKH,NgaySinh,SoDT,DiaChi,Email,GioiTinh,HinhDD")] KhachHang khachHang)
        {
            var imgKH = Request.Files["Avatar"];
            try
            {
                string postedFileName = System.IO.Path.GetFileName(imgKH.FileName);
                //Lưu hình đại diện về Server
                var path = Server.MapPath("/Content/images/avatars/kh/" + postedFileName);
                imgKH.SaveAs(path);
            }
            catch
            { }


            if (ModelState.IsValid)
            {
                db.Entry(khachHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("TrangCaNhan","KhachHang");
            }

            return View(khachHang);

        }

     

        public ActionResult LichSuMuaHang(String maKH)
        {
            if(Session["MaKH"] == null)
            {
                return RedirectToAction("DangNhapKH", "Home");
            }
            else
            {
                string MaKH = Session["MaKH"].ToString();

                HomeModel objHomeModel2 = new HomeModel();
                objHomeModel2.ListHoaDon = db.HoaDons.Where(n => n.MaKH == MaKH && n.MaTT == "TT4").ToList();
                objHomeModel2.ListKhachHang = db.KhachHangs.Where(n => n.MaKH == MaKH).ToList();

                return View(objHomeModel2);
            }    
         
        }


        public ActionResult HoaDon()
        {

            if (Session["MaKH"] == null)
            {
                return RedirectToAction("DangNhapKH", "Home");
            }
            else
            {
                string MaKH = Session["MaKH"].ToString();

                HomeModel objHomeModel2 = new HomeModel();
                objHomeModel2.ListHoaDon = db.HoaDons.Where(n => n.MaKH == MaKH ).ToList();
           
                objHomeModel2.ListKhachHang = db.KhachHangs.Where(n => n.MaKH == MaKH).ToList();
                objHomeModel2.ListTrangThai = db.TrangThais.ToList();
                return View(objHomeModel2);
            }

          
        }

        public ActionResult HoaDonChiTiet(String MaHD)
        {
            if (Session["MaKH"] == null)
            {
                return RedirectToAction("DangNhapKH", "Home");
            }
            else
            {
                string MaKH = Session["MaKH"].ToString();

                HomeModel objHomeModel2 = new HomeModel();
                objHomeModel2.ListCTHoaDon = db.CTHoaDons.Where(n => n.MaHD == MaHD).ToList();
                objHomeModel2.ListHoaDon = db.HoaDons.Where(n => n.MaHD == MaHD).ToList();
                objHomeModel2.ListKhachHang = db.KhachHangs.Where(n => n.MaKH == MaKH).ToList();
                objHomeModel2.ListTrangThai = db.TrangThais.ToList();
                objHomeModel2.ListSanPham = db.SanPhams.ToList();
                return View(objHomeModel2);
            }
        }
    }
}