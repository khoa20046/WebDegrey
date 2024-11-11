using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnLTW.Models.ViewModel
{
    public class HomeProductVM 
    {
        // GET: HomeProductVM
        public string SearchTerm { get; set; }
        
        public string PageNumber { get; set; }
        public string PageSize { get; set; }

        public List<Product> FeaturedProducts { get; set; }

        public PagedList.IPagedList<Product> NewProducts { get; set; }
        
    }
}