using DoAnLTW.Models;
using DoAnLTW.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DoAnLTW.Controllers
{
    public class OrderController : Controller
    {
        private MyStoreEntities db = new MyStoreEntities();
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Checkout() {
            var cart = Session["Cart"] as List<CartItem>;
            if (cart == null || !cart.Any())
            {
                return RedirectToAction("index", "Name");
            }

            var user = db.Users.SingleOrDefault(u => u.Username == User.Identity.Name);
            if (user == null) { return RedirectToAction("Login", "Account"); }
            
            var customer = db.Customers.SingleOrDefault(c => c.Username == user.Username);
            if (customer == null) { return RedirectToAction("Login", "Account"); }

            var model = new CheckOutVM
            {
                CartItems = cart,
                TotalAmount = cart.Sum(item => item.TotalPrice),
                OrderDate = DateTime.Now,
                ShippingAddress = customer.CustomerAddress,
                CustomerID = customer.CustomerID,   
                Username= customer.Username,
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Checkout(CheckOutVM model)
        {
            if (ModelState.IsValid)
            {
                var cart = Session["Cart"] as List<CartItem>;
                if (cart == null || !cart.Any()) {return RedirectToAction("Index", "Home");}

                var user = db.Users.SingleOrDefault( u => u.Username == User.Identity.Name);
                if (user == null) { return RedirectToAction("Login", "Acount"); }

                var customer = db.Customers.SingleOrDefault(c => c.Username == user.Username);
                if (customer == null) { return RedirectToAction("Login", "Acount"); }

                if (model.PaymentMethod == "Paypal")
                {
                    return RedirectToAction("PaymentWithPaypal", "Paypal", model);
                }
                string paymentStaus = "Chưa thanh toán";
                switch (model.PaymentMethod)
                {
                    case "Tiền mặt": paymentStaus = "Thanh toán tiền mặt"; break;
                    case "Paypal": paymentStaus = "Thanh toán Paypal"; break;
                    case "Mua trước trả sau": paymentStaus = "Trả góp"; break;
                    default: paymentStaus = "Chưa thanh toán"; break;
                }
                var order = new Order
                {
                    CustomerID = customer.CustomerID,
                    OrderDate = model.OrderDate,
                    TotalAmount = model.TotalAmount,
                    PaymentMethod = model.PaymentMethod,
                    
                    ShippingAddress = model.ShippingAddress,
                    OrderDetails = cart.Select(item => new OrderDetail
                    {
                        ProductID = item.ProductID,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice,
                        TotalPrice = item.TotalPrice
                    }).ToList()
                };
                db.Orders.Add(order);
                db.SaveChanges();
                Session["Cart"] = null;
                return RedirectToAction("OrderSusscess", new {id = order.OrderID});
            }
            return View(model);
        }

    }
}