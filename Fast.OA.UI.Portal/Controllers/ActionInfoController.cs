using Fast.OA.BLL;
using Fast.OA.IBLL;
using Fast.OA.Model;
using Fast.OA.Model.Param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fast.OA.UI.Portal.Controllers
{
    public class ActionInfoController : BaseController
    {
        IActionInfoService ActionInfoService = new ActionInfoService();
        short delflagNormal = (short)Model.Enum.DelFlagEnum.Normal;
        // GET: Admin
        public ActionResult Index()
        {
            ViewData.Model= ActionInfoService.GetEntities(u=>true);
            return View();
        }

        public ActionResult GetAllActionInfos()
        {
            //easyui: table在初始化时候自动发送以下两个参数值。
            int pageSize = int.Parse(Request["rows"] ?? "10");
            int pageIndex = int.Parse(Request["page"] ?? "1");
            int total = 0;
            var temp = ActionInfoService.GetPageEntities(pageSize,pageIndex,out total,
                                                        u=>u.delFlag== delflagNormal,u=>u.Id,true);

            var tempData = temp.Select(u => new
            {
                u.Id,
                u.IsMenu,
                u.Url,
                u.Sort,
                u.HttpMethd,
                u.MenuIcon,
                u.ActionName,
                u.remark,
                u.createTime,
                u.delFlag,
                u.updateTime
            });
            
            var data = new { total = total, rows = tempData.ToList() };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="ActionInfo"></param>
        /// <returns></returns>
        public ActionResult Add(ActionInfo ActionInfo)
        {
            var userData = ActionInfoService.GetEntities(u => u.ActionName == ActionInfo.ActionName && u.delFlag == 0);
            if (userData.ToList().Count() > 0)
            {
                //ViewBag.result = "用户编码已经存在，请重新输入！";
                //return View();
                return Content("用户编码已经存在，请重新输入！");
            }
            ActionInfo.createTime = DateTime.Now;
            ActionInfo.updateTime = DateTime.Now;
            ActionInfo.delFlag = (short)Model.Enum.DelFlagEnum.Normal;

            ActionInfoService.Add(ActionInfo);
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
            //ActionInfoService.DeleteList(idList);

            //逻辑删除
            ActionInfoService.DeleteListByLogical(idList);
            return Content("ok");
        }

        public ActionResult Edit(int id)
        {
            ViewData.Model = ActionInfoService.GetEntities(u => u.Id == id).FirstOrDefault();
            return View();
        }

        [HttpPost]
        public ActionResult Edit(ActionInfo ActionInfo)
        {
            ActionInfoService.Update(ActionInfo);
            return Content("ok");
        }
    }
}