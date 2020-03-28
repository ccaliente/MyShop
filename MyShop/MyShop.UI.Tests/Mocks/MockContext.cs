using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.UI.Tests.Mocks
{
    public class MockContext<T> : IRepository<T> where T : BaseEntity
    {
        List<T> items;
        string className;

        public MockContext()
        {
            this.items = new List<T>();
        }

        public void Commit()
        {
            return;
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

        public IQueryable<T> Collection()
        {
            return items.AsQueryable();
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