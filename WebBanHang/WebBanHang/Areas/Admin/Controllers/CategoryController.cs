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
    public class CategoryController : Controller
    {
        WebBanHangEntities2 objWebBanHangEntities2 = new WebBanHangEntities2();
        // GET: Admin/Category
        public ActionResult Index(string currentFilter, string SearchString, int? page)
        {
            var lstCategory = new List<Category>();
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
                lstCategory = objWebBanHangEntities2.Categories.Where(n => n.Name.Contains(SearchString)).ToList();

            }
            else
            {
                //lấy all sp trong bảng product
                lstCategory = objWebBanHangEntities2.Categories.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            // sl item của 1 trang = 4
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            // sap xep theo id sp, sp mới đưa lên đầu
            lstCategory = lstCategory.OrderByDescending(n => n.Id).ToList();
            return View(lstCategory.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Details(int Id)
        {
            var objCategory = objWebBanHangEntities2.Categories.Where(n => n.Id == Id).FirstOrDefault(); //recent remove
            return View(objCategory);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Category objCategory)
        {

            if (ModelState.IsValid)
            {
                try
                {

                    if (objCategory.ImageUpload != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(objCategory.ImageUpload.FileName);
                        //tenhinh
                        string extension = Path.GetExtension(objCategory.ImageUpload.FileName);
                        //png
                        fileName = fileName + extension;
                        // tenhinh.png
                        objCategory.Avartar = fileName;
                        objCategory.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/category"), fileName));
                    }
                    objWebBanHangEntities2.CreateOnUtc = DateTime.Now;
                    objWebBanHangEntities2.Categories.Add(objCategory);
                    objWebBanHangEntities2.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();

                }
            }
            return View(objCategory);
        }

        [HttpGet]
        public ActionResult Delete(int Id)
        {

            var objCategory = objWebBanHangEntities2.Categories.Where(n => n.Id == Id).FirstOrDefault();
            return View(objCategory);
        }

        [HttpPost]
        public ActionResult Delete(Category objCate)
        {

            var objCategory = objWebBanHangEntities2.Categories.Where(n => n.Id == objCate.Id).FirstOrDefault();
            objWebBanHangEntities2.Categories.Remove(objCategory);
            objWebBanHangEntities2.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var lstCategory = objWebBanHangEntities2.Categories.ToList();
            ViewBag.lstCategory = new SelectList(lstCategory, "Id", "Name", 0);
            Category row = objWebBanHangEntities2.Categories.Find(Id);
            return View(row);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(int Id, Category objCategory)
        {
            try
            {

                if (objCategory.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(objCategory.ImageUpload.FileName);
                    //tenhinh
                    string extension = Path.GetExtension(objCategory.ImageUpload.FileName);
                    //png
                    fileName = fileName + extension;
                    // tenhinh.png
                    objCategory.Avartar = fileName;
                    objCategory.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/category"), fileName));
                }
                else
                {
                    //mấy chỗ sửa k dùng find
                    Category item = objWebBanHangEntities2.Categories.AsNoTracking().Where(n => n.Id == objCategory.Id).FirstOrDefault();
                    objCategory.Avartar = item.Avartar;
                }

                objWebBanHangEntities2.CreateOnUtc = DateTime.Now;
                objWebBanHangEntities2.Entry(objCategory).State = EntityState.Modified;
                objWebBanHangEntities2.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                var lstcat = objWebBanHangEntities2.Categories.ToList();
                ViewBag.ListCategory = new SelectList(lstcat, "Id", "Name", 0);
                return View(objCategory);
            }

        }
    } 
}