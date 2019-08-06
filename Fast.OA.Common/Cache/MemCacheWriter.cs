using Memcached.ClientLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fast.OA.Common.Cache
{
    public  class MemCacheWriter : ICacheWriter
    {
        private MemcachedClient memcachedClient;

        public  MemCacheWriter()
        {
            //string[] servers = { "192.168.1.100:11211", "192.168.1.118:11211" };

            string strAppMemcachedServer = System.Configuration.ConfigurationManager.AppSettings["MemcachedServerList"];

            string[] servers = strAppMemcachedServer.Split(',');

            //初始化池
            SockIOPool pool = SockIOPool.GetInstance();
            //设置服务器列表
            pool.SetServers(servers);
            //初始化时创建连接数
            pool.InitConnections = 3;
            //最小连接数
            pool.MinConnections = 3;
            //最大连接数
            pool.MaxConnections = 5;
            //socket连接的超时时间，下面设置表示不超时（单位ms），即一直保持链接状态
            pool.SocketConnectTimeout = 1000;
            //通讯的超时时间，下面设置为3秒（单位ms），.Net版本没有实现
            pool.SocketTimeout = 3000;
            //维护线程的间隔激活时间，下面设置为30秒（单位s），设置为0时表示不启用维护线程
            pool.MaintenanceSleep = 30;
            //设置SocktIO池的故障标志
            pool.Failover = true;
            //是否对TCP/IP通讯使用nalgle算法，.net版本没有实现
            pool.Nagle = false;
            // 初始化一些值并与MemcachedServer段建立连接
            pool.Initialize();
            //客户端实例
            MemcachedClient mc = new Memcached.ClientLibrary.MemcachedClient();
            mc.EnableCompression = false;

            memcachedClient = mc;
        }

        public  void AddCache(string key, object value)
        {
            memcachedClient.Add(key,value);
        }

        public void AddCache(string key, object value, DateTime expDate)
        {
            memcachedClient.Add(key,value,expDate);
        }

        public object GetCache(string key)
        {
            return memcachedClient.Get(key);
        }

        public T GetCache<T>(string key)
        {
            return (T)memcachedClient.Get(key);
        }

        public void SetCache(string key, object value)
        {
            memcachedClient.Set(key,value);
        }

        public void SetCache(string key, object value, DateTime expDate)
        {
            memcachedClient.Set(key,value, expDate);
        }
    }
}
