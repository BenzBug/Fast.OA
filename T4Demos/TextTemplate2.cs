 

using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{

   
    
        public partial interface IAdminService:IBaseService<Admin>
    {
    }
    
        public partial interface ICustomerService:IBaseService<Customer>
    {
    }
    
        public partial interface ISysRoleService:IBaseService<SysRole>
    {
    }

}