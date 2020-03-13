using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MyShop.UI.Controllers
{
    public class ProductManagerController : Controller
    {
        ProductRepository context;

        public ProductManagerController()
        {
            context = new ProductRepository();
        }

        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }

        public ActionResult Create()
        {
            Product product = new Product();
            return View(product);
        }

        [HttpPost]
        public ActionResult Create(Product pr)
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
            Product product = new Product();
            product = context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
                return View(product);

        }

        [HttpPost]
        public ActionResult Edit(Product pr, string Id)
        {
            Product prod = new Product();
            prod = context.Find(Id);
            if (prod == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                    return View(pr);
                else
                {
                    prod.Category = pr.Category;
                    prod.Description = pr.Description;
                    prod.Image = pr.Image;
                    prod.Name = pr.Name;
                    prod.Price = pr.Price;

                    context.Commit();
                    return RedirectToAction("Index");
                }
            }

        }

        public ActionResult Delete(string Id)
        {
            Product product = new Product();
            product = context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
                return View(product);

        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult Delete(Product pr, string Id)
        {

            Product prod = new Product();
            prod = context.Find(Id);
            if (prod == null)
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