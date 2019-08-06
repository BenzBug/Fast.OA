using Memcached.ClientLibrary;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fast.OA.Common.Cache
{
    public  class RedisWriter : ICacheWriter
    {
        private RedisClient client=new RedisClient("127.0.0.1", 6379);
        private RedisClient readClient1 = new RedisClient("192.168.1.100", 6381);
        private RedisClient readClient2 = new RedisClient("192.168.1.100", 6382);

        public RedisWriter()
        {
        }

        public  void AddCache(string key, object value)
        {
            client.Add(key,value);
        }

        public void AddCache(string key, object value, DateTime expDate)
        {
            client.Add(key,value,expDate);
        }

        public object GetCache(string key)
        {
            return client.Get<object>(key);
        }

        public T GetCache<T>(string key)
        {
            return client.Get<T>(key);
        }

        public void SetCache(string key, object value)
        {
            client.Set(key,value);
        }

        public void SetCache(string key, object value, DateTime expDate)
        {
            client.Set(key,value, expDate);
        }
    }
}
