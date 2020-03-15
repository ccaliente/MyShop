using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productscat = new List<ProductCategory>();

        public ProductCategoryRepository()
        {
            this.productscat = cache["productscat"] as List<ProductCategory>;
            if (productscat == null)
            {
                productscat = new List<ProductCategory>();
            }
        }

        public void Commit()
        {
            cache["productscat"] = productscat;
        }

        public void Insert(ProductCategory p)
        {
            productscat.Add(p);
        }

        public void Update(ProductCategory pr)
        {
            ProductCategory productToUpdate = productscat.Find(p => p.Id == pr.Id);
            if (productToUpdate != null)
                productToUpdate = pr;
            else
                throw new Exception("Product category not found!");
        }

        public ProductCategory Find(string Id)
        {
            ProductCategory FindedProductCat = productscat.Find(p => p.Id == Id);
            if (FindedProductCat != null)
                return FindedProductCat;
            else
                throw new Exception("Product not found!");
        }

        public List<ProductCategory> Collection()
        {
            return productscat.ToList();
        }

        public void Delete(string Id)
        {
            ProductCategory productCatToDelete = productscat.Find(p => p.Id == Id);
            if (productCatToDelete != null)
                productscat.Remove(productCatToDelete);
            else
                throw new Exception("Product category not found and can't be deleted!");
        }
    }
}
