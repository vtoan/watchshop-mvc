using System;
using System.Collections.Generic;
using aspcore_watchshop.EF;
using aspcore_watchshop.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace aspcore_watchshop.Daos
{
    public class ProductDao : IDisposable
    {
        public void Dispose() { }
        public List<Product> GetList(watchContext context)
        {
            return context.Products.Where(p => p.isDel == false && p.isShow == true)
                .Include(item => item.Band)
                .Include(item => item.Category)
                .Include(item => item.TypeWire)
                .ToList();
        }

        public List<Product> GetListDeep(watchContext context)
        {
            return context.Products
                .Include(item => item.Band)
                .Include(item => item.Category)
                .Include(item => item.TypeWire)
                .ToList();
        }

        public List<Product> GetListByCate(watchContext context, int idCate)
        {
            return context.Products.Where(item => item.CategoryID == idCate)
                .Include(item => item.Band)
                .Include(item => item.Category)
                .Include(item => item.TypeWire)
                .ToList();
        }

        public ProductDetail Get(watchContext context, int id)
        {
            return context.ProductDetails.Where(item => item.ProductID == id).FirstOrDefault();
        }




    }
}