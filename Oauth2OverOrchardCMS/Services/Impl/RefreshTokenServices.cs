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
    public class RefreshTokenServices:IRefreshTokenService
    {
        private readonly IOrchardServices _services;
        private readonly IRepository<RefreshTokenRecord> _repository;

        public RefreshTokenServices(
            IOrchardServices services,
            IRepository<RefreshTokenRecord> repository)
        {
            _services = services;
            _repository = repository;
            Logger = NullLogger.Instance;

        }

        public ILogger Logger { get; set; }

        public Task<RefreshTokenRecord> GetAsync(string TokenId)
        {
            Expression<Func<RefreshTokenRecord,bool>> findExp=(f)=>f.TokenId==TokenId;
            var refreshToken = _repository.Get(findExp);

            return Task.FromResult(refreshToken);
        }

        public async Task<object> SaveAsync(RefreshTokenRecord refreshToken)
        {
            try 
            {
                var existingToken = _repository.Table
                    .Where(r => r.Subject == refreshToken.Subject && r.ClientId == refreshToken.ClientId)
                    .SingleOrDefault();

                if (existingToken != null)
                {
                    var delResult = await RemoveRefreshTokenAsync(existingToken);
                    
                }

                var addResult=_repository.CreateRecord(refreshToken);
                _repository.Flush();

                return addResult;
            }
            catch(Exception ex) {
                Logger.Error(ex, "保存刷新令牌时发生错误");
                return Task.FromResult<object>(default(object));
            }
            
        }

        public async Task<bool> RemoveAsync(string TokenId)
        {
            Expression<Func<RefreshTokenRecord,bool>> findExp=(f)=>f.TokenId==TokenId;

            var refreshToken = _repository.Get(findExp);

            if (refreshToken != null) {
                var isOK = await RemoveRefreshTokenAsync(refreshToken);
                return isOK;
            }

            return false;
        }


        private Task<bool> RemoveRefreshTokenAsync(RefreshTokenRecord refreshToken)
        {
            try 
            {
                _repository.Delete(refreshToken);
                return Task.FromResult(true);
            }
            catch(Exception ex) {
                Logger.Error(ex, "删除刷新令牌时发生错误");
                return Task.FromResult(false);
            }
           
        }
    }
}