using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 修改App内容
{
    class Program
    {
        static void Main(string[] args)
        {
            //string videopath= AppConfigHelper.GetValueByKey("videopath");
            //< add key = "videopath" value = "http://10.68.4.97:8011/" />
            string key = "videopath";
            string value = "http://10.68.4.97:8011/";
            AppConfigHelper.ModifyAppSettings(key,value);
            string videopath = AppConfigHelper.GetValueByKey("videopath");
            Console.WriteLine(videopath);
            Console.ReadKey();
        }
    }


}
