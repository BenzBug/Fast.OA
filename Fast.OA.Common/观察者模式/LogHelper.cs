using Fast.OA.Common.Log4Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fast.OA.Common
{
    //定义一个委托
    //[委托方式注释]public delegate void WriteLogDel(string str);

    public class LogHelper
    {
        public static Queue<string> ExceptionStringQueue = new Queue<string>();
        //[委托方式注释]public static WriteLogDel WriteLogDelFunc; 
        public static List<ILogWriter> LogWriterList = new List<ILogWriter>();

        static LogHelper()
        {

            //[委托方式注释]WriteLogDelFunc = new WriteLogDel(WriteLogToFile);
            //[委托方式注释]WriteLogDelFunc += WriteLogToMongodb;
            //LogWriterList.Add(new TextFileWriter());
            //LogWriterList.Add(new SqlServerWriter());
            LogWriterList.Add(new Log4NetWriter());
            //把从队列中获取错误消息写入日志文件
            //此函数在第一次执行LogHelper之前调用此静态构造函数一次，只执行一次
            ThreadPool.QueueUserWorkItem(o =>
            {
                lock (ExceptionStringQueue)
                {
                    if (ExceptionStringQueue.Count > 0)
                    {
                        //出队列
                        string str = ExceptionStringQueue.Dequeue();
                        //把异常信息写入日志文件里面去
                        //变化点：有可能写道日志文件/数据库/有可能两个地方都写
                        //观察者模式：

                        //执行委托方法。把异常信息写入委托里面去
                        //[委托方式注释]WriteLogDelFunc(str);

                        //ILogWriter writer = new TextFileWriter();
                        //writer.WriteLogInfo(str);
                        foreach (var logWriter in LogWriterList)
                        {
                            logWriter.WriteLogInfo(str);
                        }
                    }
                    else
                    {
                        Thread.Sleep(30);
                    }
                }
            });

        }
        #region 注册方法
        ///// <summary>
        ///// 写入日志文件
        ///// </summary>
        ///// <param name="text"></param>
        //public static void WriteLogToFile (string text)
        //{

        //}
        ///// <summary>
        ///// 写入mongodb
        ///// </summary>
        ///// <param name="text"></param>
        //public static void WriteLogToMongodb (string text)
        //{

        //}

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
