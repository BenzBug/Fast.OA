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


    }
}
