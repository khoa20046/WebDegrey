using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnLTW.Models.ViewModel
{
    public class CartItem
    {
        public int ProductID { get; set; }
        public int ProductId { get; internal set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string ProductImage { get; set; }
        // tổng giá cho mỗi sản phẩm 
        public decimal TotalPrice => Quantity * UnitPrice;

       
    }
}