using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orchard.Users.Models;

namespace Oauth2OverOrchardCMS.Services
{
    public interface ICommonService:Orchard.IDependency
    {
        /// <summary>
        /// 验证密码是否相等
        /// </summary>
        /// <param name="leftPassword"></param>
        /// <param name="rightPassword"></param>
        /// <returns></returns>
        bool ValidateEqualFor(UserPart leftPassword, string rightPassword);
    }
}
