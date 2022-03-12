using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HomeJok.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        /// <summary>
        /// 条件查询实体，返回实体
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task<T> Find(Expression<Func<T, bool>> predicate = null);
      
        /// <summary>
        /// 查询实体集合
        /// </summary>
        /// <returns></returns>
        Task<List<T>> QueryList(Expression<Func<T, bool>> predicate = null);

        /// <summary>
        /// 分页查询实体集合
        /// </summary>
        /// <returns></returns>
        Task<List<T>> QueryPagedList(int pageIndex, int pageSize, Expression<Func<T, bool>> predicate = null);

        /// <summary>
        /// 新增实体，返回结果Bool
        /// </summary>
        /// <returns></returns>
        Task<T> Insert(T t);

        /// <summary>
        /// 新增实体，返回结果Bool
        /// </summary>
        /// <returns></returns>
        Task<bool> InsertMany(IEnumerable<T> t);

        /// <summary>
        /// 修改实体，返回结果Bool
        /// </summary>
        /// <returns></returns>
        Task<bool> Update(T t);

        /// <summary>
        /// 修改实体集合，返回结果Bool
        /// </summary>
        /// <returns></returns>
        Task<bool> UpdateMany(IEnumerable<T> t);

        /// <summary>
        /// 删除实体，返回结果Bool
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task<bool> Delete(T t);

        /// <summary>
        /// 删除实体集合，返回结果Bool
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task<bool> DeleteMany(IEnumerable<T> t);
    }
}
