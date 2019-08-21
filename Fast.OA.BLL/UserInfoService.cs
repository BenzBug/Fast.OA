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
    public partial class UserInfoService : BaseService<UserInfo>, IUserInfoService
    {
        //IAdminInfoDal adminInfoDal = new AdminInfoDal();
        //IAdminInfoDal adminInfoDal = StaticDALFactory.GetAdminInfoDal();
        //IDbSession dbSession = new DbSession();

        //public override void SetCurrentDal()
        //{
        //    CurrentDal = DbSession.AdminInfoDal;
        //}
        private IDbSession dbSession = DbSessionFactory.GetCurrentDbSession();
        public UserInfo Add(UserInfo admin)
        {
            #region MyRegion
            //return adminInfoDal.Add(admin);
            //return DbSession.AdminInfoDal.Add(admin); 
            #endregion

            dbSession.UserInfoDal.Add(admin);
            //dbSession.AdminInfoDal.Update(admin);
            //dbSession.AdminInfoDal.Delete(admin);

            dbSession.SaveChanges();


            return admin;
        }

        #region 多条件查询
        public IQueryable<UserInfo> LogPageData(UserQueryParam userQueryParam)
        {
            short normalFlag = (short)Model.Enum.DelFlagEnum.Normal;
            var temp = dbSession.UserInfoDal.GetEntities(u => u.delFlag == normalFlag);

            //过滤
            if (!string.IsNullOrEmpty(userQueryParam.SchCode))
            {
                temp = temp.Where(u => u.userCode.Contains(userQueryParam.SchCode)).AsQueryable();
            }
            if (!string.IsNullOrEmpty(userQueryParam.SchName))
            {
                temp = temp.Where(u => u.userName.Contains(userQueryParam.SchName)).AsQueryable();
            }

            userQueryParam.Total = temp.Count();

            //分页
            return temp.OrderBy(u => u.Id)
                .Skip(userQueryParam.PageSize * (userQueryParam.PageIndex - 1))
                .Take(userQueryParam.PageSize).AsQueryable();
        } 
        #endregion
    }
}
