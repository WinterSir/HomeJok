using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HomeJok.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, new()
    {
        public BaseRepository()
        {
        }

        public async Task<T> Query(Expression<Func<T, bool>> predicate = null)
        {
            T ret = await Task.Run(() =>
            {
                return new T();
            });
            return ret;
        }

        public async Task<List<T>> Query()
        {
            List<T> ret = await Task.Run(() =>
            {
                List<T> temp = new List<T>();
                for (int i = 0; i < 10; i++)
                {
                    temp.Add(new T());
                }
                return temp;
            });
            return ret;
        }
        public async Task<bool> Insert(T t)
        {
            return true;
        }
        public async Task<T> InsertReturnEntity(T t)
        {
            return new T();
        }
        public async Task<bool> Update(T t)
        {
            return true;
        }
        public async Task<bool> Delete(Expression<Func<T, bool>> predicate = null)
        {
            return true;
        }
    }
}
