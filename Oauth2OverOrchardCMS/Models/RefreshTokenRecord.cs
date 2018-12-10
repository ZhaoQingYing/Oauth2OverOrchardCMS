using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Oauth2OverOrchardCMS.Models
{
    /// <summary>
    /// 刷新令牌表数据库实体
    /// </summary>
    public class RefreshTokenRecord
    {
        public virtual int Id { get; set; }
        public virtual string TokenId { get; set; }
        public virtual string Subject { get; set; }
        public virtual string ClientId { get; set; }
        public virtual DateTime IssuedUtc { get; set; }
        public virtual DateTime ExpiresUtc { get; set; }

        public virtual string ProtectedTicket { get; set; }
    }
}