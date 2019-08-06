/******************************************
*创建人：<Name>
*创建时间：2019-08-04 14:05:19
*说明：<Function>
*版权所有：<奔驰毛毛虫>
*******************************************/
using Fast.OA.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fast.OA.IDAL
{
    public partial interface IDbSession
	{
	 
		
		IActionInfoDal ActionInfoDal { get; }
	 
		
		IR_UserInfo_ActionInfoDal R_UserInfo_ActionInfoDal { get; }
	 
		
		IRoleInfoDal RoleInfoDal { get; }
	 
		
		IUserInfoDal UserInfoDal { get; }
	 
		
		IUserInfoExtDal UserInfoExtDal { get; }
			
	}
}