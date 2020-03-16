using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using MyShop.Core.Contracts;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    public class InMemoryRepository<T> : IRepository<T> where T : BaseEntity
    {
        ObjectCache cache = MemoryCache.Default;
        List<T> items;
        string className;

        public InMemoryRepository()
        {
            this.className = typeof(T).Name; //Тъй като не знаем типа към който да кастнем, то взимаме typeof(T), т.е. преобразуване към типа който получаваме като параметър на дженерик класа.
            this.items = cache[className] as List<T>;
            if (this.items == null)
                this.items = new List<T>();
        }

        public void Commit()
        {
            cache[className] = items;
        }

        public void Insert(T p)
        {
            items.Add(p);
        }

        public void Update(T pr)
        {
            T productTToUpdate = items.Find(p => p.Id == pr.Id);
            if (productTToUpdate != null)
                productTToUpdate = pr;
            else
                throw new Exception(className + " " + " not found!");
        }

        public T Find(string Id)
        {
            T FindedT = items.Find(p => p.Id == Id);
            if (FindedT != null)
                return FindedT;
            else
                throw new Exception(className + " " + " not found!");
        }

        public List<T> Collection()
        {
            return items.ToList();
        }

        public void Delete(string Id)
        {
            T TtoDelete = items.Find(p => p.Id == Id);
            if (TtoDelete != null)
                items.Remove(TtoDelete);
            else
                throw new Exception("Product category not found and can't be deleted!");
        }
    }
}
