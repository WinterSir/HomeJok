using HomeJok.Model.Models;
using System.Threading.Tasks;

namespace HomeJok.IServices
{
    public interface IUserInfoService
    {
        Task<UserInfo> InsertUserInfo(UserInfo userInfo);
    }
}
