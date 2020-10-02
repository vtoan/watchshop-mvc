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
        List<ProductVM> GetProductVMs(watchContext context, List<PromProductVM> proms, int idCate = -1);
        PropDetailVM GetProdDetailVM(watchContext context, int id);
        //Handler
        List<ProductVM> GetProductVMsByIDs(List<ProductVM> products, string ids);
        List<ProductVM> GetProductVMsByChar(List<ProductVM> products, string character);
        List<ProductVM> GetTopProductVMs(List<ProductVM> products);
        List<ProductVM> GetPromProductVMs(List<ProductVM> products);
        // Tuple<int, int> GetTotalPriceOf(List<ProductVM> products, List<Dictionary<string, int>> items);
        void GetProductInCarts(List<ProductVM> products, ref List<OrderDetailVM> items);
    }
    public class ProductModel : IProductModel
    {
        //Get DATA
        public List<ProductVM> GetProductVMs(watchContext context, List<PromProductVM> proms, int idCate = -1)
        {
            // Db
            List<Product> asset = null;
            using (ProductDao db = new ProductDao())
                asset = idCate == -1 ? db.GetListDeep(context)
                    : db.GetListByCate(context, idCate);
            // Convert to ViewModel
            if (asset == null) return null;
            List<ProductVM> result = Helper.LsObjectToLsVM<ProductVM, Product>(asset);
            AddDiscount(ref result, proms);
            return result;
        }

        public PropDetailVM GetProdDetailVM(watchContext context, int id)
        {
            // Db
            ProductDetail asset = null;
            using (ProductDao db = new ProductDao())
                asset = db.Get(context, id);
            // Convert to ViewModel
            if (asset == null) return null;
            PropDetailVM detailVM = Helper.ObjectToVM<PropDetailVM, ProductDetail>(asset);
            return detailVM;
        }

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

        // public Tuple<int, int> GetTotalPriceOf(List<ProductVM> products, List<Dictionary<string, int>> items)
        // {
        //     double total = 0;
        //     int countItems = 0;
        //     if (products != null || products.Count != 0 || items != null || items.Count != 0)
        //     {
        //         items.ForEach(item =>
        //         {
        //             ProductVM obj = products.Find(p => p.ID == item["id"]);
        //             if (obj != null)
        //             {
        //                 int quantity = item["quantity"];
        //                 countItems += quantity;
        //                 total += (obj.Discount != 0 ? obj.Price * (1 - (obj.Discount / 100)) : obj.Price) * quantity;
        //             }

        //         });
        //     }
        //     return new Tuple<int, int>(Convert.ToInt32(total), countItems);
        // }

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
        //Match discount for products
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