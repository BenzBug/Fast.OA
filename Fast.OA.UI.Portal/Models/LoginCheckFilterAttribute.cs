using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fast.OA.UI.Portal.Models
{
    public class LoginCheckFilterAttribute:ActionFilterAttribute
    {
        ///第一种方法校验用户是否已经登录
        /// <summary>
        /// 是否需要验证用户登录
        /// </summary>
        public bool isCheck { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (isCheck)
            {
                //校验用户是否已登录
                if (filterContext.HttpContext.Session["loginUser"] ==null)
                {
                    filterContext.HttpContext.Response.Redirect("/UserLogin/Index");
                }
            }
        }
    }
}