﻿using HomeJok.IServices;
using HomeJok.Model.Models;
using HomeJok.Repository;
using System;
using System.Collections.Generic;
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

        public async Task<List<UserInfo>> GetUserInfo()
        {
            return await _db.QueryList();
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
