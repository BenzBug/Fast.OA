using Fast.OA.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Fast.OA.EFDAL
{
    /// <summary>
    /// 职责：封装所有的Dal的公共的crud的方法
    /// 类的职责一定要单一
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseDal<T> where T:BaseEntity,new()
    {
        //FAST_V1Entities db = new FAST_V1Entities();

        //依赖抽象编程
        public DbContext db
        {
            get { return DbContextFactory.GetCurrentDbContext(); }
        }
        #region 查询
        public IQueryable<T> GetEntities(Expression<Func<T, bool>> wherelambda)
        {
            return db.Set<T>().Where(wherelambda).AsQueryable();
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="total"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderByLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public IQueryable<T> GetPageEntities<S>(int pageSize, int pageIndex, out int total,
                                        Expression<Func<T, bool>> whereLambda,
                                        Expression<Func<T, S>> orderByLambda,
                                        bool isAsc)
        {
            total = db.Set<T>().Where(whereLambda).Count();

            if (isAsc)
            {
                var temp = db.Set<T>().Where(whereLambda)
                .OrderBy<T, S>(orderByLambda)
                .Skip(pageSize * (pageIndex - 1))
                .Take(pageSize).AsQueryable();
                return temp;
            }
            else
            {
                var temp = db.Set<T>().Where(whereLambda)
                .OrderByDescending<T, S>(orderByLambda)
                .Skip(pageSize * (pageIndex - 1))
                .Take(pageSize).AsQueryable();
                return temp;
            }

        }
        #endregion


        #region CUD
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public  T Add(T entity)
        {
            db.Set<T>().Add(entity);
            //db.SaveChanges();
            return entity;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(T entity)
        {
            db.Entry(entity).State = EntityState.Modified;

            //return db.SaveChanges()> 0;
            return true;
        }

        /// <summary>
        /// 真删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Delete(T entity)
        {
            db.Entry(entity).State = EntityState.Deleted;

            return db.SaveChanges() > 0;

            //db.Entry(entity)

            ////return true;

        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            var entity = db.Set<T>().Find(id);

            db.Set<T>().Remove(entity);
            return true;
        }

        public int deleteListByLogical(List<int> ids)
        {
            foreach (var id in ids)
            {
                var entity = db.Set<T>().Find(id);
                #region 反射方式给delFlag字段赋值
                //db.Entry(entity).Property("DelFlag").CurrentValue = (short)Model.Enum.DelFlagEnum.Deleted;
                //db.Entry(entity).Property("DelFlag").IsModified = true; 
                #endregion
                entity.delFlag = (short)Model.Enum.DelFlagEnum.Deleted;
                db.Entry(entity).State = EntityState.Modified;

            }
            return ids.Count;
        }
        #endregion
    }
}
