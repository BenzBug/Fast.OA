using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fast.OA.Common
{
    //定义一个委托
    public delegate void WriteLogDel_weituo(string str);

    public class LogHelperDel
    {
        public static Queue<string> ExceptionStringQueue = new Queue<string>();
        public static WriteLogDel_weituo WriteLogDelFunc; 
        static LogHelperDel()
        {
            
            WriteLogDelFunc = new WriteLogDel_weituo(WriteLogToFile);
            WriteLogDelFunc += WriteLogToMongodb;

            //把从队列中获取错误消息写入日志文件
            //此函数在第一次执行LogHelper之前调用此静态构造函数一次，只执行一次
            ThreadPool.QueueUserWorkItem(o =>
            {
                lock (ExceptionStringQueue)
                {
                    //出队列
                    string str = ExceptionStringQueue.Dequeue();
                    //把异常信息写入日志文件里面去
                    //变化点：有可能写道日志文件/数据库/有可能两个地方都写
                    //观察者模式：

                    //执行委托方法。把异常信息写入委托里面去
                    WriteLogDelFunc(str);
                }
            });

        }
        #region 注册方法
        /// <summary>
        /// 写入日志文件
        /// </summary>
        /// <param name="text"></param>
        public static void WriteLogToFile (string text)
        {

        }
        /// <summary>
        /// 写入mongodb
        /// </summary>
        /// <param name="text"></param>
        public static void WriteLogToMongodb (string text)
        {

        }

        #endregion

        public static void WriteLog(string exceptionText)
        {
            lock(ExceptionStringQueue)
            {
                //入队列
                ExceptionStringQueue.Enqueue(exceptionText);
            }
        } 
    }
}
