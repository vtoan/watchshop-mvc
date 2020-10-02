using System;
using System.Linq;
using System.Collections.Generic;
using aspcore_watchshop.EF;
using aspcore_watchshop.Entities;
namespace aspcore_watchshop.Daos
{
    public class PostDao : IDisposable
    {
        public void Dispose() { }

        public Post Get(watchContext context, int id)
        {
            return context.Posts.Where(p => p.ProductID == id).FirstOrDefault();
        }

    }
}