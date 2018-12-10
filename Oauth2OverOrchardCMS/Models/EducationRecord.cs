using Orchard.ContentManagement.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Oauth2OverOrchardCMS.Models
{
    /// <summary>
    /// 受教育信息表数据库实体
    /// </summary>
    public class EducationRecord:ContentPartRecord
    {
        public EducationRecord() {
            ContentItemRecord = new ContentItemRecord();
        }

        public virtual int EmployeeId { get; set; }

        /// <summary>
        /// 毕业院校
        /// </summary>
        public virtual string GraduateSchool { get; set; }

        /// <summary>
        /// 专业
        /// </summary>
        public virtual string Major { get; set; }

        /// <summary>
        /// 学历
        /// </summary>
        public virtual string Education { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        public virtual bool IsValid { get; set; }


        /// <summary>
        /// 所属员工
        /// </summary>
        public virtual EmployeeRecord Employee { get; set; }
    }
}