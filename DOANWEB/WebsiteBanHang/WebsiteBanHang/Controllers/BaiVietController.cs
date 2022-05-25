using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.CSDL;
using WebsiteBanHang.Models;

namespace WebsiteBanHang.Controllers
{
    public class BaiVietController : Controller
    {
        QLTPEntities1 objQLTPEntities = new QLTPEntities1();

        // GET: BaiViet
        public ActionResult Index()
        {
            HomeModel objHomeModel = new HomeModel();

            objHomeModel.ListBaiViet = objQLTPEntities.BaiViets.ToList();

            return View(objHomeModel);

        }

        public ActionResult ChiTietBaiViet(String MaBV)
        {
            HomeModel objHomeModel2 = new HomeModel();       
            objHomeModel2.ListBaiViet = objQLTPEntities.BaiViets.Where(n => n.MaBV == MaBV).ToList();
            objHomeModel2.ListFullBaiViet = objQLTPEntities.BaiViets.ToList();
            objHomeModel2.ListNhaCungCap = objQLTPEntities.NhaCungCaps.ToList();
            objHomeModel2.ListDanhMuc = objQLTPEntities.DanhMucs.ToList();
           
            return View(objHomeModel2);
        }

    }
}