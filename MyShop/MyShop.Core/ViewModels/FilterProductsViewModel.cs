using MyShop.Core.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.ViewModels
{
    public class FilterProductsViewModel
    {
        public IPagedList<Product> Products { get; set; }
    }
}
