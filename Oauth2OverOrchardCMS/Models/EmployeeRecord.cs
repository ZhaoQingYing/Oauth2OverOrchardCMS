using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Orchard.ContentManagement.Records;


namespace Oauth2OverOrchardCMS.Models
{
    /// <summary>
    /// 员工表数据库实体
    /// </summary>
    public class EmployeeRecord:ContentPartRecord
    {
        public EmployeeRecord() {
            ContentItemRecord = new ContentItemRecord();
        }

        public virtual int DepartmentId { get; set; }

        public virtual int EducationId { get; set; }

        public virtual int PositionId { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        public virtual string WorkId { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public virtual string FullName { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        public virtual string MobilePhone { get; set; }

        /// <summary>
        /// 性别(男:1,女:0)
        /// </summary>
        public virtual int Sex { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public virtual string IdNumber { get; set; }

        /// <summary>
        /// 出生日期(根据身份证号生成)
        /// </summary>
        public virtual DateTime? BirthDate { get; set; }


        /// <summary>
        /// 固定电话
        /// </summary>
        public virtual string FixedPhone { get; set; }

        /// <summary>
        /// 婚姻状态(0:未婚，1:已婚)
        /// </summary>
        public virtual int? MarriageState { get; set; }


        public virtual DateTime CreateOn { get; set; }


        /// <summary>
        /// 是否激活
        /// </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>
        /// 受教育信息
        /// </summary>
        public virtual EducationRecord Education { get; set; }


        /// <summary>
        /// 部门
        /// </summary>
        public virtual DepartmentRecord Department { get; set; }

        /// <summary>
        /// 入职信息
        /// </summary>
        public virtual PositionRecord Position { get; set; }

    }
}