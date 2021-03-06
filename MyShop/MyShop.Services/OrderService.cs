﻿using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Services
{
    public class OrderService : IOrderService
    {
        IRepository<Order> orderContext;
        IRepository<OrderItem> orderItemContext; //200520
        public OrderService(IRepository<Order> OrderContext, IRepository<OrderItem> OrderItemContext)
        {
            this.orderContext = OrderContext;
            this.orderItemContext = OrderItemContext;
        
        }
        public void CreateOrder(Order baseOrder, List<BasketItemViewModel> basketItems)
        {
            foreach (var item in basketItems)
            {
                baseOrder.OrderItems.Add(new OrderItem()
                {
                    OrederId = baseOrder.Id,
                    ProductId = item.Id,
                    ProductName = item.Name,
                    Price = item.Price,
                    Image = item.Image,
                    Quantity = item.Quantity,
                    
                });
            }

            orderContext.Insert(baseOrder);
            orderContext.Commit();
        }

        public List<Order> GetOrderList()
        {
            return orderContext.Collection().ToList();
        }

        public Order GetOrder(string Id)
        {
            return orderContext.Find(Id);
        }

        public void UpdateOrder(Order updatedOrder)
        {
            orderContext.Update(updatedOrder);
            orderContext.Commit();
        }

        public void DeleteOrder(string Id)
        {
            List<OrderItem> order = orderItemContext.Collection().Where(o => o.OrederId == Id).ToList();
            foreach (var item in order)
            {
                orderItemContext.Delete(item.Id);
                orderItemContext.Commit();
            }

            orderContext.Delete(Id);
            orderContext.Commit();

        }
    }
}
