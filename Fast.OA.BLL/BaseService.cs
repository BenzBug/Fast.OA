using Fast.OA.DALFactory;
using Fast.OA.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Fast.OA.BLL
{
    /// <summary>
    /// 父类要逼迫子类给父类的一个属性赋值
    /// 赋值的操作必须在父类的方法调用之前进行
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseService<T> where T:class,new ()
    {
        public IBaseDAL<T> CurrentDal { get; set; }

        public IDbSession DbSession
        {
            get
            {
                return DbSessionFactory.GetCurrentDbSession();
            }
        }

        public BaseService()
        {
            SetCurrentDal();
        }

        public abstract void SetCurrentDal();

        #region 查询
        public IQueryable<T> GetEntities(Expression<Func<T, bool>> wherelambda)
        {
            return CurrentDal.GetEntities(wherelambda);
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
            return CurrentDal.GetPageEntities(pageSize, pageIndex, out total, whereLambda, orderByLambda, isAsc);
        }
        #endregion


        #region CUD
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T Add(T entity)
        {
            CurrentDal.Add(entity);
            DbSession.SaveChanges();
            return entity;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(T entity)
        {
            CurrentDal.Update(entity);
            return DbSession.SaveChanges() > 0;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Delete(T entity)
        {
            CurrentDal.Delete(entity);
            return DbSession.SaveChanges() > 0;

        }
        #endregion

    }
}
