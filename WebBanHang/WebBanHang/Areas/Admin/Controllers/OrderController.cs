using PagedList;
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
        public ActionResult Index(string currentFilter, string SearchString, int? page)
        {
            var lstOrder = new List<Order>();
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                // lấy ds sp theo từ khóa tìm kiếm
                lstOrder = objWebBanHangEntities2.Orders.Where(n => n.Name.Contains(SearchString)).ToList();

            }
            else
            {
                //lấy all sp trong bảng product
                lstOrder = objWebBanHangEntities2.Orders.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            // sl item của 1 trang = 4
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            // sap xep theo id sp, sp mới đưa lên đầu
            lstOrder = lstOrder.OrderByDescending(n => n.Id).ToList();
            return View(lstOrder.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Details(int Id)
        {
            var order = objWebBanHangEntities2.Orders.Where(n => n.Id == Id).FirstOrDefault();
            return View(order);
        }
        [HttpGet]
        public ActionResult Delete(int Id)
        {

            var objorder= objWebBanHangEntities2.Orders.Where(n => n.Id == Id).FirstOrDefault();
            return View(objorder);
        }
        [HttpPost]
        public ActionResult Delete(Order objOder)
        {

            var objorder = objWebBanHangEntities2.Orders.Where(n => n.Id == objOder.Id).FirstOrDefault();
            objWebBanHangEntities2.Orders.Remove(objorder);
            objWebBanHangEntities2.SaveChanges();
            return RedirectToAction("Index");
        }
       
    }
}