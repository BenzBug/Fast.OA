using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fast.OA.IDAL
{
    public partial interface IDbSession
    {
        //IAdminInfoDal AdminInfoDal { get; }

        int SaveChanges();
    }
}
