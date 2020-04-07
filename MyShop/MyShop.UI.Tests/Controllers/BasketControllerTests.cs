﻿using System;
using System.Linq;
using System.Security.Principal;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyShop.Core;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.Services;
using MyShop.UI.Controllers;
using MyShop.UI.Tests.Mocks;

namespace MyShop.UI.Tests.Controllers
{
    [TestClass]
    public class BasketControllerTests
    {
        [TestMethod]
        public void CanAddBasketItem()
        {
            IRepository<Basket> baskets = new MockContext<Basket>();
            IRepository<Product> products = new MockContext<Product>();
            IRepository<Order> orderContext = new MockContext<Order>();
            IRepository<Customer> customers = new MockContext<Customer>();

            var httpContext = new MockHttpContext();
            IBasketService basketService = new BasketService(products, baskets);
            IOrderService orderService = new OrderService(orderContext);
            var controller = new BasketController(basketService, orderService, customers);

            controller.ControllerContext = new System.Web.Mvc.ControllerContext(httpContext, new System.Web.Routing.RouteData(), controller);
            controller.AddToBasket("1");

            Basket basket = baskets.Collection().FirstOrDefault();

            Assert.IsNotNull(basket);
            Assert.AreEqual(1, basket.BasketItems.Count);
            Assert.AreEqual("1", basket.BasketItems.ToList().FirstOrDefault().ProductId);

        }

        [TestMethod]
        public void CanGetSummaryViewModel()
        {
            IRepository<Basket> baskets = new MockContext<Basket>();
            IRepository<Product> products = new MockContext<Product>();
            IRepository<Order> orderContext = new MockContext<Order>();
            IRepository<Customer> customers = new MockContext<Customer>();

            var httpContext = new MockHttpContext();

            IBasketService basketService = new BasketService(products, baskets);
            IOrderService orderService = new OrderService(orderContext);

            products.Insert( new Product() { Id = "1", Price = 10.00m });
            products.Insert(new Product() { Id = "2", Price =22.00m });

            var basket = new Basket();
            basket.BasketItems.Add( new BasketItem() { ProductId = "1", Quantity = 1 });
            basket.BasketItems.Add(new BasketItem() { ProductId = "2", Quantity = 2 });
            baskets.Insert(basket);



            var controller = new BasketController(basketService, orderService, customers);
            customers.Insert(new Customer() { Id = "1", Email = "mariq.grafova@gmail.com" });
            IPrincipal FakeUser = new GenericPrincipal(new GenericIdentity("mariq.grafova@gmail.com", "forms"), null);

            httpContext.User = FakeUser;

            httpContext.Request.Cookies.Add(new System.Web.HttpCookie("EcommerceBasket") { Value = basket.Id });
            controller.ControllerContext = new System.Web.Mvc.ControllerContext(httpContext, new System.Web.Routing.RouteData(), controller);

            var result = controller.BasketSummary() as PartialViewResult;
            var basketSummary = (BasketSummaryViewModel)result.ViewData.Model;

            Assert.AreEqual(3, basketSummary.BasketCount);
            Assert.AreEqual(32, basketSummary.BasketTotal);
        }

        [TestMethod]
        public void CanCheckOutAndCreateOrder()
        {
            IRepository<Product> products = new MockContext<Product>();
            IRepository<Order> orders = new MockContext<Order>();
            IRepository<Basket> baskets = new MockContext<Basket>();
            IRepository<Customer> customers = new MockContext<Customer>();

            products.Insert(new Product() { Id = "1", Price = 10.00m });
            products.Insert(new Product() { Id = "2", Price = 20.00m });

            Basket basket = new Basket();
            basket.BasketItems.Add( new BasketItem() { ProductId="1", Quantity = 1, BasketId= basket.Id});
            basket.BasketItems.Add(new BasketItem() { ProductId = "2", Quantity = 2, BasketId = basket.Id });
            baskets.Insert(basket);
            IBasketService basketService = new BasketService(products, baskets);
            IOrderService orderService = new OrderService(orders);


            var controller = new BasketController(basketService, orderService, customers);
            var httpContext = new MockHttpContext();

            httpContext.Request.Cookies.Add(new System.Web.HttpCookie("EcommerceBasket") { Value = basket.Id });
            controller.ControllerContext = new System.Web.Mvc.ControllerContext(httpContext, new System.Web.Routing.RouteData(), controller);

            //Act
            Order order = new Order();
            var result = controller.CheckOut(order);

            Assert.AreEqual(2, order.OrderItems.Count);
            Assert.AreEqual(0, basket.BasketItems.Count);

            Order OrderInRep = orders.Find(order.Id);
            Assert.AreEqual(2, OrderInRep.OrderItems.Count);

        }
    }
}
