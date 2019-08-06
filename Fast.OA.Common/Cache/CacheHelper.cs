using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fast.OA.Common.Cache
{
    public class CacheHelper
    {

        //static ICacheWriter CacheWriter = new MemCacheWriter();
        static ICacheWriter CacheWriter = new RedisWriter();
        public static void AddCache(string key ,object value ,DateTime expDate)
        {
            //往缓存写：单机，分布式   观察者模式可以。
            CacheWriter.AddCache(key,value,expDate);
        }

        public static void AddCache(string key, object value)
        {
            CacheWriter.AddCache(key, value);
        }

        public static object GetCache(string key)
        {
            return CacheWriter.GetCache(key);
        }

        public static object GetCache<T>(string key)
        {
            return CacheWriter.GetCache<T>(key);
        }

        public static void SetCache(string key, object value, DateTime expDate)
        {
            CacheWriter.SetCache(key, value, expDate);
        }

        public static void SetCache(string key, object value)
        {
            CacheWriter.SetCache(key, value);
        }
    }
}
