using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Linq.Expressions;

using Orchard;
using Orchard.Data;
using Orchard.Logging;


using Oauth2OverOrchardCMS.Models;

namespace Oauth2OverOrchardCMS.Services
{
    public class AppClientServices:IAppClientServices
    {
        private readonly IOrchardServices _services;
        private readonly IRepository<AppClientRecord> _repository;

        public AppClientServices(
            IOrchardServices services,
            IRepository<AppClientRecord> clientPolicyRepository)
        {
            _services = services;
            _repository = clientPolicyRepository;
            Logger = NullLogger.Instance;
        }

        public ILogger Logger { get; set; }

        public Task<AppClientRecord> FindClientAsync(string clientId)
        {
            Expression<Func<AppClientRecord, bool>> clientPredicate = (f) => f.ClientId == clientId && f.IsActive == true;
            
            var record = _repository.Get(clientPredicate);

            return Task.FromResult(record);
        }


        public Task<object> SaveAsync(AppClientRecord clientRecord)
        {
            try {

                var result=_repository.CreateRecord(clientRecord);
                _repository.Flush();

                return Task.FromResult(result);

            }
            catch(Exception ex){
                Logger.Error(ex, "保存App端配置时发生错误");
                return Task.FromResult<object>(default(object));
            }
            
        }
    }
}