using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oauth2OverOrchardCMS.Models
{
    /// <summary>
    /// 枚举OAuth2协议授权的类型
    /// </summary>
    public enum OAuth2Grant
    {
        Code,
        Implicit,
        Password,
        Client,
        DeviceCode,
        RefreshToken
    }
}
