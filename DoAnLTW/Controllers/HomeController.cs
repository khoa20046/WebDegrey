using DoAnLTW.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using DoAnLTW.Models;
using PagedList;
using System.Net;
using PagedList.Mvc;


namespace DoAnLTW.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult TrangChu()
        {
            return View();
        }

        public ActionResult SanPham()
        {
            return View();
        }

        public ActionResult Store()
        {
            return View();
        }

        public ActionResult GioiThieu()
        {
            return View();
        }

        public ActionResult ShoppingCart()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult ChiTietSanPham1()
        {
            return View();
        }

        public ActionResult ChiTietSanPham2()
        {
            return View();
        }

        public ActionResult ChiTietSanPham3()
        {
            return View();
        }

        public ActionResult ChiTietSanPham4()
        {
            return View();
        }

        public ActionResult abc()
        {
            return View();
        }

        private MyStoreEntities db = new MyStoreEntities();

        public ActionResult Index(string searchTern, int? page)
        {
            var model = new HomeProductVM();
            var products = db.Products.AsQueryable();

            if (!string.IsNullOrEmpty(searchTern))
            {
                model.SearchTerm = searchTern;
                products = products.Where(p => p.ProductName.Contains(searchTern) ||
                                    p.ProductDescription.Contains(searchTern) ||
                                    p.Category.CategoryName.Contains(searchTern));
            }

            int pageNumber = page ?? 1;
            int pageSize = 6;

            

            model.NewProducts = products.OrderBy(p => p.OrderDetails.Count()).Take(20).ToPagedList(pageNumber, pageSize);

            model.FeaturedProducts = products.OrderByDescending(p => p.OrderDetails.Count()).Take(16).ToList();

            return View(model);
        }

        //GET : Home/ProductDetails/5
        public ActionResult ProductDetails(int? id, int? quantity, int? page)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product pro = db.Products.Find(id);
            if (pro == null)
            {
                return HttpNotFound();
            }
            //Lấy tất cả các sản phẩm cùng danh mục
            var products = db.Products.Where(p => p.CategoryID == pro.CategoryID && p.ProductID != pro.ProductID).AsQueryable();
            ProductDetailsVM model = new ProductDetailsVM();

            //Đoạn code liên quan tới phân trang 
            //lấy số trang hiện tại (mặc định là trang 1 nếu không có giá trị )
            //int pageNumber = page ?? 1;
            //int pageSize = model.PageSize; //Số sản phẩm mỗi trang
            //model.product = pro;
            //model.RelatedProducts = products.OrderBy(p => p.ProductID).Take(8).ToPagedList(pageNumber, pageSize);
            //model.TopProducts = Product.OrderByDescending(p => p.OrderDetails.Count()).Take(8).ToPagedList(pageNumber, pageSize);

            //if (quantity.HasValue)
            //{
            //    model.Quantity = quantity.Value;
            //}
            return View(model);
        }
    }
}