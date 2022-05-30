using HomeJok.IServices;
using HomeJok.Model.Models;
using HomeJok.Repository;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HomeJok.Services
{
    public class UserInfoService : BaseService, IUserInfoService
    {
        private readonly IBaseRepository<UserInfo> _db;

        public UserInfoService(IBaseRepository<UserInfo> db)
        {
            _db = db;
        }

        public UserInfo Login(string id)
        {
            var parameter = new NpgsqlParameter[] {
                new NpgsqlParameter("@id",id),
            };
            List<UserInfo> list = _db.QueryListSql("select u.* from \"UserInfo\" u left join \"AspNetUsers\" au on u.\"Id\"=au.\"UserInfoId\" where au.\"Id\"=@id", parameter);
            //"select u.* from userinfo u left join AspNetUsers au on u.id=au.userinfoid where au.id=@id", parameter);
            return list.Count > 0 ? list[0] : null;
        }

        public async Task<List<UserInfo>> GetUserInfo(Expression<Func<UserInfo, bool>> predicate = null)
        {
            return await _db.QueryList(predicate);
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public async Task<UserInfo> InsertUserInfo(UserInfo userInfo)
        {
            return await _db.Insert(userInfo);
        }
    }
}
