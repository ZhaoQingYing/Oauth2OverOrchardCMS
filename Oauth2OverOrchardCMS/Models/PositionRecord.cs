using Orchard.ContentManagement.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Oauth2OverOrchardCMS.Models
{
    /// <summary>
    /// 员工入职信息表数据库实体
    /// </summary>
    public class PositionRecord : ContentPartRecord
    {
        public PositionRecord() {
            ContentItemRecord = new ContentItemRecord();
        }

        public virtual int EmployeeId { get; set; }

        /// <summary>
        /// 职位名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 入职时间
        /// </summary>
        public virtual DateTime? EntryTime { get; set; }

        /// <summary>
        /// 在职状态
        /// </summary>
        public virtual WorkingState WorkingState { get; set; }

        /// <summary>
        /// 离职原因
        /// </summary>
        public virtual string LeaveReason { get; set; }

        /// <summary>
        /// 离职时间
        /// </summary>
        public virtual DateTime? LeaveTime { get; set; }


        /// <summary>
        /// 合同开始日
        /// </summary>
        public virtual DateTime? ContractStart { get; set; }

        /// <summary>
        /// 合同终止日
        /// </summary>
        public virtual DateTime? ContractEnd { get; set; }

        /// <summary>
        /// 所属员工
        /// </summary>
        public virtual EmployeeRecord Employee { get;set; }
    }
}