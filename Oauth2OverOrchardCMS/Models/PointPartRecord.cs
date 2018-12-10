using Orchard.ContentManagement.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Oauth2OverOrchardCMS.Models
{
    /// <summary>
    /// 积分表数据库实体
    /// </summary>
    public class PointPartRecord : ContentPartRecord
    {
        public PointPartRecord() {
            ContentItemRecord = new ContentItemRecord();
        }

        /// <summary>
        /// 积分来源:(例如:维修单)
        /// </summary>
        public virtual string PointFrom { get; set; }

        /// <summary>
        /// 积分值
        /// </summary>
        public virtual int PointValue { get; set; }

        /// <summary>
        /// 积分类型(预留扩展)
        /// </summary>
        public virtual int PointType { get; set; }

        /// <summary>
        /// 积分理由(说明)
        /// </summary>
        public virtual string PointReason { get; set; }

        /// <summary>
        /// 积分所属账户
        /// </summary>
        public virtual AccountPartRecord  OwnAccount { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        public virtual bool IsValid { get; set; }

        public virtual DateTime CreateTime { get; set; }

    }
}