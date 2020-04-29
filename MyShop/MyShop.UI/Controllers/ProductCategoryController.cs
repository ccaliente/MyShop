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
    [Authorize(Roles = "Admin")]
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
            //List<ProductCategory> productscat = context.Collection().ToList();
            return View();
        }

        public ActionResult GetData()
        {
            List<ProductCategory> data = context.Collection().ToList();
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AddOrEditPartial(string Id)
        {
            ProductCategory data = new ProductCategory();
            data = context.Find(Id);
            return View("AddOrEditPartial", data);
            //return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddOrEditPartial(ProductCategory pr)
        {
            bool status = false;
            if (ModelState.IsValid)
            {

                var data = context.Find(pr.Id);
                if (data != null)
                {
                    data.Category = pr.Category;
                    context.Commit();
                }
                else
                {
                    context.Insert(pr);
                    context.Commit();
                }

                return RedirectToAction("Index");
            }
            return new JsonResult { Data = new { status = status } };

        }

        public JsonResult Delete(string Id)
        {
            ProductCategory productcat = new ProductCategory();
            productcat = context.Find(Id);
            if (productcat != null)
            {
                if (ModelState.IsValid)
                {
                    context.Delete(Id);
                    context.Commit();
                }
            }
            return Json("success", JsonRequestBehavior.AllowGet);

        }

        //[HttpPost]
        //public ActionResult Create(ProductCategory pr)
        //{
        //    if (!ModelState.IsValid)
        //        return View(pr);
        //    else
        //    {
        //        context.Insert(pr);
        //        context.Commit();

        //        return RedirectToAction("Index");
        //    }

        //}

        //public ActionResult Edit(string Id)
        //{
        //    ProductCategory productcat = new ProductCategory();
        //    productcat = context.Find(Id);
        //    if (productcat == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    else
        //        return View(productcat);

        //}

        //[HttpPost]
        //public ActionResult Edit(Product pr, string Id)
        //{
        //    ProductCategory prodcat = new ProductCategory();
        //    prodcat = context.Find(Id);
        //    if (prodcat == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    else
        //    {
        //        if (!ModelState.IsValid)
        //            return View(pr);
        //        else
        //        {
        //            prodcat.Category = pr.Category;

        //            context.Commit();
        //            return RedirectToAction("Index");
        //        }
        //    }

        //}

        //public ActionResult Delete(string Id)
        //{
        //    ProductCategory productcat = new ProductCategory();
        //    productcat = context.Find(Id);
        //    if (productcat == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    else
        //        return View(productcat);

        //}

        //[HttpPost]
        //[ActionName("Delete")]
        //public ActionResult Delete(ProductCategory pr, string Id)
        //{

        //    ProductCategory prodcat = new ProductCategory();
        //    prodcat = context.Find(Id);
        //    if (prodcat == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    else
        //    {
        //        if (!ModelState.IsValid)
        //            return View(pr);
        //        else
        //        {
        //            context.Delete(Id);
        //            context.Commit();
        //            return RedirectToAction("Index");
        //        }
        //    }
        //}
    }
}