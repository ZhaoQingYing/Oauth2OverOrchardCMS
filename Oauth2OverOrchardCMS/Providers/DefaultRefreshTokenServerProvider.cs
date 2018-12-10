using System;
using System.Threading.Tasks;

using Orchard;
using Microsoft.Owin.Security.Infrastructure;
using Oauth2OverOrchardCMS.Models;
using Oauth2OverOrchardCMS.Services;

namespace Oauth2OverOrchardCMS.Providers
{
    /// <summary>
    /// 提供一个默认令牌刷新机制
    /// </summary>
    public class DefaultRefreshTokenServerProvider : IAuthenticationTokenProvider
    {
        private IOrchardServices _services;
        private IRefreshTokenService _refreshTokenService;

        /// <summary>
        /// 初始化默认令牌刷新机制
        /// </summary>
        /// <param name="services"></param>
        /// <param name="refreshTokenService"></param>
        /// <param name="logger"></param>
        public DefaultRefreshTokenServerProvider(IOrchardServices services,
            IRefreshTokenService refreshTokenService)
        {
            _services = services;
            _refreshTokenService = refreshTokenService;
        }


        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            var refreshTokenId = Guid.NewGuid().ToString("n");

            var client = context.OwinContext.Get<AppClientRecord>("jytOAuth2:client");
            if (client == null) {
                return;
            }

            var refreshToken = new RefreshTokenRecord()
            {
                TokenId = TokenHelper.BuildHashToken(refreshTokenId),
                ClientId = client.ClientId,
                Subject = context.Ticket.Identity.Name,
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(Convert.ToDouble(client.RefreshTokenLifecycle))//刷新令牌过期时间
            };

            context.Ticket.Properties.IssuedUtc = refreshToken.IssuedUtc;
            context.Ticket.Properties.ExpiresUtc = refreshToken.ExpiresUtc;

            //这里不能随便移动，否则可能导致无法刷新令牌
            refreshToken.ProtectedTicket = context.SerializeTicket();

            //保存刷新令牌
            var saveResult=(int) await _refreshTokenService.SaveAsync(refreshToken);

            var isOK = saveResult > 0 ? true : false;
            if (isOK) 
                context.SetToken(refreshTokenId);
        }

       
        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            var client = context.OwinContext.Get<AppClientRecord>("jytOAuth2:client");
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { client.AllowedOrigin });

            var hashedTokenId =TokenHelper.BuildHashToken(context.Token);

            var refreshToken = await _refreshTokenService.GetAsync(hashedTokenId);
            if (refreshToken != null)
            {
                context.DeserializeTicket(refreshToken.ProtectedTicket);
                await _refreshTokenService.RemoveAsync(context.Token);//及时移除旧的令牌
            }
        }

        public void Create(AuthenticationTokenCreateContext context)
        {
            
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
           
        }
    }
}