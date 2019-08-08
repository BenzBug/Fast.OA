using Fast.OA.BLL;
using Fast.OA.Common;
using Fast.OA.IBLL;
using Fast.OA.Model.Enum;
using Fast.OA.UI.Portal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fast.OA.UI.Portal.Controllers
{
    //[LoginCheckFilter(isCheck=false)]
    public class UserLoginController : BaseController
    {
        public UserLoginController()
        {
            this.isCheckLoginuser = false;
        }
        IUserInfoService adminInfoService = new UserInfoService();    

        // GET: UserLogin
        public ActionResult Index()
        {
            return View("IndexLTE_Login");
        }

        public ActionResult ShowVCode()
        {

            //throw new Exception("test1111111111111");

            Common.ValidateCode validateCode = new ValidateCode();
            string vCode = validateCode.CreateValidateCode(4);

            Session["VCode"] = vCode; 

            byte[] imgBytes = validateCode.CreateValidateGraphic(vCode);
            return File(imgBytes,@"image/jpeg");

        }

        public ActionResult ProcessLogin()
        {
            //第一步：处理验证码。
            //拿到表单中的验证码
            #region 验证码
            string vCode = Request["vCode"];

            //拿到Session中的验证码
            string sessionCode = Session["vCode"] as string;

            Session["vCode"] = null;
            if (string.IsNullOrEmpty(sessionCode))
            {
                return Content("验证码错误！");
            }
            if (vCode!=sessionCode)
            {
                return Content("验证码错误！");
            }
            #endregion

            //第二步：处理验证用户名密码
            string name = Request["LoginCode"];
            string pwd = Request["LoginPwd"];

            short delNormal = (short)DelFlagEnum.Normal;
            var userInfo = adminInfoService.GetEntities(u => u.userCode == name && u.pwd == pwd && u.delFlag==delNormal)
                            .Select(u=>new {u.Id, u.userCode,u.pwd,u.delFlag }).FirstOrDefault();

            if (userInfo==null)
            {
                return Content("用户名密码错误！请重新输入");
            }

            //Session["loginUser"] = userInfo;

            //用memcache+cookie代替Session
            //立即分配一个标志，Guid。把标志作为mm存储数据的key，把用户对象放到mm。 把guid写到客户端cookie里面去。
            string userLoginId = Guid.NewGuid().ToString();

            //把用户的数据写到mm,怎么解决变化：写到不同地方去，可能同时写入多个地方。
            Common.Cache.CacheHelper.AddCache(userLoginId, userInfo, DateTime.Now.AddMinutes(20));

            //往客户端写入cookie
            Response.Cookies["userLoginId"].Value = userLoginId;

            //如果正确那么跳转到首页
            return Content("ok");
        }

    }
}