using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Context;

namespace WebBanHang.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        WebBanHangEntities2 objWebBanHangEntities2 = new WebBanHangEntities2();
        // GET: Admin/User
        public ActionResult Index(string currentFilter, string SearchString, int? page)
        {
            var lstUser = new List<User>();
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
                lstUser = objWebBanHangEntities2.Users.Where(n => n.Email.Contains(SearchString)).ToList();

            }
            else
            {
                //lấy all sp trong bảng product
                lstUser = objWebBanHangEntities2.Users.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            // sl item của 1 trang = 4
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            // sap xep theo id sp, sp mới đưa lên đầu
            lstUser = lstUser.OrderByDescending(n => n.Id).ToList();
            return View(lstUser.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Details(int Id)
        {
            var objUser = objWebBanHangEntities2.Users.Where(n => n.Id == Id).FirstOrDefault();
            return View(objUser);
        }
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(User objUser)
        {
            if (ModelState.IsValid)
            {
                objWebBanHangEntities2.CreateOnUtc = DateTime.Now;
                objWebBanHangEntities2.Users.Add(objUser);
                objWebBanHangEntities2.SaveChanges();

                return RedirectToAction("Index");
            }
                return View(objUser);
        }
        [HttpGet]
        public ActionResult Delete(int Id)
        {

            var objUser = objWebBanHangEntities2.Users.Where(n => n.Id == Id).FirstOrDefault();
            return View(objUser);
        }
        [HttpPost]
        public ActionResult Delete(Brand objU)
        {

            var objUser = objWebBanHangEntities2.Users.Where(n => n.Id == objU.Id).FirstOrDefault();
            objWebBanHangEntities2.Users.Remove(objUser);
            objWebBanHangEntities2.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            //var objUser = objWebBanHangEntities2.Brands.Where(n => n.Id == Id).FirstOrDefault();
            //return View(objUser);

            var objUser = objWebBanHangEntities2.Users.ToList();
            ViewBag.ListUser = new SelectList(objUser, "Id", "Name", 0);

            User row = objWebBanHangEntities2.Users.Find(Id);
            return View(row);
        }
        [HttpPost]
        public ActionResult Edit(User objUser)
        {

            if (ModelState.IsValid)
            {


                objWebBanHangEntities2.Entry(objUser).State = EntityState.Modified;

                objWebBanHangEntities2.SaveChanges();

                    return RedirectToAction("Index");
                
               
            }
            return View(objUser);

        }
    }
}