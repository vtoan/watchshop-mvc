using System;
using System.Collections.Generic;
using System.Linq;
using aspcore_watchshop.EF;
using aspcore_watchshop.Entities;

namespace aspcore_watchshop.Daos
{
    public class CategoryDao : IDisposable
    {
        public void Dispose() { }

        public List<Category> GetList(watchContext context)
        {
            return context.Categories.ToList();
        }

        public Category Get(watchContext context, int id)
        {
            return context.Categories.Where(item => item.ID == id).FirstOrDefault();
        }

    }
}