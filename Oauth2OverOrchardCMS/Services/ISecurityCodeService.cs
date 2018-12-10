using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oauth2OverOrchardCMS.Services
{
    /// <summary>
    ///  定义一个验证码服务接口
    /// </summary>
    public interface ISecurityCodeService : Orchard.IDependency
    {
        /// <summary>
        /// 为指定手机号或Email地址请求验证码
        /// </summary>
        /// <param name="channel">通道号码，如短信为手机号码，邮件为邮箱号码</param>
        /// <param name="type">验证码类型</param>
        /// <returns>获取验证码</returns>
        Task<string> RequiredCode(string channel, string type);

        /// <summary>
        /// 验证码是否正确
        /// </summary>
        /// <param name="channel">通道号码</param>
        /// <param name="type">验证类型</param>
        /// <param name="code">验证的编码类型</param>
        /// <param name="update">是否更新验证标识位</param>
        /// <returns></returns>
        Task<bool> CheckCode(string channel, string type, string code, bool update);
    }
}
