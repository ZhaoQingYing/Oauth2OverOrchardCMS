using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oauth2OverOrchardCMS.Services
{
    public interface ISMService : Orchard.IDependency
    {
        /// <summary>
        /// 验证码提供商
        /// </summary>
        /// <returns></returns>
        Task<string> GetSMSProvider();

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="mobile">接收短信的手机号码</param>
        /// <param name="message">短信内容</param>
        /// <returns>发送成功返回ture，失败返回false</returns>
        Task<bool> SendSMS(string mobile, string message);
    }
}
