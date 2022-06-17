using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.CSDL;
using WebsiteBanHang.Models;


namespace WebsiteBanHang.Controllers
{
    public class DanhMucController : Controller
    {
        QLTP3Entities2 objQLTPEntities = new QLTP3Entities2();
        // GET: DanhMuc
        public ActionResult DanhMuc()
        {
            HomeModel objHomeModel = new HomeModel();

            objHomeModel.ListSanPham = objQLTPEntities.SanPhams.ToList();
            objHomeModel.ListNhaCungCap = objQLTPEntities.NhaCungCaps.ToList();
            objHomeModel.ListDanhMuc = objQLTPEntities.DanhMucs.ToList();
            objHomeModel.ListBaiViet = objQLTPEntities.BaiViets.ToList();

            var lstDanhMuc = objQLTPEntities.DanhMucs.ToList();
            return View(objHomeModel);
        }

        public ActionResult SanPhamDanhMuc(String MaDM)
        {
            HomeModel objHomeModel2 = new HomeModel();

            objHomeModel2.ListNhaCungCap = objQLTPEntities.NhaCungCaps.ToList();
            objHomeModel2.ListDanhMuc = objQLTPEntities.DanhMucs.Where(n => n.MaDM == MaDM).ToList();
            objHomeModel2.ListSanPham = objQLTPEntities.SanPhams.Where(n => n.MaDM == MaDM).ToList();
            return View(objHomeModel2);
        }

        public ActionResult SanPhamDanhMuc2(String MaDM)
        {
            HomeModel objHomeModel3 = new HomeModel();

            objHomeModel3.ListNhaCungCap = objQLTPEntities.NhaCungCaps.ToList();
            objHomeModel3.ListDanhMuc = objQLTPEntities.DanhMucs.Where(n => n.MaDM == MaDM).ToList();
            objHomeModel3.ListSanPham = objQLTPEntities.SanPhams.Where(n => n.MaDM == MaDM).ToList();

            return View(objHomeModel3);
        }


        public ActionResult TimKiemTenSP(String TenSP)
        {
            HomeModel objHomeMode4 = new HomeModel();
            objHomeMode4.ListNhaCungCap = objQLTPEntities.NhaCungCaps.ToList();
            objHomeMode4.ListDanhMuc = objQLTPEntities.DanhMucs.ToList();
            objHomeMode4.ListBaiViet = objQLTPEntities.BaiViets.ToList();
            objHomeMode4.ListSanPham = objQLTPEntities.SanPhams.Where(n => n.TenSP == TenSP).ToList();
            return View(objHomeMode4);

        }
    }
}