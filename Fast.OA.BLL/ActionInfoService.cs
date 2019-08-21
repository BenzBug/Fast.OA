using Fast.OA.BLL;
using Fast.OA.DALFactory;
using Fast.OA.EFDAL;
using Fast.OA.IBLL;
using Fast.OA.IDAL;
using Fast.OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fast.OA.Model.Param;

namespace Fast.OA.IBLL
{
    public partial class ActionInfoService : BaseService<ActionInfo>, IActionInfoService
    {
        //IAdminInfoDal adminInfoDal = new AdminInfoDal();
        //IAdminInfoDal adminInfoDal = StaticDALFactory.GetAdminInfoDal();
        //IDbSession dbSession = new DbSession();

        //public override void SetCurrentDal()
        //{
        //    CurrentDal = DbSession.AdminInfoDal;
        //}
        private IDbSession dbSession = DbSessionFactory.GetCurrentDbSession();
        public ActionInfo Add(ActionInfo action)
        {
            #region MyRegion
            //return adminInfoDal.Add(admin);
            //return DbSession.AdminInfoDal.Add(admin); 
            #endregion
            dbSession.ActionInfoDal.Add(action);
            //dbSession.AdminInfoDal.Update(admin);
            //dbSession.AdminInfoDal.Delete(admin);
            dbSession.SaveChanges();

            return action;
        }

        #region 多条件查询
        public IQueryable<ActionInfo> LogPageData(ActionQueryParam actionQueryParam)
        {
            short normalFlag = (short)Model.Enum.DelFlagEnum.Normal;
            var temp = dbSession.ActionInfoDal.GetEntities(u => u.delFlag == normalFlag);

            //过滤
            if (!string.IsNullOrEmpty(actionQueryParam.SchName))
            {
                temp = temp.Where(u => u.ActionName.Contains(actionQueryParam.SchName)).AsQueryable();
            }

            actionQueryParam.Total = temp.Count();

            //分页
            return temp.OrderBy(u => u.Id)
                .Skip(actionQueryParam.PageSize * (actionQueryParam.PageIndex - 1))
                .Take(actionQueryParam.PageSize).AsQueryable();
        } 
        #endregion
    }
}
