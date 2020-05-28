using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Services
{
    public class ProductService : IProductService
    {
        IRepository<Product> productContext;

        public ProductService(IRepository<Product> orderContext)
        {
            this.productContext = orderContext;
        }

        public decimal GetMaxPrice()
        {
            decimal max = productContext.Collection().Max(pr => pr.Price);
            return max;
        }

        public decimal GetMinPrice()
        {
            decimal min = productContext.Collection().Min(pr => pr.Price);
            return min;
        }

        public List<Product> SearchProducts(decimal? minPrice, decimal? maxPrice, string Category = null)
        {
            List<Product> prod = new List<Product>();
            if (Category == null)
            {
                prod = productContext.Collection().OrderBy(pr => pr.Id).ToList();

            }
            else
            {
                prod = productContext.Collection().Where(m => m.Category == Category).OrderBy(pr => pr.Id).ToList();
            }

            if (minPrice.HasValue)
                prod = prod.Where(p => p.Price>=minPrice).ToList();
            if (maxPrice.HasValue)
                prod = prod.Where(p => p.Price <= maxPrice).ToList();

            return prod;

        }
    }
}
