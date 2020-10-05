using System.Collections.Generic;
using aspcore_watchshop.Daos;
using aspcore_watchshop.EF;
using aspcore_watchshop.Entities;
using System.Linq;
using System;
using aspcore_watchshop.Hepler;

namespace aspcore_watchshop.Models
{
    public interface IProductModel
    {
        List<ProductVM> GetProductVMs(watchContext context, List<PromProductVM> proms, int idCate = -1); // Get product in  Client Page
        List<ProductVM> GetALLProductVMs(watchContext context); // Get product in  Admin Page
        PropDetailVM GetProdDetailVM(watchContext context, int id);
        bool Add(watchContext context, ProductVM product, PropDetailVM detail);
        bool UpdateStatus(watchContext context, int id, bool stt);
        bool Remove(watchContext context, int id);
        bool Update(watchContext context, ProductVM product, PropDetailVM detail);
        //Handler
        List<ProductVM> GetProductVMsByIDs(List<ProductVM> products, string ids);
        List<ProductVM> GetProductVMsByChar(List<ProductVM> products, string character);
        List<ProductVM> GetTopProductVMs(List<ProductVM> products);
        List<ProductVM> GetPromProductVMs(List<ProductVM> products);
        void GetProductInCarts(List<ProductVM> products, ref List<OrderDetailVM> items);
    }
    public class ProductModel : IProductModel
    {
        //Get DATA
        public List<ProductVM> GetProductVMs(watchContext context, List<PromProductVM> proms, int idCate = -1)
        {
            // Db
            List<Product> asset = null;
            using (ProductDao db = new ProductDao(context))
                asset = idCate == -1 ? db.GetList()
                    : db.GetListByCate(idCate);
            // Convert to ViewModel
            if (asset == null) return null;
            List<ProductVM> result = Helper.LsObjectMapperTo<ProductVM, Product>(asset);
            AddDiscount(ref result, proms);
            return result;
        }

        public List<ProductVM> GetALLProductVMs(watchContext context)
        {
            // Db
            List<Product> asset = null;
            using (ProductDao db = new ProductDao(context))
                asset = db.GetListDeep();
            // Convert to ViewModel
            if (asset == null) return null;
            return Helper.LsObjectMapperTo<ProductVM, Product>(asset); ;
        }

        public PropDetailVM GetProdDetailVM(watchContext context, int id)
        {
            // Db
            ProductDetail asset = null;
            using (ProductDao db = new ProductDao(context))
                asset = db.GetDetail(id);
            // Convert to ViewModel
            if (asset == null) return null;
            return Helper.ObjectMapperTo<PropDetailVM, ProductDetail>(asset);
        }

        public bool Add(watchContext context, ProductVM product, PropDetailVM detail)
        {
            using (ProductDao db = new ProductDao(context))
            {
                Product p = Helper.ObjectMapperTo<Product, ProductVM>(product);
                if (p == null) return false;
                int prodId = db.Insert(p); // Insert to DB
                if (prodId == 0) return false;
                detail.ProductID = prodId;
                ProductDetail pDetail = Helper.ObjectMapperTo<ProductDetail, PropDetailVM>(detail);
                if (pDetail == null) return false;
                db.InsertDetail(pDetail); // Insert to DB
            }
            //Update Cache
            return true;
        }

        public bool UpdateStatus(watchContext context, int id, bool stt)
        {
            using (ProductDao db = new ProductDao(context))
                return db.UpdateStatus(id, stt) == 1;
        }

        public bool Remove(watchContext context, int id)
        {
            using (ProductDao db = new ProductDao(context))
            {
                return db.Remove(id) == 1;
            }
        }

        //==================================
        public bool Update(watchContext context, ProductVM product, PropDetailVM detail)
        {
            return true;
        }
        //==================================
        //Handler List Product
        public List<ProductVM> GetProductVMsByIDs(List<ProductVM> products, string ids)
        {
            if (products == null || ids == null) return null;
            int[] arrInt = ids.Split(',').Where(s => s != "").Select(int.Parse).ToArray();
            List<ProductVM> re = products.FindAll(item => arrInt.Contains(item.ID));
            return re;
        }

        public List<ProductVM> GetProductVMsByChar(List<ProductVM> products, string character)
        {
            if (products == null) return null;
            var data = products.Where(item => item.Name.Contains(character)
                 || item.BandName.Contains(character)).ToList();
            return data;
        }

        public List<ProductVM> GetTopProductVMs(List<ProductVM> products)
        {
            if (products == null) return null;
            return products.OrderByDescending(item => item.SaleCount).ToList();
        }

        public List<ProductVM> GetPromProductVMs(List<ProductVM> products)
        {
            if (products == null) return null;
            return products.Where(item => item.Discount != 0.0).ToList();
        }

        public void GetProductInCarts(List<ProductVM> products, ref List<OrderDetailVM> items)
        {
            if (products == null || products.Count == 0 || items == null || items.Count == 0)
                return;
            items.ForEach(item =>
            {
                ProductVM obj = products.Find(p => p.ID == item.id);
                if (obj != null)
                {
                    item.price = obj.Price;
                    item.discount = obj.Discount;
                }
            });
        }

        //============== PRIVATE ====================
        private void AddDiscount(ref List<ProductVM> products, List<PromProductVM> promProds)
        {
            if (promProds == null || promProds.Count == 0) return;
            foreach (var prom in promProds)
            {
                // Find Product math in promotin and set discount
                products.ForEach(p =>
                {
                    if (prom.ProductIDs.Contains(p.ID))
                        p.Discount = prom.Discount;
                });
            }
        }
    }

    public class ProductVM
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public string WireName { get; set; }
        public string BandName { get; set; }
        public double Discount { get; set; }
        public int Price { get; set; }
        public int CategoryID { get; set; }
        public int TypeWireID { get; set; }
        public int BandID { get; set; }
        public string Image { get; set; }
        public int SaleCount { get; set; }
    }

    public class PropDetailVM
    {
        public int ProductID { get; set; }
        public string Images { get; set; }
        public string TypeGlass { get; set; }
        public string TypeBorder { get; set; }
        public string TypeMachine { get; set; }
        public string Parameter { get; set; }
        public string ResistWater { get; set; }
        public string Warranty { get; set; }
        public string Origin { get; set; }
        public string Color { get; set; }
        public string Func { get; set; }
        //SEO
        public string SeoImage { get; set; }
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
    }
}