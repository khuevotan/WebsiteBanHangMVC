using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.CSDL;
using WebsiteBanHang.Models;

namespace WebsiteBanHang.Controllers
{
    public class ProductController : Controller
    {

        QLTP3Entities2 objQLTPEntities = new QLTP3Entities2();

        // Xem thoong tin cu the cua mot doi tuong
        // GET: Product
        public ActionResult Detail(String MaSP)
        {
            HomeModel objHomeModel = new HomeModel();
            objHomeModel.ListNhaCungCap = objQLTPEntities.NhaCungCaps.ToList();
            objHomeModel.ListDanhMuc = objQLTPEntities.DanhMucs.ToList();
            objHomeModel.ListBaiViet = objQLTPEntities.BaiViets.ToList();
            objHomeModel.ListSanPham = objQLTPEntities.SanPhams.Where(n => n.MaSP == MaSP).ToList();
            return View(objHomeModel);

        }


      
    }
}