using Fast.OA.EFDAL;
using Fast.OA.IDAL;

namespace Fast.OA.DALFactory
{
    public partial class DbSession:IDbSession
    {
        #region 简单工厂或抽象工厂部分
        //public  IAdminInfoDal AdminInfoDal
        //{
        //    get { return StaticDALFactory.GetAdminInfoDal(); }
        //}
        #endregion

        /// <summary>
        /// 拿到当前EF上下文,然后 把修改实体进行一个整体提交
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
            return DbContextFactory.GetCurrentDbContext().SaveChanges();
        }
    }
}
