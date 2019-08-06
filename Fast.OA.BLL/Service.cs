/******************************************
*创建人：<Name>
*创建时间：2019-08-04 14:05:46
*说明：<Function>
*版权所有：<奔驰毛毛虫>
*******************************************/
using Fast.OA.BLL;
using Fast.OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fast.OA.IBLL
{
 
	
	public partial class ActionInfoService:BaseService<ActionInfo>,IActionInfoService
    {
		public override void SetCurrentDal()
        {
            CurrentDal = DbSession.ActionInfoDal;
        } 
    }
 
	
	public partial class R_UserInfo_ActionInfoService:BaseService<R_UserInfo_ActionInfo>,IR_UserInfo_ActionInfoService
    {
		public override void SetCurrentDal()
        {
            CurrentDal = DbSession.R_UserInfo_ActionInfoDal;
        } 
    }
 
	
	public partial class RoleInfoService:BaseService<RoleInfo>,IRoleInfoService
    {
		public override void SetCurrentDal()
        {
            CurrentDal = DbSession.RoleInfoDal;
        } 
    }
 
	
	public partial class UserInfoService:BaseService<UserInfo>,IUserInfoService
    {
		public override void SetCurrentDal()
        {
            CurrentDal = DbSession.UserInfoDal;
        } 
    }
 
	
	public partial class UserInfoExtService:BaseService<UserInfoExt>,IUserInfoExtService
    {
		public override void SetCurrentDal()
        {
            CurrentDal = DbSession.UserInfoExtDal;
        } 
    }
}