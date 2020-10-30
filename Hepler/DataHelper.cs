using System;
using System.Collections.Generic;
using System.Text.Json;
using aspcore_watchshop.EF;
using aspcore_watchshop.Interfaces;
using aspcore_watchshop.Models;
using aspcore_watchshop.ViewModels;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;

namespace aspcore_watchshop.Hepler {
    public class DataHelper {
        private static readonly DataHelper _instance = new DataHelper ();
        public static DataHelper Instance { get { return _instance; } }
        private DataHelper () { }

        public IMapper Mapper { get; set; }

        public List<T> LsObjectMapperTo<T, V> (List<V> obj) {
            if (obj == null || obj.Count == 0) return null;
            List<T> result = new List<T> ();
            foreach (var item in obj)
                result.Add (Mapper.Map<T> (item));
            return result;
        }

        public T ObjectMapperTo<T, V> (V obj) {
            if (obj == null) return default (T);
            return Mapper.Map<T> (obj);
        }

        public Tuple<string, int> ConvertValue (double value) {
            string suffixes = "đ";
            if (!(Convert.ToInt32 (value) == value)) {
                suffixes = "%";
                value *= 100;
            }
            return new Tuple<string, int> (suffixes, (int) value);
        }

        public List<ProductVM> Products (watchContext context, IMemoryCache cache, IProductModel productModel, IPromModel promModel) {
            List<ProductVM> products = CacheHelper.Get<List<ProductVM>> (cache, Changed.PRODUCT);
            if (products == null || products.Count == 0) {
                products = productModel.GetListVMs (context, 0);
                productModel.AddDiscount (ref products, promModel.GetListVMProducts (context));
                if (products != null)
                    CacheHelper.Set (cache, products, Changed.PRODUCT);
            }
            return products;
        }

        public T ParserJsonTo<T> (string target) {
            return JsonSerializer.Deserialize<T> (target, new JsonSerializerOptions {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }

        public string ParserObjToJson (object target) {
            return JsonSerializer.Serialize (target, new JsonSerializerOptions {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }

    }
}