using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.CSDL;

namespace WebsiteBanHang.Areas.Admin.Controllers
{
    public class ThongKeController : Controller
    {
        private QLTPEntities1 db = new QLTPEntities1();

        // GET: Admin/ThongKe
        public ActionResult Index()
        {
            
                return View();
      
            
        }

       
    



    }
}