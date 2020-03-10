using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using MyShop.Core;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Product> products = new List<Product>();

        public ProductRepository()
        {
            this.products = cache["products"] as List<Product>;
            if (products == null)
            {
                products = new List<Product>();
            }
        }

        public void Commit()
        {
            cache["products"] = products;
        }

        public void Insert(Product p)
        {
            products.Add(p);
        }

        public void Update(Product pr)
        {
            Product productToUpdate = products.Find(p => p.Id == pr.Id);
            if (productToUpdate != null)
                productToUpdate = pr;
            else
                throw new Exception("Product not found!");
        }

        public Product Find(string Id)
        {
            Product FindedProduct = products.Find(p => p.Id == Id);
            if (FindedProduct != null)
                return FindedProduct;
            else
                throw new Exception("Product not found!");
        }

        public List<Product> Collection()
        {
            return products.ToList();
        }

        public void Delete(string Id)
        {
            Product productToDelete = products.Find(p => p.Id == Id);
            if (productToDelete != null)
                products.Remove(productToDelete);
            else
                throw new Exception("Product not found and cant be deleted!");
        }
    }
}
