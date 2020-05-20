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
        IRepository<Order> contextOrder;
        IRepository<OrderItem> itemContext;

        public UserController(IRepository<Customer> customerContext, IRepository<InvoiceData> invoiceContext, IOrderService OrderService, IRepository<Order> ContextOrder, IRepository<OrderItem> ItemContext)
        {
            this.contextC = customerContext;
            this.contextInv = invoiceContext;
            this.orderService = OrderService;
            this.contextOrder = ContextOrder;
            this.itemContext = ItemContext;
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
            return PartialView("UserOrderPartial", data.Where(o => o.Email == User.Identity.Name));
            //return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Delete(string Id)
        {
            Order or = contextOrder.Find(Id);

            if (or.OrderStatus != "Order Shipped")
            {
                orderService.DeleteOrder(Id);
                orderService.GetOrderList().Where(o => o.Email == User.Identity.Name);
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            else
                return Json("not success", JsonRequestBehavior.AllowGet);

        }

        public decimal OrderSummary(string Id)
        {
            List<Order> order = contextOrder.Collection().Where(o => o.Id==Id).ToList();
            List<OrderItem> items = itemContext.Collection().Where(i => i.OrederId== Id).ToList();
            decimal OrderTotal = (from item in order
                                   join p in items on item.Id equals p.OrederId
                                   select p.Quantity * p.Price).Sum();

            return OrderTotal;
        }

    }
}