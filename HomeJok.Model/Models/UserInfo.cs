using System;
using System.Collections.Generic;
using System.Text;

namespace HomeJok.Model.Models
{
    public class UserInfo
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RealName { get; set; }
        public string NickName { get; set; }
        public string Avatar { get; set; }
        public int Sex { get; set; }
        public int Age { get; set; }
        public string Phone { get; set; }
        public DateTime Birthday { get; set; }
        public string Address { get; set; }
        public bool ActiveState { get; set; }
        public DateTime LastLoginTime { get; set; }
        public int CreateUser { get; set; }
        public DateTime CreateTime { get; set; }
        public int UpdateUser { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
