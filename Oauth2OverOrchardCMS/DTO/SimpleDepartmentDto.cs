using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Oauth2OverOrchardCMS.DTO
{
    [Serializable]
    public class SimpleDepartmentDto
    {
        /// <summary>
        /// 部门编号
        /// </summary>
        public int DeptId { get; set; }


        /// <summary>
        /// 部门名称
        /// </summary>
        public  string DeptName { get; set; }
    }
}