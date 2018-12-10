using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Oauth2OverOrchardCMS.DTO
{
    [Serializable]
    public class AccountDto
    {
        public int AccountId { get; set; }

        public string AccountName { get; set; }

        public string Password { get; set; }

        public string Email { get;set;}


        public string WorkId { get; set; }

        public int UserId { get; set; }


        public bool IsEmployee { get; set; }


        public int StaffId { get; set; }

        public int PointScores { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string MobilePhone { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string Photo { get; set; }

        /// <summary>
        /// 固定电话
        /// </summary>
        public string FixedPhone { get; set; }

        /// <summary>
        /// 性别(男:1,女:0,-1:未知)
        /// </summary>
        public int Sex { get; set; }


        /// <summary>
        /// 登录方式
        /// </summary>
        public string LoginWay { get; set; }

        /// <summary>
        /// 公司信息
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 公司地址
        /// </summary>
        public string CompanyAddress { get; set; }

        /// <summary>
        /// 部门信息
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 职位信息
        /// </summary>
        public string PositionName { get; set; }
    }
}