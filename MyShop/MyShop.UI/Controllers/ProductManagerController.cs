using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.DataAccess.InMemory;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductManagerController : Controller
    {
        IRepository<Product> context;
        IRepository<ProductCategory> productCategories;

        public ProductManagerController(IRepository<Product> productContext, IRepository<ProductCategory> prodcatContext)
        {
            context = productContext;
            productCategories = prodcatContext;
        }
        // GET: ProductManager
        public ActionResult Index()
        {
            ProductManagerViewModel viewModel = new ProductManagerViewModel();
            viewModel.Product = new Product();
            viewModel.ProductCategories = productCategories.Collection();

            //List<Product> products = context.Collection().ToList();
            //return View(products);
            return View(viewModel);
        }

        public ActionResult GetData()
        {
            List<Product> data = context.Collection().ToList();
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //public ActionResult AddOrEditPartial(Product pr, HttpPostedFileBase file)
        //{
        //    bool status = false;
        //    if (ModelState.IsValid)
        //    {

        //        var data = context.Find(pr.Id);
        //        if (data != null)
        //        {
        //            data.Category = pr.Category;
        //            context.Commit();
        //        }
        //        else
        //        {
        //            if (file != null)
        //            {
        //                pr.Image = pr.Id + Path.GetExtension(file.FileName); //запазваме името на файла, който качваме като ид+иметона файла
        //                file.SaveAs(Server.MapPath("//Content//ProductImages//") + pr.Image); //запазваме файла на физическия път 
        //            }
        //            context.Insert(pr);
        //            context.Commit();
        //        }

        //        return RedirectToAction("Index");
        //    }
        //    return new JsonResult { Data = new { status = status } };

        //}

        [HttpGet]
        public ActionResult AddOrEditPartial(string Id)
        {
            ProductManagerViewModel viewModel = new ProductManagerViewModel();

            //viewModel.Product = new Product();
            //viewModel.ProductCategories = productCategories.Collection();
            //return View(viewModel);

            viewModel.Product = new Product();
            viewModel.Product = context.Find(Id);
            viewModel.ProductCategories = productCategories.Collection();
            return PartialView("AddOrEditPartial", viewModel);
            //return View("AddOrEditPartial", viewModel);

        }

        [HttpPost]
        public ActionResult AddOrEditPartial(Product product, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                var data = context.Find(product.Id);
                if (data != null)
                {
                    if (file != null)
                    {
                        product.Image = product.Id + Path.GetExtension(file.FileName); //запазваме името на файла, който качваме като ид+иметона файла
                        file.SaveAs(Server.MapPath("//Content//ProductImages//") + product.Image); //запазваме файла на физическия път 
                    }
                    product.Category = product.Category;
                    product.Description = product.Description;
                    product.Name = product.Name;
                    product.Price = product.Price;
                }
                else
                {
                    if (file != null)
                    {
                        product.Image = product.Id + Path.GetExtension(file.FileName); //запазваме името на файла, който качваме като ид+иметона файла
                        file.SaveAs(Server.MapPath("//Content//ProductImages//") + product.Image); //запазваме файла на физическия път 
                    }
                    context.Insert(product);
                    context.Commit();
                }
               

                return RedirectToAction("Index");
            }

        }

        //public ActionResult Create()
        //{
        //    ProductManagerViewModel viewModel = new ProductManagerViewModel();

        //    viewModel.Product = new Product();
        //    viewModel.ProductCategories = productCategories.Collection();
        //    return View(viewModel);
        //}

        //[HttpPost]
        //public ActionResult Create(Product product, HttpPostedFileBase file)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(product);
        //    }
        //    else
        //    {
        //        if (file != null)
        //        {
        //            product.Image = product.Id + Path.GetExtension(file.FileName); //запазваме името на файла, който качваме като ид+иметона файла
        //            file.SaveAs(Server.MapPath("//Content//ProductImages//") + product.Image); //запазваме файла на физическия път 
        //        }
        //        context.Insert(product);
        //        context.Commit();

        //        return RedirectToAction("Index");
        //    }

        //}

        //public ActionResult Edit(string Id)
        //{
        //    Product product = context.Find(Id);
        //    if (product == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    else
        //    {
        //        ProductManagerViewModel viewModel = new ProductManagerViewModel();
        //        viewModel.Product = product;
        //        viewModel.ProductCategories = productCategories.Collection();

        //        return View(viewModel);
        //    }
        //}

        [HttpPost]
        public ActionResult Edit(Product product, string Id, HttpPostedFileBase file)
        {
            Product productToEdit = context.Find(Id);

            if (productToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }
                if (file != null)
                {
                    productToEdit.Image = product.Id + Path.GetExtension(file.FileName); //запазваме името на файла, който качваме като ид+иметона файла
                    file.SaveAs(Server.MapPath("//Content//ProductImages//") + productToEdit.Image); //запазваме файла на физическия път 
                }
                productToEdit.Category = product.Category;
                productToEdit.Description = product.Description;
                productToEdit.Name = product.Name;
                productToEdit.Price = product.Price;

                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public JsonResult Delete(string Id)
        {
            Product prod = new Product();
            prod = context.Find(Id);
            if (prod != null)
            {
                if (ModelState.IsValid)
                {
                    context.Delete(Id);
                    context.Commit();
                }
            }
            return Json("success", JsonRequestBehavior.AllowGet);

        }

        //public ActionResult Delete(string Id)
        //{
        //    Product productToDelete = context.Find(Id);

        //    if (productToDelete == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    else
        //    {
        //        return View(productToDelete);
        //    }
        //}

        //[HttpPost]
        //[ActionName("Delete")]
        //public ActionResult ConfirmDelete(string Id)
        //{
        //    Product productToDelete = context.Find(Id);

        //    if (productToDelete == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    else
        //    {
        //        context.Delete(Id);
        //        context.Commit();
        //        return RedirectToAction("Index");
        //    }
        //}

    }
}