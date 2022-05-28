using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.CSDL;
using WebsiteBanHang.Models;
using PagedList;

namespace WebsiteBanHang.Controllers
{
    public class BaiVietController : Controller
    {
        QLTPEntities1 objQLTPEntities = new QLTPEntities1();
        QLTPEntities1 db = new QLTPEntities1();

        // GET: BaiViet
        public ActionResult Index(int? page)
        {
            var lstSP = db.BaiViets;
         
            int PageSize = 9;
            int PageNumber = (page ?? 1);

            return View(lstSP.OrderBy(n => n.MaBV).ToPagedList(PageNumber, PageSize));

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