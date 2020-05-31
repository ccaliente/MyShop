using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Contracts
{
    public interface IProductService
    {
        decimal GetMinPrice();

        decimal GetMaxPrice();

        List<Product> SearchProducts(decimal? minPrice, decimal? maxPrice, string Category = null, string Search = null);

    }
}
