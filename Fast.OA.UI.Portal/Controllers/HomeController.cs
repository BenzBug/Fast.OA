using Fast.OA.IBLL;
using Fast.OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fast.OA.UI.Portal.Controllers
{
    public class HomeController : BaseController
    {   
        short delFlagNormal =(short) Model.Enum.DelFlagEnum.Normal;
        public IUserInfoService UserInfoService = new UserInfoService();
        public IActionInfoService ActionInfoService = new ActionInfoService();
        public ActionResult Index()
        {
            //this.adminInfo.UserCode
            ViewBag.AllMenu = LoadUserMenu();

            return View();
        }

        public List<ActionInfo> LoadUserMenu()
        {
            //拿到当前用户
            int userID = this.userLoginInfo.Id;
            var user = UserInfoService.GetEntities(u=>u.Id== userID).FirstOrDefault();

            //拿到当前用户所有的权限【必须是菜单类型权限。】
            var allRole = user.RoleInfo;

            //把用户对应的所有的角色关联权限的id都拿出来了。
            var allRoleActionIds = (from r in allRole
                                    from a in r.ActionInfo
                                    select a.Id).ToList();
            //用户直接拒绝的权限。
            var allDenyActionIds = (from r in user.R_UserInfo_ActionInfo
                                    where r.HasPermission == false
                                    select r.ActionInfoID).ToList();

            //角色权限 -  特殊拒绝权限
            var allActionIds = (from a in allRoleActionIds
                                where !allDenyActionIds.Contains(a)
                                select a).ToList();
            //特殊直接允许权限
            var allUserActionIds = (from t in user.R_UserInfo_ActionInfo
                                    where t.HasPermission == true
                                    select t.ActionInfoID).ToList();

            //把当前用户的所有的权限拿到。
            allActionIds.AddRange(allUserActionIds.AsEnumerable());
            allActionIds = allActionIds.Distinct().ToList();//去重操作。

            var actionList = ActionInfoService.GetEntities(a=> allActionIds.Contains(a.Id)&&a.IsMenu==true&&a.delFlag==delFlagNormal).ToList();

            return actionList;
        }
    }
}