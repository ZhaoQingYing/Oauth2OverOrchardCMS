using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Oauth2OverOrchardCMS.DTO
{
    [Serializable]
    public class CompanyDto
    {
        public int CompanyId { get; set; } 

        /// <summary>
        /// 公司名称
        /// </summary>
        public  string CompanyName { get; set; }

        /// <summary>
        /// 公司描述
        /// </summary>
        public  string CompanyDesc { get; set; }

        /// <summary>
        /// 公司电话
        /// </summary>
        public string CompanyPhone { get; set; }

        /// <summary>
        /// 公司地址
        /// </summary>
        public  string CompanyAddress { get; set; }
    }
}