using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Fast.OA.Common.Cache
{
    public class HttpRuntimeCacheWriter : ICacheWriter
    {
        public void AddCache(string key, object value)
        {
            HttpRuntime.Cache.Insert(key,value);
        }

        public void AddCache(string key, object value, DateTime expDate)
        {
            HttpRuntime.Cache.Insert(key,value,null,expDate,TimeSpan.Zero);
        }

        public object GetCache(string key)
        {
            return HttpRuntime.Cache[key];
        }

        public T GetCache<T>(string key)
        {
            return (T)HttpRuntime.Cache[key];
        }

        public void SetCache(string key, object value)
        {
            HttpRuntime.Cache.Remove(key);
            AddCache( key,  value);
        }

        public void SetCache(string key, object value, DateTime expDate)
        {
            HttpRuntime.Cache.Remove(key);
            AddCache(key, value, expDate);
        }
    }
}
