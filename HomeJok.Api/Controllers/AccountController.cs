using HomeJok.Model;
using HomeJok.Model.Models;
using HomeJok.Model.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DongSir.Core.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AccountController : Controller
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResponseModel> Login(AccountVM login)
        {
            ResponseModel rpm = new ResponseModel();
            if (login.Account == "admin" && login.Password == "dongsir.admin")
            {
                rpm.ResponseState = true;
                rpm.ResponseInfo = "登录成功";
                rpm.ResponseData = new UserInfo()
                {
                    UserId = 1,
                    UserName = "admin",
                    Password = "dongsir.admin"
                };
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
