using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fast.OA.UI.Portal.Models
{
    public class MyExceptionFilterAttribute: HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);

            //自己处理异常

            //直接把错误信息写道日志文件里面去

            Common.LogHelper.WriteLog(filterContext.Exception.ToString());
        }
    }
}