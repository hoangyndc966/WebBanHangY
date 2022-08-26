using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Context;


namespace WebBanHang.Areas.Admin.Controllers
{
    public class BrandController : Controller
    {
        WebBanHangEntities2 objWebBanHangEntities2 = new WebBanHangEntities2();
        // GET: Admin/Brand
        public ActionResult Index(string currentFilter, string SearchString, int? page)
        {
            var lstBrand = new List<Brand>();
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
                lstBrand = objWebBanHangEntities2.Brands.Where(n => n.Name.Contains(SearchString)).ToList();

            }
            else
            {
                //lấy all sp trong bảng product
                lstBrand = objWebBanHangEntities2.Brands.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            // sl item của 1 trang = 4
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            // sap xep theo id sp, sp mới đưa lên đầu
            lstBrand = lstBrand.OrderByDescending(n => n.Id).ToList();
            return View(lstBrand.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Details(int Id)
        {
            var objbrand = objWebBanHangEntities2.Brands.Where(n => n.Id == Id).FirstOrDefault(); 
            return View(objbrand);
        }

        [HttpGet]
        public ActionResult Create()
        {
           
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Brand objBrand)
        {
            
            if (ModelState.IsValid)
            {
                try
                {

                    if (objBrand.ImageUpload != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(objBrand.ImageUpload.FileName);
                        //tenhinh
                        string extension = Path.GetExtension(objBrand.ImageUpload.FileName);
                        //png
                        fileName = fileName + extension;
                        // tenhinh.png
                        objBrand.Avatar = fileName;
                        objBrand.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/allProduct"), fileName));
                    }
                    objWebBanHangEntities2.CreateOnUtc = DateTime.Now;
                    objWebBanHangEntities2.Brands.Add(objBrand);
                    objWebBanHangEntities2.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();

                }
            }
            return View(objBrand);
        }
        [HttpGet]
        public ActionResult Delete(int Id)
        {

            var objBrand = objWebBanHangEntities2.Brands.Where(n => n.Id == Id).FirstOrDefault();
            return View(objBrand);
        }
        [HttpPost]
        public ActionResult Delete(Brand objBr)
        {

            var objBrand = objWebBanHangEntities2.Brands.Where(n => n.Id == objBr.Id).FirstOrDefault();
            objWebBanHangEntities2.Brands.Remove(objBrand);
            objWebBanHangEntities2.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int Id)
        {


            var lstBrand = objWebBanHangEntities2.Brands.ToList();
            ViewBag.ListBrand = new SelectList(lstBrand, "Id", "Name", 0);

            Brand row = objWebBanHangEntities2.Brands.Find(Id);
            return View(row);

        }
        [ValidateInput(false)]
            [HttpPost]
            public ActionResult Edit(int Id, Brand objBrand)
            {
            try
            {

                if (objBrand.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(objBrand.ImageUpload.FileName);
                    //tenhinh
                    string extension = Path.GetExtension(objBrand.ImageUpload.FileName);
                    //png
                    fileName = fileName + extension;
                    // tenhinh.png
                    objBrand.Avatar = fileName;
                    objBrand.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/allProduct"), fileName));
                }
                else
                {
                    //mấy chỗ sửa k dùng find
                    Brand item = objWebBanHangEntities2.Brands.AsNoTracking().Where(n => n.Id == objBrand.Id).FirstOrDefault();
                    objBrand.Avatar = item.Avatar;
                }
               
                objWebBanHangEntities2.CreateOnUtc = DateTime.Now;
                objWebBanHangEntities2.Entry(objBrand).State = EntityState.Modified;
                objWebBanHangEntities2.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                var lstcat = objWebBanHangEntities2.Categories.ToList();
                ViewBag.ListCategory = new SelectList(lstcat, "Id", "Name", 0);
                return View(objBrand);
            }

        }
        }
    }