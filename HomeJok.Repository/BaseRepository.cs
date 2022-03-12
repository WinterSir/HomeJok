﻿using HomeJok.Repository.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HomeJok.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, new()
    {
        private readonly WinterSirContext _context;

        public BaseRepository(WinterSirContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 条件查询实体，返回实体
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task<T> Find(Expression<Func<T, bool>> predicate)
        {
            return await _context.FindAsync<T>(predicate);
        }

        /// <summary>
        /// 查询实体集合
        /// </summary>
        /// <returns></returns>
        public async Task<List<T>> QueryList(Expression<Func<T, bool>> predicate = null)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        /// <summary>
        /// 分页查询实体集合
        /// </summary>
        /// <returns></returns>
        public async Task<List<T>> QueryPagedList(int pageIndex, int pageSize, Expression<Func<T, bool>> predicate = null)
        {
            return await _context.Set<T>().Where(predicate).Skip(pageSize * (pageIndex - 1))
                                      .Take(pageSize).ToListAsync();
        }

        /// <summary>
        /// 新增实体，返回结果Bool
        /// </summary>
        /// <returns></returns>
        public async Task<T> Insert(T t)
        {
            try
            {
                await _context.AddAsync<T>(t);
                await _context.SaveChangesAsync();
                return t;
            }
            catch (Exception ex)
            {
                return t;
            }
        }

        /// <summary>
        /// 新增实体，返回结果Bool
        /// </summary>
        /// <returns></returns>
        public async Task<bool> InsertMany(IEnumerable<T> list)
        {
            try
            {
                await _context.Set<T>().AddRangeAsync(list);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 修改实体，返回结果Bool
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Update(T t)
        {
            try
            {
                _context.Set<T>().Update(t);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 修改实体集合，返回结果Bool
        /// </summary>
        /// <returns></returns>
        public async Task<bool> UpdateMany(IEnumerable<T> list)
        {
            try
            {
                _context.Set<T>().UpdateRange(list);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 删除实体，返回结果Bool
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task<bool> Delete(T t)
        {
            try
            {
                _context.Set<T>().Remove(t);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 删除实体集合，返回结果Bool
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task<bool> DeleteMany(IEnumerable<T> list)
        {
            try
            {
                _context.Set<T>().RemoveRange(list);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
