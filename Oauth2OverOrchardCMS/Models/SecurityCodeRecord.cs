using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JYT.Common.Models
{
    public class SecurityCodeRecord
    {
        public virtual int Id { get; set; }

        /// <summary>
        /// 服务提供商名称
        /// </summary>
        public virtual string ServiceProvider { get; set; }

        /// <summary>
        /// 通道号码，手机号码或者邮箱
        /// </summary>
        public virtual string Channel { get; set; }

        /// <summary>
        /// 验证码类型
        /// </summary>
        public virtual string CodeType { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public virtual string Code { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateOn { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public virtual DateTime ExpireTime { get; set; }

        /// <summary>
        /// 是否已经验证
        /// </summary>
        public virtual bool IsValid { get; set; }
    }
}