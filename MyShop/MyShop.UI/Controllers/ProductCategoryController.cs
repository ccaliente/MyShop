using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.UI.Controllers
{
    public class ProductCategoryController : Controller
    {
        IRepository<ProductCategory> context;

        public ProductCategoryController(IRepository<ProductCategory> prodcatContext)
        {
            context = prodcatContext;
        }

        // GET: ProductManager
        public ActionResult Index()
        {
            List<ProductCategory> productscat = context.Collection().ToList();
            return View(productscat);
        }

        public ActionResult Create()
        {
            ProductCategory productcat = new ProductCategory();
            return View(productcat);
        }

        [HttpPost]
        public ActionResult Create(ProductCategory pr)
        {
            if (!ModelState.IsValid)
                return View(pr);
            else
            {
                context.Insert(pr);
                context.Commit();

                return RedirectToAction("Index");
            }

        }

        public ActionResult Edit(string Id)
        {
            ProductCategory productcat = new ProductCategory();
            productcat = context.Find(Id);
            if (productcat == null)
            {
                return HttpNotFound();
            }
            else
                return View(productcat);

        }

        [HttpPost]
        public ActionResult Edit(Product pr, string Id)
        {
            ProductCategory prodcat = new ProductCategory();
            prodcat = context.Find(Id);
            if (prodcat == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                    return View(pr);
                else
                {
                    prodcat.Category = pr.Category;

                    context.Commit();
                    return RedirectToAction("Index");
                }
            }

        }

        public ActionResult Delete(string Id)
        {
            ProductCategory productcat = new ProductCategory();
            productcat = context.Find(Id);
            if (productcat == null)
            {
                return HttpNotFound();
            }
            else
                return View(productcat);

        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult Delete(ProductCategory pr, string Id)
        {

            ProductCategory prodcat = new ProductCategory();
            prodcat = context.Find(Id);
            if (prodcat == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                    return View(pr);
                else
                {
                    context.Delete(Id);
                    context.Commit();
                    return RedirectToAction("Index");
                }
            }
        }
    }
}