using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace aspcore_watchshop.Hepler
{
    public static class Cache
    {
        public static bool PRODUCT = false;
        public static bool MEN_PRODUCT = false;
        public static bool WOMEN_PRODUCT = false;
        public static bool COUPLE_PRODUCT = false;
        public static bool ACCESSORIES_PRODUCT = false;
        public static bool POLICY = false;
        public static bool INFO = false;
        public static bool FEE = false;
        public static bool BAND = false;
        public static bool TYPEWIRE = false;

        public static void Set(IMemoryCache cache, object data, string key)
        {
            if (data == null) return;
            Task task = new Task(() => cache.Set(key, data));
            task.Start();
        }

        public static T Get<T>(IMemoryCache cache, string key)
        {
            var result = cache.Get(key);
            return result == null ? default(T) : (T)result;
        }
    }
    public static class CacheKey
    {
        public const string PRODUCT = "prod";
        public const string MEN = "men_prod";
        public const string WOMEN = "women_prod";
        public const string ACCESSORIES = "assessories_prod";
        public const string COUPLE = "couple_prod";
        public const string DISCOUNT = "discount_prod";
        public const string SELLER = "seller_prod";
        public const string MEN_SEO = "men_seo";
        public const string WOMEN_SEO = "women_seo";
        public const string ACCESSORIES_SEO = "assessories_seo";
        public const string COUPLE_SEO = "couple_seo";
        public const string INFO_SEO = "info_seo";
        public const string WIRE_INFO = "info_seo";
        public const string FEES = "fees";
    }
}