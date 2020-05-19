using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.UI.Controllers
{

    [Authorize]
    public class UserController : Controller
    {
        IRepository<Customer> contextC;
        IRepository<InvoiceData> contextInv;
        IOrderService orderService;

        public UserController(IRepository<Customer> customerContext, IRepository<InvoiceData> invoiceContext, IOrderService OrderService)
        {
            this.contextC = customerContext;
            this.contextInv = invoiceContext;
            this.orderService = OrderService;
        }

        // GET: User
        public ActionResult Index()
        {
            Customer customer = contextC.Collection().FirstOrDefault(c => c.Email == User.Identity.Name);
            return View(customer);
        }

        [HttpGet]
        public PartialViewResult UserPartial()
        {
            Customer customer = contextC.Collection().FirstOrDefault(c => c.Email == User.Identity.Name);
            return PartialView("UserPartial", customer);
        }

        [HttpPost]
        public ActionResult UserPartial(Customer user, HttpPostedFileBase file)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                Customer u = contextC.Find(user.Id);
                u.City =  user.City;
                u.Country = user.Country;
                u.FirstName = user.FirstName;
                u.LastName = user.LastName;
                u.Phone = user.Phone;
                u.Street = user.Street;
                if(string.IsNullOrEmpty(user.ZipCode))
                    u.ZipCode = "";
                else
                    u.ZipCode = user.ZipCode;
                contextC.Commit();
                TempData["Success"] = "Updated Successfully!";
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult UserOrderPartial()
        {
            List<Order> data = orderService.GetOrderList();
            return PartialView("UserOrderPartial", data);
            //return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }

    }
}