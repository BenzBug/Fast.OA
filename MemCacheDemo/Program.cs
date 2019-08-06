using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemCacheDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            ////存入key为a，value为123的一个缓存
            //AMemcached.cache.Add("a", "123");
            ////读出key为a的缓存值
            //var s = AMemcached.cache.Get("a");
            ////输出
            //Console.WriteLine(s);
            //Console.ReadKey();

            //存入key为a，value为123的一个缓存
            new AMemcached("me").cache.Add("b", 123);
            //读出key为a的缓存值
            var s = new AMemcached("me").cache.Get("b");
            //输出
            Console.WriteLine(s);
            Console.ReadKey();

        }
    }
}
