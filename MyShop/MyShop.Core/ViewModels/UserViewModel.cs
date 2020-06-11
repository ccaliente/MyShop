using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.ViewModels
{
    public class UserViewModel
    {
        public Customer Cust { get; set; }
        public string SortBy { get; set; }
        public IEnumerable<Order> UserOrders { get; set; }
    }
}
