using System;
using System.Collections.Generic;
using System.Linq;
using aspcore_watchshop.EF;
using Microsoft.EntityFrameworkCore;

namespace aspcore_watchshop.Daos {
    public class ProductDao : DAO<Product> {
        private IQueryable<Product> RefProductForAdmin (watchContext context) {
            return context.Products.Where (p => p.isDel == false);
        }

        private IQueryable<Product> RefProductForClient (watchContext context) {
            return context.Products.Where (p => p.isDel == false && p.isShow == true);
        }

        // ============== Admin =============
        public bool Insert (watchContext context, Product product, ProductDetail productDetail) {
            product.ProductDetail = productDetail;
            return base.Insert (context, product);
        }

        public override List<Product> GetList (watchContext context) {
            return RefProductForAdmin (context)
                .Include (item => item.Band)
                .Include (item => item.Category)
                .Include (item => item.TypeWire)
                .ToList ();
        }

        public override Product Get (watchContext context, int id) {
            return RefProductForAdmin (context).Where (item => item.Id == id).FirstOrDefault ();
        }

        public override bool Remove (watchContext context, int id) {
            Product p = RefProductForAdmin (context).Where (item => item.Id == id).FirstOrDefault ();
            if (p == null) return false;
            p.isDel = true;
            context.SaveChangesAsync ();
            return true;
        }

        public bool UpdateStatus (watchContext context, int id, bool stt) {
            Product p = RefProductForAdmin (context).Where (item => item.Id == id).FirstOrDefault ();
            if (p == null) return false;
            p.isShow = stt;
            context.SaveChangesAsync ();
            return true;
        }

        // ============== Client =============
        public List<Product> GetListByCate (watchContext context, int idCate) {
            return RefProductForClient (context)
                .Where (item => item.CategoryId == idCate)
                .Include (item => item.Band)
                .Include (item => item.Category)
                .Include (item => item.TypeWire)
                .ToList ();
        }

        public List<Product> GetListAvaiable (watchContext context) {
            return RefProductForClient (context)
                .Include (item => item.Band)
                .Include (item => item.Category)
                .Include (item => item.TypeWire)
                .ToList ();
        }
    }
}