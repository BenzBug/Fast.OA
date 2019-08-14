using Fast.OA.IBLL;
using Fast.OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fast.OA.UI.Portal.Controllers
{
    public class UserInfoController : Controller
    {
        IUserInfoService UserInfoService = new UserInfoService();
        // GET: UserInfo
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetAllUserInfos()
        {
            //easyui table在初始化的时候自动传入两个参数
            int pageSize = int.Parse(Request["rows"]??"10");
            int pageIndex = int.Parse(Request["page"]??"1");
            int total = 0;

            short delflagNormal = (short)Model.Enum.DelFlagEnum.Normal;

            //拿到当前页数据
            var pagedata = UserInfoService.GetPageEntities(pageSize, pageIndex, out total,
                                                u => u.delFlag == delflagNormal, u => u.Id, true)
                                                .Select(u => new { u.Id,u.userCode,u.userName,u.pwd,u.showName,
                                                        u.remark,u.createTime,u.delFlag,u.updateTime});
            var data = new { total = total, rows = pagedata.ToList() };

            return Json(data,JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public ActionResult Add(UserInfo userInfo)
        {
            var userData = UserInfoService.GetEntities(u=>u.userCode==userInfo.userCode&&u.delFlag==0);
            if (userData.ToList().Count()>0)
            {
                //ViewBag.result = "用户编码已经存在，请重新输入！";
                //return View();
                return Content("用户编码已经存在，请重新输入！");
            }
            userInfo.createTime = DateTime.Now;
            userInfo.updateTime = DateTime.Now;
            userInfo.delFlag = (short)Model.Enum.DelFlagEnum.Normal;

            UserInfoService.Add(userInfo);
            //ViewBag.result = "ok";
            return Content("ok");
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return Content("请选中要删除的数据");
            }

            string[] strIds = ids.Split(',');
            List<int> idList = new List<int>();
            foreach (var id in strIds)
            {
                if (string.IsNullOrEmpty(id))
                {
                    continue;
                }
                idList.Add(int.Parse(id));
            }

            //真删除
            //UserInfoService.DeleteList(idList);

            //逻辑删除
            UserInfoService.DeleteListByLogical(idList);
            return Content("ok");
        }

        public ActionResult Edit(int id)
        {
            //ViewBag.Model = UserInfoService.GetEntities(u => u.Id == id).FirstOrDefault();
            ViewData.Model = UserInfoService.GetEntities(u => u.Id == id).FirstOrDefault();
            return View();
        }

        [HttpPost]
        public ActionResult Edit(UserInfo userInfo)
        {
            UserInfoService.Update(userInfo);
            return Content("ok");
        }
    }
}