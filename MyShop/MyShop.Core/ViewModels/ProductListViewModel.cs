using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;

namespace MyShop.Core.ViewModels
{
    public class ProductListViewModel
    {
        public string Category { get; set; }
        public string Search { get; set; }

        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public IPagedList<Product> Products { get; set; }
        public IEnumerable<ProductCategory> ProductCategories { get; set; }
    }
}
