﻿using MyShop.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyShop.Core.Contracts
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> Collection();
        void Commit();
        void Delete(string Id);
        T Find(string Id);
        void Insert(T p);
        void Update(T pr);
    }
}