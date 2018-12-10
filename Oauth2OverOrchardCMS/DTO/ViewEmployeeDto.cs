using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Oauth2OverOrchardCMS.DTO
{
    [Serializable]
    public class ViewEmployeeDto
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public virtual int Sex { get; set; }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        public  string Email { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 职位名称
        /// </summary>
        public string PositionName { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 固定电话(座机)
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string MobilePhone { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string AddressText { get; set; }
    }
}