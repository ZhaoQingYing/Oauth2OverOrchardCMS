using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Orchard.ContentManagement.Records;

namespace JYT.Common.Models
{
    /// <summary>
    /// 配件表数据库实体
    /// </summary>
    public class HeatingFurnacePartRecord : ContentPartRecord
    {
        public HeatingFurnacePartRecord() {
            ContentItemRecord = new ContentItemRecord();
        }

        /// <summary>
        /// 配件名称
        /// </summary>
        public virtual string PartName { get; set; }

        /// <summary>
        /// 配件型号
        /// </summary>
        public virtual string PartModel { get; set; }

        /// <summary>
        /// ERP系统编号
        /// </summary>
        public virtual string ERPNumber { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public virtual int Quantity { get; set; }

        /// <summary>
        /// 配件描述
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        public virtual bool IsValid { get; set; }
    }
}