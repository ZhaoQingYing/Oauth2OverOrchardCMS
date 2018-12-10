using Orchard.ContentManagement.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace JYT.Common.Models
{
    /// <summary>
    /// 车辆信息表数据库实体
    /// </summary>
    public class VehicleRecord:ContentPartRecord
    {
        public VehicleRecord() {
            ContentItemRecord = new ContentItemRecord();
        }

        /// <summary>
        /// 司机
        /// </summary>
        public virtual string Driver { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public virtual string VehicleLicence { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        public virtual bool IsValid { get; set; }
    }
}