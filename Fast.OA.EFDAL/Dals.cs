 

/******************************************
*创建人：<Name>
*创建时间：2019-08-04 22:42:58
*说明：<Function>
*版权所有：<奔驰毛毛虫>
*******************************************/
using Fast.OA.EFDAL;
using Fast.OA.IDAL;
using Fast.OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fast.OA.IDAL
{
 
    public partial class ActionInfoDal:BaseDal<ActionInfo>,IActionInfoDal
	{
	}
 
    public partial class R_UserInfo_ActionInfoDal:BaseDal<R_UserInfo_ActionInfo>,IR_UserInfo_ActionInfoDal
	{
	}
 
    public partial class RoleInfoDal:BaseDal<RoleInfo>,IRoleInfoDal
	{
	}
 
    public partial class UserInfoDal:BaseDal<UserInfo>,IUserInfoDal
	{
	}
 
    public partial class UserInfoExtDal:BaseDal<UserInfoExt>,IUserInfoExtDal
	{
	}

}

