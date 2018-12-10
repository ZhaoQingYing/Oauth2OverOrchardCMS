using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Orchard.Data.Conventions;
using Orchard.ContentManagement.Records;


namespace Oauth2OverOrchardCMS.Models
{
    /// <summary>
    /// 公司信息表数据库实体
    /// </summary>
    public class CompanyRecord:ContentPartRecord
    {
        public CompanyRecord() {
            Departments = new List<DepartmentRecord>();
            ContentItemRecord = new ContentItemRecord();
        }

        /// <summary>
        /// 公司名称
        /// </summary>
        public virtual string CompanyName { get; set; }

        /// <summary>
        /// 公司描述
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// 公司电话
        /// </summary>
        public virtual string CompanyPhone { get; set; }

        /// <summary>
        /// 公司地址
        /// </summary>
        public virtual string CompanyAddress { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        public virtual bool IsValid { get; set; }


        /// <summary>
        /// 公司的部门
        /// </summary>
        public virtual ICollection<DepartmentRecord> Departments { get; set; }
    }
}