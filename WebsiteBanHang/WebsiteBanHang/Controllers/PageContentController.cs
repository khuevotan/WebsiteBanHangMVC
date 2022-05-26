using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebsiteBanHang.Controllers
{
    public class PageContentController : Controller
    {
        // GET: PageContent
        public ActionResult Content()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}