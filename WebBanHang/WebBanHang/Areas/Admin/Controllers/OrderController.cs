using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Context;

namespace WebBanHang.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        WebBanHangEntities2 objWebBanHangEntities2 = new WebBanHangEntities2();

        // GET: Admin/Order
        public ActionResult Index()
        {
            return View(objWebBanHangEntities2.Orders.ToList());
        }
    }
}