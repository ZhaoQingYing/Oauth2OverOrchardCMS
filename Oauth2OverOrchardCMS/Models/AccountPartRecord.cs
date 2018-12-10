using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Users.Models;
using Orchard.ContentManagement.Records;

using Orchard.Data.Conventions;

namespace Oauth2OverOrchardCMS.Models
{
    /// <summary>
    /// 账户表
    /// </summary>
    public class AccountPartRecord : ContentPartRecord
    {
        public AccountPartRecord() {

            ContentItemRecord = new ContentItemRecord();
            PointScores = new List<PointPartRecord>();
        }

        public virtual string MobilePhone { get; set; }

        /// <summary>
        /// 0:女,1:男,-1:未知
        /// </summary>
        public virtual int Sex { get; set; }

        /// <summary>
        /// 积分
        /// </summary>
        public virtual ICollection<PointPartRecord> PointScores { get; set; }

        
        public virtual int UserId { get; set; }


        /// <summary>
        /// 是否是员工
        /// </summary>
        public virtual bool IsEmployee { get; set; }


        /// <summary>
        /// 登录方式(短信验证码登录，普通登录)
        /// </summary>
        public virtual string LoginWay { get; set; }

        /// <summary>
        /// 活跃状态(暂定)
        /// </summary>
        public virtual ActiveState ActiveState { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateOn { get; set; }

    }
}