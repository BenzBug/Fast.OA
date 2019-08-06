using Fast.OA.BLL;
using Fast.OA.IBLL;
using Fast.OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fast.OA.UI.Portal.Controllers
{
    public class ActionInfoController : BaseController
    {
        IUserInfoService AdminInfoService = new UserInfoService();
        // GET: Admin
        public ActionResult Index()
        {
            ViewData.Model= AdminInfoService.GetEntities(u=>true);
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(UserInfo UserInfo)
        {
            if (ModelState.IsValid)
            {
                AdminInfoService.Add(UserInfo);
            }
            return RedirectToAction("Index");
        }
    }
}