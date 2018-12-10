using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Oauth2OverOrchardCMS.Providers
{
    public class SecurityCodeProvider:ISecurityCodeProvider
    {
        /// <summary>
        /// 优先级，数字越大，越优先
        /// </summary>
        public int Priority { get { return -10; } }

        /// <summary>
        /// 是否可以处理类型
        /// </summary>
        /// <param name="type">验证码类型</param>
        /// <returns>可以处理，返回true，否则返回false</returns>
        public bool CanHandle(string type)
        {
            return true;
        }

        /// <summary>
        /// 获取过期时间
        /// </summary>
        /// <returns></returns>
        public DateTime GetExpireTime()
        {
            return DateTime.Now.AddMinutes(5);//2
        }

        /// <summary>
        /// 获取验证码长度
        /// </summary>
        /// <returns></returns>
        public int GetLenth()
        {
            return 5;
        }
    }
}