using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Oauth2OverOrchardCMS.Models;

namespace Oauth2OverOrchardCMS.Services
{
    public interface IRefreshTokenService : Orchard.IDependency
    {
        Task<RefreshTokenRecord> GetAsync(string TokenId);
        Task<object> SaveAsync(RefreshTokenRecord refreshToken);
        Task<bool> RemoveAsync(string TokenId);
    }
}