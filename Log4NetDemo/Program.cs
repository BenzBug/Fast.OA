using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log4NetDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //从配置文件读取log4Net的配置，然后进行一个初始化工作。
            log4net.Config.XmlConfigurator.Configure();

            ILog logWriter = log4net.LogManager.GetLogger("DemoWriter");

            logWriter.Debug("调试级别的日志消息");
            logWriter.Error("错误级别的日志消息");
        }
    }
}
