using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace aspcore_watchshop.Hepler
{
    public static class Helper
    {
        public static IMapper Mapper { get; set; }
        public static List<T> LsObjectToLsVM<T, V>(List<V> obj)
        {
            if (obj == null || obj.Count == 0) return null;
            List<T> result = new List<T>();
            foreach (var item in obj)
                result.Add(Mapper.Map<T>(item));
            return result;
        }

        public static T ObjectToVM<T, V>(V obj)
        {
            if (obj == null) return default(T);
            return Mapper.Map<T>(obj);
        }

        public static Tuple<string, int> ConvertValue(double value)
        {
            string suffixes = "đ";
            if (!(Convert.ToInt32(value) == value))
            {
                suffixes = "%";
                value *= 100;
            }
            return new Tuple<string, int>(suffixes, (int)value);
        }

    }
}