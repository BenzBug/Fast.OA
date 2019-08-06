using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Fast.OA.IBLL
{
    public partial interface IBaseService<T> where T :class ,new ()
    {
        IQueryable<T> GetEntities(Expression<Func<T, bool>> wherelambda);
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
        IQueryable<T> GetPageEntities<S>(int pageSize, int pageIndex, out int total,
                                       Expression<Func<T, bool>> whereLambda,
                                       Expression<Func<T, S>> orderByLambda,
                                       bool isAsc);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        T Add(T entity);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Update(T entity);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Delete(T entity);
    }
}
