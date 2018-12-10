using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Orchard.Data.Conventions;
using Orchard.ContentManagement.Records;

namespace Oauth2OverOrchardCMS.Models
{
    /// <summary>
    /// 部门信息表数据库实体
    /// </summary>
    public class DepartmentRecord : ContentPartRecord
    {
        public DepartmentRecord() {
            Employees = new List<EmployeeRecord>();
            ContentItemRecord = new ContentItemRecord();
        }

        public virtual int CompanyId { get; set; }


        public virtual int ParentId { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public virtual string DeptName { get; set; }

        /// <summary>
        /// 所属上级部门名称
        /// </summary>
        public virtual DepartmentRecord Parent { get; set; }

        /// <summary>
        /// 部门描述
        /// </summary>
        public virtual string Description { get; set; }


        public virtual DateTime CreateOn { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        public virtual bool IsValid { get; set; }


        /// <summary>
        /// 所属公司信息
        /// </summary>
        public virtual CompanyRecord Company { get; set; }

        /// <summary>
        /// 部门的员工
        /// </summary>
        public virtual ICollection<EmployeeRecord> Employees { get; set; }

    }
}