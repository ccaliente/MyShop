using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace MyShop.UI.Controllers
{
    public class HomeController : Controller
    {
        IRepository<Product> context;
        IRepository<ProductCategory> productCategories;

        public HomeController(IRepository<Product> productContext, IRepository<ProductCategory> prodcatContext)
        {
            context = productContext;
            productCategories = prodcatContext;
        }

        public ActionResult Index(string Search, int? i, string Category = null)
        {
            List<Product> products;
            List<ProductCategory> prodCat = productCategories.Collection().ToList();
            var list = new SelectList(new[]
            {
                new { ID = "1", Name = "10" },
                new { ID = "2", Name = "15" },
                new { ID = "3", Name = "20" },
            },
            "ID", "Name", 1);

            ViewData["list"] = list;

            if (Category == null)
            {
               products =  context.Collection().ToList();
            }
            else
            {
                products = context.Collection().Where(m => m.Category == Category).ToList();
            }

            ProductListViewModel model = new ProductListViewModel();
            model.Products = products;
            model.ProductCategories = prodCat;

            int pageSize = 5;
            int pageNumber = (i ?? 1);

            return View(model);
        }

        public ActionResult Details(string Id)
        {
            Product product = context.Find(Id);
            if (product != null)
            {
                return View(product);
            }
            else
                return HttpNotFound();

        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}