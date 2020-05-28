using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace MyShop.UI.Controllers
{
    public class HomeController : Controller
    {
        IRepository<Product> context;
        IRepository<ProductCategory> productCategories;
        IProductService prodScont;

        public HomeController(IRepository<Product> productContext, IRepository<ProductCategory> prodcatContext, IProductService prodServcontext )
        {
            context = productContext;
            productCategories = prodcatContext;
            prodScont = prodServcontext;
        }

        public ActionResult Index(string Search, int? page, int? pageSize, int? maxPrice, int? minPrice, string Category = null)
        {

            IPagedList<Product> products;
            //List<Product> products;
            List<ProductCategory> prodCat = productCategories.Collection().ToList();
            //int pagesize = 5;
            ViewBag.pageSize = new SelectList(new[]
            {
                new { ID = "5", Name = "5" },
                new { ID = "10", Name = "10" },
                new { ID = "15", Name = "15" },
            },
            "ID", "Name", 1);

            int pagesize = (pageSize ?? 20);
            ViewBag.psize = pagesize;
            ViewBag.Min = prodScont.GetMinPrice();
            ViewBag.Max = prodScont.GetMaxPrice();
            //minPrice = 0;
            //maxPrice = 100;
            //if (Category == null)
            //{
            //   products =  context.Collection().OrderBy(pr => pr.Id).ToPagedList(page ?? 1, pagesize);

            //}
            //else
            //{
            //    products = context.Collection().Where(m => m.Category == Category).OrderBy(pr => pr.Id).ToPagedList(page ?? 1, pagesize);

            //27.05.20//
            products = prodScont.SearchProducts(minPrice, maxPrice, Category).ToPagedList(page ?? 1, pagesize);
            ///////////
            if (!String.IsNullOrEmpty(Search))
            {
                products = products.Where(pr => pr.Name.ToUpper().Contains(Search.ToUpper())
                    || pr.Name.ToUpper().Contains(Search.ToUpper())).OrderBy(pr => pr.Id).ToPagedList(page ?? 1, pagesize);
            }
            ProductListViewModel model = new ProductListViewModel();
            model.Products = products;
            model.ProductCategories = prodCat;
            return View(model);
        }

        public ActionResult FilterProducts(string Search, int? page, int? pageSize, int? maxPrice, int? minPrice, string Category = null)
        {
            FilterProductsViewModel model = new FilterProductsViewModel();
            IPagedList<Product> products;

            int pagesize = (pageSize ?? 20);
            //minPrice = 0;
            //maxPrice = 100;
            products = prodScont.SearchProducts(minPrice, maxPrice, Category).ToPagedList(page ?? 1, pagesize);
            ///////////
            if (!String.IsNullOrEmpty(Search))
            {
                products = products.Where(pr => pr.Name.ToUpper().Contains(Search.ToUpper())
                    || pr.Name.ToUpper().Contains(Search.ToUpper())).OrderBy(pr => pr.Id).ToPagedList(page ?? 1, pagesize);
            }

            model.Products = products;
            return PartialView(model);
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