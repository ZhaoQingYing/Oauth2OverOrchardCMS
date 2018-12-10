
using Orchard;
using Orchard.Security;
using Orchard.ContentManagement;
using Orchard.Users.Events;
using Orchard.Users.Models;
using Orchard.Logging;
using Oauth2OverOrchardCMS.DTO;
using Oauth2OverOrchardCMS.Services;
using Orchard.Services;
using Oauth2OverOrchardCMS.Models;
using Orchard.Environment.Extensions;

namespace Oauth2OverOrchardCMS.Events
{
    public class AccountEventHandler :IAccountEventHandler
    {
        private readonly IClock _clock;
        private readonly IOrchardServices _orchardService;


        public AccountEventHandler(
            IOrchardServices orchardService,
            IClock clock
            )
        {
            _orchardService = orchardService;
            _clock = clock;
            Logger = NullLogger.Instance;
        }


        public ILogger Logger { get; set; }

        public void CreateAccountForUser(IUser user,string loginWay)
        {
            //每新建一个用户，就为其创建一个账户
            var account = new AccountDto();

            //使用user信息进行关联
            account.AccountId = user.Id;
            account.AccountName = user.UserName;
            account.Email = user.Email;
            account.Sex = -1;

            if (VerifyHelper.IsMobile(user.UserName))
            {
                account.MobilePhone = user.UserName;
            }

            account.LoginWay = loginWay;

            _orchardService.WorkContext.Resolve<IAccountService>().CreateAsync(account);

            Logger.Debug(string.Format("创建账户:{0}", account));
        }

        public void UpdateLoginMode(int userId,string loginWay)
        {
            var acccount=_orchardService.WorkContext.Resolve<IAccountService>().GetAccountAsync(userId);
            if (acccount.Result != null) {
                _orchardService.ContentManager.Get<AccountPart>(acccount.Result.AccountId).LoginWay = loginWay;
            }
        }

        public void Creating(UserContext context)
        {
            
        }

        public void Created(UserContext context)
        {
            var user = context.User.As<UserPart>();

        }

        public void LoggingIn(string userNameOrEmail, string password)
        {
            
        }

        public void LoggedIn(IUser user)
        {
            user.As<UserPart>().LastLoginUtc = _clock.UtcNow;
        }

        public void LogInFailed(string userNameOrEmail, string password)
        {
            
        }

        public void LoggedOut(IUser user)
        {
            user.As<UserPart>().LastLogoutUtc = _clock.UtcNow;
        }

        public void AccessDenied(IUser user)
        {
            
        }

        public void ChangedPassword(IUser user)
        {
            
        }

        public void SentChallengeEmail(IUser user)
        {
            
        }

        public void ConfirmedEmail(IUser user)
        {
            
        }

        public void Approved(IUser user)
        {
            
        }
    }
}