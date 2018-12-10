using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oauth2OverOrchardCMS.Models
{
    /// <summary>
    /// App登录方式
    /// </summary>
    public enum AppLoginMode
    {
        /// <summary>
        /// 标准方式(用户名/密码)
        /// </summary>
        standard,

        /// <summary>
        /// 短信验证码
        /// </summary>
        smscode,

    }
}
