using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement.Records;

namespace JYT.Common.Models
{

    /// <summary>
    /// 资源信息记录数据库实体
    /// </summary>
    public class ResourceRecord:ContentPartRecord
    {

        public ResourceRecord() {
            ContentItemRecord = new ContentItemRecord();
        }


        /// <summary>
        /// 用户ID，用于记录上传者
        /// </summary>
        public virtual int UserId { get; set; }

        /// <summary>
        /// 是否删除：0 - 未删除 1 - 已删除
        /// </summary>
        public virtual bool Deleted { get; set; }

        /// <summary>
        /// 文件的媒体类型
        /// </summary>
        public virtual string MimeType { get; set; }

        /// <summary>
        /// 类别
        /// </summary>
        public virtual string BusinessType { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        public virtual string Path { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        public virtual string FileName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }

        /// <summary>
        /// 上传时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }


    }
}