using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CacheExtention.Caching
{
    public class CacheKey
    {
        //public static string GenCacheKey(string cacheName, params object[] args)
        //{
        //    if (args != null && args.Length > 0)
        //    {
        //        string separator = "_";
        //        string cacheKey = cacheName;
        //        return args.Aggregate(cacheKey, (current, param) => current + (separator + (param.GetType() == typeof(string) ? SecurityProcess.MD5Hash(param.ToString()) : param)));
        //    }
        //    else return cacheName;
        //}
    }
}
