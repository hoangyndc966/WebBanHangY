using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanHang.Models
{
    public class CartModel
    {
        public Context.Product Product { get; set; }
        public int Quantity { get; set; }   

    }
}