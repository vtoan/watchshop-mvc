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
        private watchContext _context = null;

        public ProductDao(watchContext context) => _context = context;

        public void Dispose() { }

        private IQueryable<Product> RefProductForAdmin()
        {
            return _context.Products.Where(p => p.isDel == false);
        }

        private IQueryable<Product> RefProductForClient()
        {
            return _context.Products.Where(p => p.isDel == false && p.isShow == true);
        }
        // Query Product
        public List<Product> GetList()
        {
            return RefProductForClient()
                .Include(item => item.Band)
                .Include(item => item.Category)
                .Include(item => item.TypeWire)
                .ToList();
        }

        public List<Product> GetListByCate(int idCate)
        {
            return RefProductForClient()
                .Where(item => item.CategoryID == idCate)
                .Include(item => item.Band)
                .Include(item => item.Category)
                .Include(item => item.TypeWire)
                .ToList();
        }

        public List<Product> GetListDeep()
        {
            return RefProductForAdmin()
                .Include(item => item.Band)
                .Include(item => item.Category)
                .Include(item => item.TypeWire)
                .ToList();
        }

        public int Insert(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return product.ID;
        }

        public int UpdateStatus(int id, bool stt)
        {
            Product p = RefProductForAdmin().Where(item => item.ID == id).FirstOrDefault();
            if (p == null) return 0;
            p.isShow = stt;
            _context.SaveChangesAsync();
            return 1;
        }

        public int Remove(int id)
        {
            Product p = RefProductForAdmin().Where(item => item.ID == id).FirstOrDefault();
            if (p == null) return 0;
            p.isDel = true;
            _context.SaveChangesAsync();
            return 1;
        }

        public Product GetProduct(int id)
        {
            return RefProductForAdmin().Where(item => item.ID == id).FirstOrDefault();
        }

        // Query Product Detail
        public ProductDetail GetDetail(int id)
        {
            return _context.ProductDetails.Where(item => item.ProductID == id).FirstOrDefault();
        }

        public int InsertDetail(ProductDetail detail)
        {
            _context.ProductDetails.Add(detail);
            _context.SaveChangesAsync();
            return 1;
        }
    }
}