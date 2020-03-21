﻿using MyShop.Core;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyShop.Services
{
    public class BasketService
    {
        IRepository<Product> ProdContext;
        IRepository<Basket> BasketContext;

        public const string BasketSessionName = "eCommerceBasket";

        public BasketService(IRepository<Product> prodContext, IRepository<Basket> basketContext)
        {
            ProdContext = prodContext;
            BasketContext = basketContext;
        }

        private Basket GetBasket(HttpContextBase HttpContext, bool CreateIfNull)
        {
            HttpCookie cookie = HttpContext.Request.Cookies.Get(BasketSessionName); //подаваме константата като параметър на Get, за да я преобразува към тип cookie

            Basket basket = new Basket();
            if (cookie != null)
            {
                string BasketId = cookie.Value;
                if (!string.IsNullOrEmpty(BasketId))
                {
                    basket = BasketContext.Find(BasketId);
                }
                else
                {
                    if (CreateIfNull)
                    {
                        basket = CreateNewBasket(HttpContext);
                    }
                }
            }
            else
            {
                if (CreateIfNull)
                {
                    basket = CreateNewBasket(HttpContext);
                }
            }
            return basket;
        }

        private Basket CreateNewBasket(HttpContextBase httpContext)
        {
            Basket basket = new Basket();
            BasketContext.Insert(basket);
            BasketContext.Commit();

            HttpCookie cookie = new HttpCookie(BasketSessionName);
            cookie.Value = basket.Id;
            cookie.Expires = DateTime.Now.AddDays(1);
            httpContext.Response.Cookies.Add(cookie);

            return basket;
        }

        public void AddToBasket(HttpContextBase httpContext, string productId)
        {
            Basket basket = GetBasket(httpContext, true);
            BasketItem item = basket.BasketItems.FirstOrDefault(i => i.ProductId == productId);
            if (item == null)
            {
                item = new BasketItem()
                {
                    BasketId = basket.Id,
                    ProductId = productId,
                    Quantity = 1
                };
                basket.BasketItems.Add(item);
            }
            else
                item.Quantity = item.Quantity + 1;

                BasketContext.Commit();
         
        }

        public void RemoveFromBasket(HttpContextBase httpContext, string itemId)
        {
            Basket basket = GetBasket(httpContext, true);
            BasketItem item = basket.BasketItems.FirstOrDefault(i => i.Id == itemId);
            if (item != null)
            {
                basket.BasketItems.Remove(item);
                BasketContext.Commit();
            }
        }
    }
}
