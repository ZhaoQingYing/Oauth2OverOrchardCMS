using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orchard.Users.Events;
using Orchard.Security;

namespace Oauth2OverOrchardCMS.Events
{
   
    public interface IAccountEventHandler :IUserEventHandler
    {
       
        /// <summary>
        /// 为用户创建账号
        /// </summary>
        /// <param name="user"></param>
        /// <param name="loginWay"></param>
        void CreateAccountForUser(IUser user,string loginWay);

        /// <summary>
        /// 更新登录模式
        /// </summary>
        /// <param name="loginWay"></param>
        void UpdateLoginMode(int userId,string loginWay);
    }
}
