using Fast.OA.IDAL;
using System.Reflection;

namespace Fast.OA.DALFactory
{
    public partial class StaticDALFactory
    {
        public static string assemblyName = System.Configuration.ConfigurationManager.AppSettings["DalAssemblyName"];
        //public static IAdminInfoDal GetAdminInfoDal()
        //{
        //    //return new AdminInfoDal();

        //    //把上面个的new去掉，改一个配置那么创建实例就发生变化，不需要改代码   assemblyName +   Fast.OA.EFDAL.
        //    return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".AdminInfoDal") as IAdminInfoDal;
        //}
    }
}
