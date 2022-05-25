using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.CSDL;
using WebsiteBanHang.Models;

namespace WebsiteBanHang.Controllers
{
    
    public class HomeController : Controller
    {
       
        QLTPEntities1 objQLTPEntities = new QLTPEntities1();
        public ActionResult Index()
        {
            HomeModel objHomeModel = new HomeModel();

            objHomeModel.ListSanPham = objQLTPEntities.SanPhams.ToList();
            objHomeModel.ListNhaCungCap = objQLTPEntities.NhaCungCaps.ToList();
            objHomeModel.ListDanhMuc = objQLTPEntities.DanhMucs.ToList();
            objHomeModel.ListBaiViet = objQLTPEntities.BaiViets.ToList();

            return View(objHomeModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        // GET: KhachHangs
        string LayMaKH()
        {
            var maMax = objQLTPEntities.KhachHangs.ToList().Select(n => n.MaKH).Max();
            int maKH = int.Parse(maMax.Substring(2)) + 1;
            string KH = String.Concat("00", maKH.ToString());
            return "KH" + KH.Substring(maKH.ToString().Length - 1);
        }

        public ActionResult DangKyKH()
        {
            ViewBag.MaKH = LayMaKH();
            return View();
        }
        // POST: NhanViens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken] 
        public ActionResult DangKyKH([Bind(Include = "MaKH,TaiKhoan,MatKhau,HoKH,TenKH,NgaySinh,SoDT,DiaChi,Email,GioiTinh,HinhDD")] KhachHang khachHang)
        {
            //System.Web.HttpPostedFileBase Avatar;
            var imgKH = Request.Files["Avatar"];
            //Lấy thông tin từ input type=file có tên Avatar
            string postedFileName = System.IO.Path.GetFileName(imgKH.FileName);
            //Lưu hình đại diện về Server
            var path = Server.MapPath("/Content/images/avatars/kh/" + postedFileName);
            imgKH.SaveAs(path);

            if (ModelState.IsValid)
            {
                khachHang.MaKH = LayMaKH();
                khachHang.HinhDD = postedFileName;
                objQLTPEntities.KhachHangs.Add(khachHang);
                objQLTPEntities.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(khachHang);
        }


        [HttpGet]
        public ActionResult DangNhapKH()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangNhapKH(string taiKhoan, string matKhau)
        {
            if (ModelState.IsValid)
            {
                var data = objQLTPEntities.KhachHangs.Where(s => s.TaiKhoan.Equals(taiKhoan) && s.MatKhau.Equals(matKhau)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["FullName"] = data.FirstOrDefault().HoKH + " " + data.FirstOrDefault().TenKH;
                    Session["Email"] = data.FirstOrDefault().Email;
                    Session["MaKH"] = data.FirstOrDefault().MaKH;
                    Session["SoDT"] = data.FirstOrDefault().SoDT;
                    Session["DiaChi"] = data.FirstOrDefault().DiaChi;
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
            return RedirectToAction("DangNhapKH");
        }
    }
}