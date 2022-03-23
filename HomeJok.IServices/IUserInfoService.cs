using HomeJok.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeJok.IServices
{
    public interface IUserInfoService
    {
        Task<List<UserInfo>> GetUserInfo();
        Task<UserInfo> InsertUserInfo(UserInfo userInfo);
    }
}
