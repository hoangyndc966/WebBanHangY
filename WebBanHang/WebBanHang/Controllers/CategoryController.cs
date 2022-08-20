using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Context;

namespace WebBanHang.Controllers
{
    public class CategoryController : Controller
    {
        WebBanHangEntities2 objWebBanHangEntities2 = new WebBanHangEntities2();
        // GET: Category
        public ActionResult Index()
        {
            var lstCategory = objWebBanHangEntities2.Categories.ToList();
            return View(lstCategory);
        }
        public ActionResult ProductCategory(int Id)
        {
            var lstProduct = objWebBanHangEntities2.Products.Where(n => n.CategoryId == Id).ToList();
            return View(lstProduct);
        }
    }
}