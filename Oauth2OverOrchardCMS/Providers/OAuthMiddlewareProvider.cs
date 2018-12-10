using System;
using System.Collections.Generic;
using Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;

using Orchard;
using Orchard.Security;
using Orchard.Owin;
using Oauth2OverOrchardCMS.Services;
using Oauth2OverOrchardCMS.Events;

namespace Oauth2OverOrchardCMS.Providers
{
    public class OAuthMiddlewareProvider : IOwinMiddlewareProvider
    {
        private readonly IOrchardServices _orchardServices;
        private readonly IRefreshTokenService _refreshTokenServices;
        private readonly IAppClientServices _clientPolicyServices;
        private readonly ISecurityCodeService _securityCodeService;
        private readonly IAccountEventHandler _accountEventHandler;


        public OAuthMiddlewareProvider(
            IOrchardServices orardSeries,
            IRefreshTokenService refreshTokenServices,
            IAppClientServices clientPolicyServices,
            ISecurityCodeService securityCodeService,
            IAccountEventHandler accountEventHandler)
        {
            _orchardServices = orardSeries;
            _refreshTokenServices = refreshTokenServices;
            _clientPolicyServices = clientPolicyServices;
            _securityCodeService = securityCodeService;
            _accountEventHandler = accountEventHandler;


        }

        public IEnumerable<OwinMiddlewareRegistration> GetOwinMiddlewares()
        {
            return new[]{
                 
                //OAuth2认证模式:密码模式(https://www.oauth.com/oauth2-servers/access-tokens/password-grant/)
                new OwinMiddlewareRegistration
                {
                    Priority = "1",
                    Configure = app =>
                    {
                        var options = new OAuthAuthorizationServerOptions
                        {
                            TokenEndpointPath = new PathString("/oauth2/token"),

                            Provider = new DefaultAuthorizationServerProvider(_orchardServices,_clientPolicyServices,_securityCodeService,_accountEventHandler),

                            AccessTokenExpireTimeSpan =TimeSpan.FromHours(1), //TimeSpan.FromDays(30),//访问令牌过期时间
                            AllowInsecureHttp = true,
                            RefreshTokenProvider = new DefaultRefreshTokenServerProvider(_orchardServices,_refreshTokenServices)
                        };

                        app.UseOAuthAuthorizationServer(options);
                        app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

                    }
                },

                new OwinMiddlewareRegistration{
                    Priority="2",
                    Configure=app=>{
                        app.Use(async (context,next)=>
                        {
                            var workContext=_orchardServices.WorkContext;

                            var authenticationService = workContext.Resolve<IAuthenticationService>();

                            var membershipService = workContext.Resolve<IMembershipService>();

                            var user = membershipService.GetUser(context.Request.User.Identity.Name);

                            authenticationService.SetAuthenticatedUserForRequest(user);

                            await next();

                        });
                    }
                }
            }; 
        }
    }
}