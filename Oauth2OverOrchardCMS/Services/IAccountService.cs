using System.Collections.Generic;
using System.Threading.Tasks;

using Oauth2OverOrchardCMS.DTO;
using Oauth2OverOrchardCMS.Models;

namespace Oauth2OverOrchardCMS.Services
{
    public interface IAccountService:Orchard.IDependency
    {
        /// <summary>
        /// 创建账户
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>一般返回标识</returns>
        Task CreateAsync(AccountDto dto);

        /// <summary>
        /// 修改账户密码
        /// </summary>
        /// <param name="distUser"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        Task ChangePasswordAsync(Orchard.Security.IUser distUser, string newPassword);


        /// <summary>
        /// 保存用户头像资源
        /// </summary>
        /// <param name="file">头像资源</param>
        Task<string> SaveUserAvatar(MediaFileDto file);

        /// <summary>
        ///获取所有账户
        /// </summary>
        /// <returns></returns>
        Task<List<AccountPartRecord>> GetAccountsAsync();

      
        /// <summary>
        /// 获取指定账户
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        Task<AccountDto> GetAccountAsync(int userId);

        /// <summary>
        /// 检查是否有对应的员工信息
        /// </summary>
        /// <param name="mobilePhone"></param>
        /// <returns></returns>
        Task<EmployeeRecord> HasEmployee(string mobilePhone);

        void ClearCache();

    }
}
