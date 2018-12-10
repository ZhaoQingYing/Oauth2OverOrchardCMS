using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Oauth2OverOrchardCMS.Models;

namespace Oauth2OverOrchardCMS.Services
{
    public interface IAppClientServices : Orchard.IDependency
    {
        Task<AppClientRecord> FindClientAsync(string clientId);

        Task<object> SaveAsync(AppClientRecord clientRecord);
    }
}