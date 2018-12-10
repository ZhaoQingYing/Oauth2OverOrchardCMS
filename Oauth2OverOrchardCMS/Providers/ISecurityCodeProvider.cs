using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orchard;

namespace Oauth2OverOrchardCMS.Providers
{
    public interface ISecurityCodeProvider:IDependency
    {
        /// <summary>
        /// 获取优先级，数字越大，越优先
        /// </summary>
        int Priority { get; }

        /// <summary>
        /// 是否可以处理类型
        /// </summary>
        /// <param name="type">验证码类型</param>
        /// <returns>可以处理，返回true，否则返回false</returns>
        bool CanHandle(string type);

        /// <summary>
        /// 获取过期时间
        /// </summary>
        /// <returns></returns>
        DateTime GetExpireTime();

        /// <summary>
        /// 获取验证码长度
        /// </summary>
        /// <returns></returns>
        int GetLenth();
    }
}
