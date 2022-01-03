using HomeJok.IServices;
using HomeJok.Model;
using HomeJok.Model.Models;
using HomeJok.Model.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeJok.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly IUserInfoService _userInfoService;
        public AccountController(IUserInfoService userInfoService)
        {
            _userInfoService = userInfoService;
        }

        #region 登录

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseModel> Login(AccountVM login)
        {
            ResponseModel rpm = new ResponseModel();
            if ((login.Account == "wintersir" || login.Account == "guest") && login.Password == "homejok.com")
            {
                rpm.ResponseState = true;
                rpm.ResponseInfo = "登录成功";
                rpm.ResponseData = new UserInfo()
                {
                    UserId = 1,
                    UserName = "wintersir",
                    Password = "homejok.com"
                };
            }
            else
            {
                rpm.ResponseState = false;
                rpm.ResponseInfo = "用户名或密码错误";
            }
            return rpm;
        } 
        #endregion

        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseModel> InsertUserInfo(UserInfo userInfo)
        {
            ResponseModel rpm = new ResponseModel();
            userInfo = await _userInfoService.InsertUserInfo(userInfo);
            if (userInfo != null)
            {
                rpm.ResponseState = true;
                rpm.ResponseInfo = "保存成功";
                rpm.ResponseData = userInfo;
            }
            else
            {
                rpm.ResponseState = false;
                rpm.ResponseInfo = "用户名或密码错误";
            }
            return rpm;
        }
    }
}
