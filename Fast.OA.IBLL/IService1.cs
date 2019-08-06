/******************************************
*创建人：<Name>
*创建时间：2019-08-04 14:05:40
*说明：<Function>
*版权所有：<奔驰毛毛虫>
*******************************************/
using Fast.OA.IBLL;
using Fast.OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fast.OA.IBLL
{
 
	public partial interface IActionInfoService:IBaseService<ActionInfo>
    {
    }
 
	public partial interface IR_UserInfo_ActionInfoService:IBaseService<R_UserInfo_ActionInfo>
    {
    }
 
	public partial interface IRoleInfoService:IBaseService<RoleInfo>
    {
    }
 
	public partial interface IUserInfoService:IBaseService<UserInfo>
    {
    }
 
	public partial interface IUserInfoExtService:IBaseService<UserInfoExt>
    {
    }
}