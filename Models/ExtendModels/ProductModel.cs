using System;
using System.Collections.Generic;
using System.Linq;
using aspcore_watchshop.Daos;
using aspcore_watchshop.EF;
using aspcore_watchshop.Hepler;
using aspcore_watchshop.Interfaces;
using aspcore_watchshop.ViewModels;

namespace aspcore_watchshop.Models {

    public class ProductModel : DataModelBase<ProductVM, Product>, IProductModel {
        public ProductModel () : base (new ProductDao ()) { }
        //Get DATA
        public bool AddModel (watchContext context, ProductVM product, ProdDetailVM prodDetail) {
            var obj = DataHelper.Instance.ObjectMapperTo<Product, ProductVM> (product);
            var objDetail = DataHelper.Instance.ObjectMapperTo<ProductDetail, ProdDetailVM> (prodDetail);
            if (obj == null) return false;
            if (objDetail == null) objDetail = new ProductDetail ();
            using (ProductDao db = new ProductDao ())
            db.Insert (context, obj, objDetail);
            return true;
        }

        public List<ProductVM> GetListVMs (watchContext context, int idCate) {
            using (ProductDao db = new ProductDao ())
            return DataHelper.Instance.LsObjectMapperTo<ProductVM, Product> (idCate == 0 ?
                db.GetListAvaiable (context) : db.GetListByCate (context, idCate));
        }

        public bool UpdateStatus (watchContext context, int id, bool stt) {
            using (ProductDao db = new ProductDao ())
            return db.UpdateStatus (context, id, stt);
        }

        #region Helpful
        //================== Handler List Product //==================
        public List<ProductVM> GetProductVMsByIDs (List<ProductVM> products, string ids) {
            if (products == null || ids == null) return null;
            int[] arrInt = ids.Split (',').Where (s => s != "").Select (int.Parse).ToArray ();
            List<ProductVM> re = products.FindAll (item => arrInt.Contains ((int) item.Id));
            return re;
        }

        public List<ProductVM> GetProductVMsByChar (List<ProductVM> products, string character) {
            if (products == null) return null;
            var data = products.Where (item => item.Name.Contains (character) ||
                item.BandName.Contains (character)).ToList ();
            return data;
        }

        public List<ProductVM> GetTopProductVMs (List<ProductVM> products) {
            if (products == null) return null;
            return products.OrderByDescending (item => item.SaleCount).ToList ();
        }

        public List<ProductVM> GetPromProductVMs (List<ProductVM> products) {
            if (products == null) return null;
            return products.Where (item => item.Discount != null).ToList ();
        }

        public void GetProductInCarts (List<ProductVM> products, ref List<OrderDetailVM> items) {
            if (products == null || products.Count == 0 || items == null || items.Count == 0)
                return;
            items.ForEach (item => {
                ProductVM obj = products.Find (p => p.Id == item.ProductId);
                if (obj != null) {
                    if (obj.Price != null)
                        item.Price = (int) obj.Price;
                    if (obj.Discount != null)
                        item.Discount = (double) obj.Discount;
                }
            });
        }

        public void AddDiscount (ref List<ProductVM> products, List<PromProductVM> proms) {
            if (proms == null || proms.Count == 0) return;
            foreach (var promProduct in proms) {
                // Find Product math in promotin and set discount
                products.ForEach (p => {
                    if (promProduct.BandId != null && p.BandId == promProduct.BandId)
                        p.Discount = promProduct.Discount;
                    if (promProduct.CategoryId != null && p.CategoryId == promProduct.CategoryId)
                        p.Discount = promProduct.Discount;
                    if (promProduct.ProductIds.Contains ((int) p.Id))
                        p.Discount = promProduct.Discount;
                });
            }
        }

        #endregion
    }

}