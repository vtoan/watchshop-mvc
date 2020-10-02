using System.Collections.Generic;
using aspcore_watchshop.EF;
using aspcore_watchshop.Models;
using Microsoft.Extensions.Caching.Memory;

namespace aspcore_watchshop.Hepler
{
    public class DataHelper
    {
        public static List<ProductVM> Products(watchContext context, IMemoryCache cache, IProductModel productModel, IPromModel promModel)
        {
            List<ProductVM> products = Cache.Get<List<ProductVM>>(cache, CacheKey.PRODUCT);
            if (products == null || products.Count == 0)
            {
                products = productModel.GetProductVMs(context, promModel.GetPromProductVMs(context));
                if (products != null)
                    Cache.Set(cache, products, CacheKey.PRODUCT);
            }
            return products;
        }
    }
}