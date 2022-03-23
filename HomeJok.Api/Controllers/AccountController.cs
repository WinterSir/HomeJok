using HomeJok.IServices;
using HomeJok.Model;
using HomeJok.Model.Models;
using HomeJok.Model.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeJok.Api.Controllers
{
    public class AccountController : BaseController
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IUserInfoService _userInfoService;
        public AccountController(ILogger<AccountController> logger, IUserInfoService userInfoService)
        {
            _logger = logger;
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
                    Id = 1,
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
        /// 查询用户列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<ResponseModel> GetUserInfo()
        {
            ResponseModel rpm = new ResponseModel();
            List<UserInfo> ulist = await _userInfoService.GetUserInfo();
            if (ulist.Count() > 0)
            {
                rpm.ResponseState = true;
                rpm.ResponseInfo = "获取成功";
                rpm.ResponseData = ulist;
            }
            else
            {
                rpm.ResponseState = false;
                rpm.ResponseInfo = "数据为空";
            }
            return rpm;
        }

        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseModel> InsertUserInfo(UserInfo userInfo)
        {
            ResponseModel rpm = new ResponseModel();
            _logger.LogInformation("开始：AccountController调用UserInfoService方法InsertUserInfo");
            userInfo = await _userInfoService.InsertUserInfo(userInfo);
            _logger.LogInformation("结束：AccountController调用UserInfoService方法InsertUserInfo");
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
