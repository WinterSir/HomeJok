using HomeJok.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HomeJok.IServices
{
    public interface IUserInfoService
    {
        UserInfo Login(string id);
        Task<List<UserInfo>> GetUserInfo(Expression<Func<UserInfo, bool>> predicate = null);
        Task<UserInfo> InsertUserInfo(UserInfo userInfo);
    }
}
