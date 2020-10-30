using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using System.Collections;
using System.Collections.Generic;

namespace aspcore_watchshop.Hepler
{
    public enum Changed
    {
        PRODUCT, MEN_PRODUCT, WOMEN_PRODUCT, COUPLE_PRODUCT, ACCESSORIES_PRODUCT, DISCOUNT_PRODUCT, SELLER_PRODUCT,
        POLICY, INFO, FEE, BAND, TYPEWIRE,
        MEN_SEO, WOMEN_SEO, ACCESSORIES_SEO, COUPLE_SEO
    }
    public class CacheHelper
    {
        private static Dictionary<string, bool> DataChanged = new Dictionary<string, bool>()
        {
            {"product",false},
            {"men_product",false},
            {"women_product",false},
            {"couple_product",false},
            {"assessories_product",false},
            {"discount_product",false},
            {"seller_product",false},
            {"info",false},
            { "fee",false},
            { "band",false},
            { "typewire",false},
            { "men_seo",false},
            { "women_seo",false},
            { "assessories_seo",false},
            { "couple_seo",false}
        };

        public static void Set(IMemoryCache cache, object data, Changed keyChanged)
        {
            string key = keyChanged.ToString().ToLower();
            if (data == null) return;
            Task task = new Task(() => cache.Set(key, data));
            task.Start();
        }

        public static T Get<T>(IMemoryCache cache, Changed keyChanged)
        {
            string key = keyChanged.ToString().ToLower();
            var result = cache.Get(key);
            return result == null ? default(T) : (T)result;
        }

        public static bool isChanged(Changed keyChanged)
        {
            string key = keyChanged.ToString().ToLower();
            bool status;
            if (!DataChanged.TryGetValue(key, out status))
                return false;
            return status;
        }

        public static void DataUpdated(Changed keyChanged)
        {
            string key = keyChanged.ToString().ToLower();
            bool status;
            if (DataChanged.TryGetValue(key, out status))
                DataChanged.TryAdd(key, true);
        }
    }
    // public static class CacheKey
    // {
    //     public const string PRODUCT = "prod";
    //     public const string MEN = "men_prod";
    //     public const string WOMEN = "women_prod";
    //     public const string ACCESSORIES = "assessories_prod";
    //     public const string COUPLE = "couple_prod";
    //     public const string DISCOUNT = "discount_prod";
    //     public const string SELLER = "seller_prod";
    //     public const string MEN_SEO = "men_seo";
    //     public const string WOMEN_SEO = "women_seo";
    //     public const string ACCESSORIES_SEO = "assessories_seo";
    //     public const string COUPLE_SEO = "couple_seo";
    //     public const string INFO_SEO = "info_seo";
    //     public const string WIRE_INFO = "info_seo";
    //     public const string FEES = "fees";
    // }
}