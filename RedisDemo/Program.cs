using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Redis;

namespace RedisDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new RedisClient("127.0.0.1", 6379);

            var readClient1 = new RedisClient("192.168.1.100", 6381);
            var readClient2 = new RedisClient("192.168.1.100", 6382);

            //最后一个参数为我们排序的依据
            #region Redis 支持排序集合
            //var s = client.AddItemToSortedSet("12", "百度", 400);

            //client.AddItemToSortedSet("12", "谷歌", 300);
            //client.AddItemToSortedSet("12", "阿里", 200);
            //client.AddItemToSortedSet("12", "新浪", 100);
            //client.AddItemToSortedSet("12", "人人", 500);

            ////升序获取最一个值:"新浪"
            //var list = client.GetRangeFromSortedSet("12", 0,2);

            //foreach (var item in list)
            //{
            //    Console.WriteLine(item);
            //}

            ////降序获取最一个值:"人人"
            //list = client.GetRangeFromSortedSetDesc("12", 0, 0);

            //foreach (var item in list)
            //{
            //    Console.WriteLine(item);
            //}
            #endregion


            #region redis最基本的功能  ---分布式缓存

            client.Add("ssss", "ssss", DateTime.Now.AddMinutes(20));
            //public T Get<T>(string key);
            string strHead = client.Get<string>("ssss");
            Console.WriteLine(strHead);
            Console.WriteLine("-----------------------------------------------");

            List<string> b=client.GetAllKeys();
            foreach (var item in b)
            {
                Console.WriteLine(item);
            }
            #endregion
            Console.WriteLine("-----------------------------------------------");

            #region 数据结构：队列  + 栈

            client.EnqueueItemOnList("LogQueue", "错误....");
            client.EnqueueItemOnList("LogQueue", "错误2....");

            string str = client.DequeueItemFromList("LogQueue");
            Console.WriteLine(str);
            Console.WriteLine("-----------------------------------------------");

            client.PushItemToList("fz", "1");
            client.PushItemToList("fz", "2");

            Console.WriteLine(client.PopItemFromList("fz"));

            #endregion

            Console.ReadKey();

        }
    }
}
