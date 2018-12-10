using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Oauth2OverOrchardCMS.Models
{
    /// <summary>
    /// 客户端交互配置表数据库实体类
    /// </summary>
    public class AppClientRecord
    {
        public virtual int Id { get; set; }

        public virtual string ClientId { get; set; }
        public virtual string ClientSecret { get; set; }

        /// <summary>
        /// 获取或设置使用场景名称
        /// </summary>
        public virtual string Name { get; set; }

        public virtual bool IsActive { get; set; }

        /// <summary>
        /// 获取或设置客户端跨域访问的边界
        /// </summary>
        public virtual string AllowedOrigin { get; set; }

        /// <summary>
        /// 允许的授权类型(https://oauth.net/2/)
        /// </summary>
        public virtual OAuth2Grant AllowedGrant { get; set; }

        /// <summary>
        /// 获取或设置刷新令牌的生命周期(分钟数）
        /// </summary>
        public virtual int RefreshTokenLifecycle { get; set; }
        public virtual DateTime CreatedOn { get; set; }
    }

    
}