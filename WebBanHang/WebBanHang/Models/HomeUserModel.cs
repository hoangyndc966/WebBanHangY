using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Context;
using WebBanHang.Models;
namespace WebBanHang.Models
{
    public class HomeUserModel
    {
        public List<Context.Product> ListProduct { get; set; }
        //public List<Product> ListProduct { get; set; }

        public List<Category> ListCategory { get; set; } 
        public List<Brand> ListBrand { get; set; }

    }
}