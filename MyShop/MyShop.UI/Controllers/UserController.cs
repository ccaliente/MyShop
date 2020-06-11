using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
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
        public ActionResult Index(string listChoice = null)
        {
            UserViewModel vm = new UserViewModel();
            vm.Cust = contextC.Collection().FirstOrDefault(c => c.Email == User.Identity.Name);
            vm.UserOrders = contextOrder.Collection().Where(o => o.Email == User.Identity.Name);
            if (listChoice == null)
                listChoice = "User";
            vm.SortBy = listChoice;
                
            return View(vm);

        }

        [HttpGet]
        public PartialViewResult UserPartial()
        {
            Customer customer = contextC.Collection().FirstOrDefault(c => c.Email == User.Identity.Name);
            return PartialView("UserPartial");
        }

        //[HttpGet]
        //public PartialViewResult UserPartial() 
        //{ 
        //    Customer customer = contextC.Collection().FirstOrDefault(c => c.Email == User.Identity.Name);
        //    return PartialView("UserPartial", customer);
        //}

        [HttpPost]
        public ActionResult UserPartial(Customer user, HttpPostedFileBase file)
        {
            bool status = false;

            if (ModelState.IsValid)
            {
                Customer u = contextC.Find(user.Id);
                u.City =  user.City;
                u.Country = user.Country;
                u.Email = user.Email;
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
        }

        //[HttpGet]
        //public ActionResult UserOrderPartial(string listId)
        //{
        //    List<Order> data = orderService.GetOrderList();
        //    return PartialView("UserOrderPartial", data.Where(o => o.Email == User.Identity.Name));
        //}

        [HttpPost]
        public JsonResult Delete(string Id)
        {
            Order or = contextOrder.Find(Id);

            if (or.OrderStatus != "Order Shipped"  && or.OrderStatus != "Order Complete")
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

        public ActionResult OrderDetails(string Id)
        {
            Order order = orderService.GetOrder(Id);
            return View(order);
        }

    }
}