/******************************************
*创建人：<Name>
*创建时间：2019-08-04 14:05:25
*说明：<Function>
*版权所有：<奔驰毛毛虫>
*******************************************/
using Fast.OA.EFDAL;
using Fast.OA.IDAL;

namespace Fast.OA.DALFactory
{
	public partial class DbSession:IDbSession
    {
 
        public  IActionInfoDal ActionInfoDal
        {
            get { return StaticDALFactory.GetActionInfoDal(); }
        }
 
        public  IR_UserInfo_ActionInfoDal R_UserInfo_ActionInfoDal
        {
            get { return StaticDALFactory.GetR_UserInfo_ActionInfoDal(); }
        }
 
        public  IRoleInfoDal RoleInfoDal
        {
            get { return StaticDALFactory.GetRoleInfoDal(); }
        }
 
        public  IUserInfoDal UserInfoDal
        {
            get { return StaticDALFactory.GetUserInfoDal(); }
        }
 
        public  IUserInfoExtDal UserInfoExtDal
        {
            get { return StaticDALFactory.GetUserInfoExtDal(); }
        }
		
	}
}