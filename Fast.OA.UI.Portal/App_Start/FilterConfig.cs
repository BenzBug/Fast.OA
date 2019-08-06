using Fast.OA.UI.Portal.Models;
using System.Web;
using System.Web.Mvc;

namespace Fast.OA.UI.Portal
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());

            //ActionFilter ResultFilter

            //第三种Filter:异常过滤器
            //把自己的过滤器注册到全局过滤器中
            filters.Add(new MyExceptionFilterAttribute());

            ////注册过滤器，验证每个页面用户是否登录
            //filters.Add(new LoginCheckFilterAttribute() { isCheck=true});
        }
    }
}

