using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;


using Orchard;
using Orchard.Security;
using Orchard.Logging;
using Orchard.Environment.Configuration;

using Oauth2OverOrchardCMS.Services;
using Oauth2OverOrchardCMS.Models;

using Oauth2OverOrchardCMS.Events;




namespace Oauth2OverOrchardCMS.Providers
{
    /// <summary>
    /// 提供一个默认实现OAuth2协议的身份认证机制
    /// </summary>
    public class DefaultAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private readonly IOrchardServices _orchardServices;
        private readonly IAppClientServices _clientPolicyServices;
        private readonly ISecurityCodeService _securityCodeService;
        private readonly IAccountEventHandler _accountEventHandler;

        /// <summary>
        /// 初始化默认实现OAuth2协议的身份认证机制
        /// </summary>
        /// <param name="orchardServices"></param>
        /// <param name="clientPolicyServices"></param>
        /// <param name="logger"></param>
        public DefaultAuthorizationServerProvider(
            IOrchardServices orchardServices,
            IAppClientServices clientPolicyServices, 
            ISecurityCodeService securityCodeService,
            IAccountEventHandler accountEventHandler
            )
        {
            _orchardServices = orchardServices;
            _clientPolicyServices = clientPolicyServices;
            _securityCodeService = securityCodeService;
            _accountEventHandler = accountEventHandler;
            
            Logger =NullLogger.Instance;
            
        }

        public ILogger Logger { get; set; }



        /// <summary>
        /// 处理OAuth令牌请求
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            await base.TokenEndpoint(context);
        }

        /// <summary>
        /// 验证客户端身份
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {

            string clientId;
            string clientSecret;

            if (context.TryGetBasicCredentials(out clientId, out clientSecret) == false)
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }


            var  loginWay = context.Parameters.Get("loginway");
            
            if (string.IsNullOrEmpty(loginWay))
            {
                context.SetError("invalid_loginway", "未收到LoginWay.");
                return;
            }

            if (context.ClientId == null)
            {
                context.SetError("invalid_clientId", "未收到ClientId.");
                return;
            }


            var client = await _clientPolicyServices.FindClientAsync(context.ClientId);
            if (client == null)
            {
                context.SetError("invalid_clientId", string.Format("客户端'{0}'未在系统中注册.", context.ClientId));
                return;
            }


            if (client.ClientSecret != TokenHelper.BuildHashToken(clientSecret))
            {
                context.SetError("invalid_client", "客户端密钥无效");
                return;
            }


            if (!client.IsActive)
            {
                context.SetError("invalid_clientId", "客户端未被激活.");
                return;
            }

            context.OwinContext.Set<AppClientRecord>("jytOAuth2:client", client);
            context.OwinContext.Set<string>("AppLoginModel", loginWay);

            context.Validated();

        }

        /// <summary>
        /// 处理资源的授权
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var userName = context.UserName;
            var password = context.Password;

            var client = context.OwinContext.Get<AppClientRecord>("jytOAuth2:client");
            var loginMode=context.OwinContext.Get<string>("AppLoginModel");

            if (string.IsNullOrEmpty(client.AllowedOrigin))
                context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            else
                context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { client.AllowedOrigin });

            if (client.AllowedGrant == OAuth2Grant.Password)
            {
                var AppLoginWay=(AppLoginMode)Enum.Parse(typeof(AppLoginMode),loginMode.ToLowerInvariant());
                
                //如果是短信验证码登录方式，首先检测验证码是否正确
                if (AppLoginWay == AppLoginMode.smscode)
                {
                    var success=await _securityCodeService.CheckCode(userName,Constant.SmsTypeForLogin, password, true);
                    if (!success) {
                        context.SetError("invalid_grant", "验证码不正确或已过期");
                    }
                }

                if (String.IsNullOrWhiteSpace(userName) || String.IsNullOrWhiteSpace(password))
                {
                    context.SetError("invalid_grant", "用户名和密码必须填写");
                }

                var user = _orchardServices.WorkContext.Resolve<IMembershipService>().GetUser(userName);
                if (user != null)
                {
                    if (AppLoginWay == AppLoginMode.smscode)
                    {
                        //调用后台的登录服务验证用户名与密码
                        user = _orchardServices.WorkContext.Resolve<IMembershipService>().ValidateUser(userName, Constant.DefaultAccountPassword);
                        if (user == null) {
                            context.SetError("invalid_grant", "用户名或密码不正确");
                            Logger.Warning(string.Format("用户{0}，从APP登录失败,登录方式:{1}.)", userName, loginMode));
                        }
                       
                    }
                    else {

                        //调用后台的登录服务验证用户名与密码
                        user = _orchardServices.WorkContext.Resolve<IMembershipService>().ValidateUser(userName, password);
                        
                    }
                   
                    if (user == null)
                    {
                        context.SetError("invalid_grant", "用户或密码不正确");
                        Logger.Warning(string.Format("用户{0}，从APP登录失败,登录方式:{1}.", userName, loginMode));
                    }

                    _accountEventHandler.LoggedIn(user);
                    _accountEventHandler.UpdateLoginMode(user.Id, loginMode);
                    
                }
                else
                {
                    //创建用户并设置默认密码
                    user=_orchardServices.WorkContext.Resolve<IMembershipService>().CreateUser(new CreateUserParams(string.Join("|", userName,loginMode), Constant.DefaultAccountPassword, null, null, null, true));

                    _accountEventHandler.CreateAccountForUser(user, loginMode);
                    
                }

                ClaimsIdentity oAuthIdentity = new ClaimsIdentity(OAuthDefaults.AuthenticationType);
                oAuthIdentity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                oAuthIdentity.AddClaim(new Claim(ClaimTypes.PrimarySid, user.Id.ToString()));
                oAuthIdentity.AddClaim(new Claim("SettingName", _orchardServices.WorkContext.Resolve<ShellSettings>().Name));
                AuthenticationProperties properties = CreateProperties(user, context);
                AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);

                Logger.Information("用户{0}从APP登录成功", userName);

                context.Validated(ticket);
            }
            else
            {
                context.SetError("invalid_grant", "客户端的密码凭据没有被授予访问");
                Logger.Warning("用户{0}，没有被授于密码凭据访问，登录失败", userName);
            }

        }

        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            var originalClient = context.Ticket.Properties.Dictionary["client_id"];
            var client = context.OwinContext.Get<AppClientRecord>("jytOAuth2:client");

            var currentClient = client.ClientId.ToString();

            if (originalClient != currentClient)
            {
                context.SetError("invalid_clientId","接收的刷新令牌与clientId不匹配.");
                return Task.FromResult<object>(null);
            }

            //更改刷新令牌请求的身份验证票据
            var newIdentity = new ClaimsIdentity(context.Ticket.Identity);
            newIdentity.AddClaim(new Claim("newClaim", "newValue"));

            var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);
            context.Validated(newTicket);

            return Task.FromResult<object>(null);
        }

        private AuthenticationProperties CreateProperties(IUser user,OAuthGrantResourceOwnerCredentialsContext context)
        {
            var checkResult =_orchardServices.WorkContext.Resolve<IAccountService>().HasEmployee(user.UserName);

            var isstaff = (checkResult.Result != null) ? 1 : 0;

            IDictionary<string, string> data = new Dictionary<string,string>
            {
                { "client_id", (context.ClientId == null) ? string.Empty : context.ClientId },
                { "staff",isstaff.ToString() },//是否是内部员工
                { "userid",user.Id.ToString()},
                { "userName", user.UserName }
                             
            };
            return new AuthenticationProperties(data);
        }

    }
}