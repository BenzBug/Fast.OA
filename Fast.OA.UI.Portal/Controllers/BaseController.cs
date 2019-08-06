using Fast.OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fast.OA.UI.Portal.Controllers
{
    public class BaseController : Controller
    {
        public bool isCheckLoginuser = true;

        public UserInfo userLoginInfo { get; set; }

        //在当前的控制器里面所有的方法执行之前。都先执行此代码 
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            //TODO:测试结束后删除return
            //return; 

            if (isCheckLoginuser)
            {
                //校验用户是否已登录

                #region 使用mm+cookie代替session(已改为Redis方式)
                //从缓存中拿到当前的登录的用户信息。
                if (Request.Cookies["userLoginId"].Value == null)
                {
                    filterContext.Result = new RedirectResult("/UserLogin/Index");
                    return;
                }
                string userGuid = Request.Cookies["userLoginId"].Value;
                //Admin adminInfoCh = Common.Cache.CacheHelper.GetCache(userGuid) as Admin;
                UserInfo userInfoCh = Common.Cache.CacheHelper.GetCache<UserInfo>(userGuid) as UserInfo;
                if (userInfoCh == null)
                {
                    //用户长时间不操作，。超时。
                    filterContext.Result = new RedirectResult("/UserLogin/Index");
                    return;
                }
                userLoginInfo = userInfoCh;
                //滑动窗口机制，延长（重置）过期时间
                Common.Cache.CacheHelper.SetCache(userGuid, userLoginInfo, DateTime.Now.AddMinutes(20)); 
                #endregion


                #region 用Memcache代替Session的做法
                //if (filterContext.HttpContext.Session["loginUser"] == null)
                //{
                //    filterContext.HttpContext.Response.Redirect("/UserLogin/Index");
                //}
                //else
                //{
                //    adminInfo = filterContext.HttpContext.Session["loginUser"] as Admin;
                //} 
                #endregion
            }

        }
    }
}