using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBanHang.Context;

namespace WebBanHang.Models
{
    public class HomeUserModel
    {
        public List<Context.Product> ListProduct { get; set; }
        public List<Category> ListCategory { get; set; } 

    }
}