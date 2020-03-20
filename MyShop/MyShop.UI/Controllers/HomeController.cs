using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        public ActionResult Index(string Category=null)
        {
            List<Product> products;
            List<ProductCategory> prodCat = productCategories.Collection().ToList();

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