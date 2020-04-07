using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.UI.Controllers
{
    public class OrderManagerController : Controller
    {
        IOrderService orderService;

        public OrderManagerController(IOrderService OrderService)
        {
            this.orderService = OrderService;
        }

        // GET: OrderManager
        public ActionResult Index()
        {
            List<Order> orderList = orderService.GetOrderList();
            return View(orderList);
        }

        public ActionResult UpdateOrder(string Id)
        {
            ViewBag.StatusList = new List<string>() {
                "Order Created",
                "Order Processed",
                "Order Shipped",
                "Order Complete"
            };
            Order order = orderService.GetOrder(Id);
            return View(order);
        }

        [HttpPost]
        public ActionResult UpdateOrder(Order UpdatedOrder, string Id)
        {
            Order order = orderService.GetOrder(Id);
            order.OrderStatus = UpdatedOrder.OrderStatus;
            orderService.UpdateOrder(order);
            return RedirectToAction("Index");
        }

    }
}