using Fast.OA.IBLL;
using Fast.OA.Model;
using Fast.OA.Model.Param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fast.OA.IBLL
{
    public partial interface IUserInfoService : IBaseService<UserInfo>
    {
        IQueryable<UserInfo> LogPageData(UserQueryParam userQueryParam);
    }
}
