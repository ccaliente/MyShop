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
using Microsoft.AspNet.Identity;

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
            List<ProductCategory> prodCat = productCategories.Collection().ToList();
            if (Category == "")
                Category = null;
            int pagesize = (pageSize ?? 5);
            ViewBag.psize = pagesize;
            ViewBag.Min = prodScont.GetMinPrice();
            ViewBag.Max = prodScont.GetMaxPrice();

            products = prodScont.SearchProducts(minPrice, maxPrice, Category, Search).ToPagedList(page ?? 1, pagesize);

            ProductListViewModel model = new ProductListViewModel();
            model.Products = products;
            model.ProductCategories = prodCat;
            model.Category = Category;
            model.Search = Search;
            model.Page = page;
            return View(model);
        }

        public PartialViewResult FilterProducts(int? page, int? pageSize, int? maxPrice, int? minPrice, string Category = null, string Search = null)
        {
            FilterProductsViewModel model = new FilterProductsViewModel();
            IPagedList<Product> products;
            if (Category == "")
               Category = null;
            int pagesize = (pageSize ?? 5);
            ViewBag.psize = pagesize;

            products = prodScont.SearchProducts(minPrice, maxPrice, Category, Search).ToPagedList(page ?? 1, pagesize);

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
        public ActionResult Comment(CommentViewModel model)
        {
            Comment com = new Comment();
            model.Text = model.Text;
            model.ProductId = model.ProductId;
            model.UserId = User.Identity.GetUserId();


            return View();
        }

    }
}