using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace MyShop.DataAccess.SQL
{
    public class SQLRepository<T> : IRepository<T> where T : BaseEntity
    {
        internal DataContext context;
        internal DbSet<T> dbSet;

        public SQLRepository(DataContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();

        }

        public IQueryable<T> Collection()
        {
            return dbSet;
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void Delete(string Id)
        {
            var f = Find(Id); 
            if (context.Entry(f).State == EntityState.Detached)
                dbSet.Attach(f);

            dbSet.Remove(f);

        }

        public T Find(string Id)
        {
            return dbSet.Find(Id);
        }

        public void Insert(T p)
        {
            dbSet.Add(p);
        }

        public void Update(T p)
        {
            dbSet.Attach(p);
            context.Entry(p).State = EntityState.Modified;

        }
    }
}
