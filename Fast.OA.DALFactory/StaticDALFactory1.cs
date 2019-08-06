/******************************************
*创建人：<Name>
*创建时间：2019-08-04 14:05:30
*说明：<Function>
*版权所有：<奔驰毛毛虫>
*******************************************/
using Fast.OA.EFDAL;
using Fast.OA.IDAL;
using System.Reflection;

namespace Fast.OA.DALFactory
{
	public partial class StaticDALFactory
    {
 
        public static IActionInfoDal GetActionInfoDal()
        {
            return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".ActionInfoDal") as IActionInfoDal;
        }
 
        public static IR_UserInfo_ActionInfoDal GetR_UserInfo_ActionInfoDal()
        {
            return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".R_UserInfo_ActionInfoDal") as IR_UserInfo_ActionInfoDal;
        }
 
        public static IRoleInfoDal GetRoleInfoDal()
        {
            return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".RoleInfoDal") as IRoleInfoDal;
        }
 
        public static IUserInfoDal GetUserInfoDal()
        {
            return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".UserInfoDal") as IUserInfoDal;
        }
 
        public static IUserInfoExtDal GetUserInfoExtDal()
        {
            return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".UserInfoExtDal") as IUserInfoExtDal;
        }
	}
}