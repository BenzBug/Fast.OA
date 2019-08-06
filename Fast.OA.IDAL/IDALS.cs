 
/******************************************
*创建人：<Name>
*创建时间：2019-08-04 14:05:13
*说明：<Function>
*版权所有：<奔驰毛毛虫>
*******************************************/
using Fast.OA.IDAL;
using Fast.OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Fast.OA.IDAL
{
 
    public partial interface IActionInfoDal:IBaseDAL<ActionInfo>
	{
	}
 
    public partial interface IR_UserInfo_ActionInfoDal:IBaseDAL<R_UserInfo_ActionInfo>
	{
	}
 
    public partial interface IRoleInfoDal:IBaseDAL<RoleInfo>
	{
	}
 
    public partial interface IUserInfoDal:IBaseDAL<UserInfo>
	{
	}
 
    public partial interface IUserInfoExtDal:IBaseDAL<UserInfoExt>
	{
	}

}