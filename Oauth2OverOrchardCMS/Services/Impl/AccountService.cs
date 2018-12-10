using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Orchard;
using Orchard.Data;
using Orchard.Caching;
using Orchard.Logging;
using Orchard.ContentManagement;
using Orchard.Users.Models;
using Orchard.Security;
using Oauth2OverOrchardCMS.Models;
using Oauth2OverOrchardCMS.DTO;
using Oauth2OverOrchardCMS.Events;
using Oauth2OverOrchardCMS.Services;


namespace Oauth2OverOrchardCMS.Services
{
    public class AccountService:IAccountService
    {
        private readonly IOrchardServices _orchardService;
        private readonly IRepository<AccountPartRecord> _repository;
        private readonly IRepository<EmployeeRecord> _employeeRepository;
        private readonly IRepository<UserPartRecord> _userPartRepository;
        private readonly IMembershipService _membershipService;
        private readonly IAccountEventHandler _accountEventHandler;
        private readonly ICompanyService _companyService;
        private readonly IDepartmentService _deptService;
        private readonly IPositionService _positionService;
        private readonly ICacheManager _cacheManager;
        private readonly IMediaService _mediaService;
        
        
        private readonly static Object clearLock = new Object();
        private static bool RefreshAfterAccounts = false;

        public AccountService(
            IRepository<AccountPartRecord> repository,
            IRepository<UserPartRecord> userPartRepository,
            IRepository<EmployeeRecord> employeeRepository,
            IAccountEventHandler accountEventHandler,
            IMembershipService membershipService,
            ICompanyService companyService,
            IDepartmentService deptService,
            IPositionService positionService,
            ICacheManager cacheManager,
            IMediaService mediaService,
            IOrchardServices orchardService)
        {
            _repository = repository;
            _userPartRepository = userPartRepository;
            _employeeRepository = employeeRepository;
            _accountEventHandler = accountEventHandler;
            _membershipService = membershipService;
            _companyService = companyService;
            _deptService = deptService;
            _positionService = positionService;
            _cacheManager = cacheManager;
            _mediaService = mediaService;
            _orchardService = orchardService;
            Logger = NullLogger.Instance;
        }

        public ILogger Logger { get; set; }


        public Task CreateAsync(AccountDto dto)
        {
            var account = _orchardService.ContentManager.New<AccountPart>("Account");
            account.Sex = dto.Sex;
            account.UserId = dto.AccountId;
            account.MobilePhone = dto.MobilePhone;
            account.LoginWay = dto.LoginWay;
            account.ActiveState = ActiveState.Normal;//默认正常

            var employeeResult=HasEmployee(dto.MobilePhone);
            if (employeeResult.Result != null)
            {
                account.IsEmployee = true;
                account.Sex = employeeResult.Result.Sex;
                account.MobilePhone = dto.MobilePhone;
            }


            account.CreateOn = DateTime.Now;
            _orchardService.ContentManager.Create(account,VersionOptions.Published);//创建账号

            var defFileStream = _mediaService.GetDefaultMediaFile(Constant.DefaultAvatarName);
            if (defFileStream.Result != null)
            {

                //初次创建时使用默认头像
                var defaultFile = new MediaFileDto
                {
                    UserId = dto.AccountId,
                    UserName =dto.AccountName,
                    Stream = defFileStream.Result,
                    BusinessType = Constant.DefaultAvatar,
                    FileName = Constant.DefaultAvatarName
                };

                SaveUserAvatar(defaultFile);
            }

            ClearCache();

            return Task.FromResult<object>(null);
           
        }

        public Task<List<AccountPartRecord>> GetAccountsAsync()
        {
            return _cacheManager.Get("AllAccounts", context =>
            {
                context.Monitor(new SimpleBooleanToken(() => !RefreshAfterAccounts));

                var resultAll = _repository.Table.ToList();

                lock (clearLock)
                {
                    RefreshAfterAccounts = false;
                }

                return Task.FromResult(resultAll);
            });
        }


        public void ClearCache()
        {
            lock (clearLock) {
                RefreshAfterAccounts = true;
            }
        }


        public Task<string> SaveUserAvatar(MediaFileDto file)
        {
            try 
            {
                //先删除默认头像
                _mediaService.DeleteMediaFileAsync(file.UserId, file.BusinessType, true);

                //再新增
                var result=_mediaService.AddMediaFileAsync(file);

                return result;
            }
            catch (Exception ex) {

                throw ex;
            }
        }


        public Task<AccountDto> GetAccountAsync(int userId)
        {
            if (userId == 0)
            {
                return null;
            }

            var user = _userPartRepository.Get(userId);
            var dto = BuildAccountInfo(user);

            return Task.FromResult(dto);

        }

        private AccountDto BuildAccountInfo(UserPartRecord user) {
            if (user == null)
            {
                return null;
            }

            try
            {
                var model = new AccountDto();
                model.AccountName = user.UserName;
                model.Password = user.Password;
                model.Email = user.Email;

                var accountRecord = _repository.Table.Where(a => a.UserId == user.Id).FirstOrDefault();
                if (accountRecord != null)
                {
                    model.UserId = accountRecord.UserId;
                    model.AccountId = accountRecord.Id;
                    model.Sex = accountRecord.Sex;
                    model.LoginWay = accountRecord.LoginWay;
                    model.MobilePhone = accountRecord.MobilePhone;
                    model.PointScores = accountRecord.PointScores.Sum(x => x.PointValue);

                    //如果是员工
                    if (accountRecord.IsEmployee)
                    {
                        var employeRecord = _employeeRepository.Table.Where(emp => emp.MobilePhone == accountRecord.MobilePhone).FirstOrDefault();
                        if (employeRecord != null)
                        {
                            model.Sex = employeRecord.Sex;
                            model.WorkId = employeRecord.WorkId;
                            model.StaffId = employeRecord.Id;
                            model.MobilePhone = employeRecord.MobilePhone;
                            model.IsEmployee = true;

                            if (employeRecord.Department != null)
                            {
                                model.DeptName = employeRecord.Department.DeptName;

                                if (employeRecord.Department.Company != null)
                                {
                                    model.CompanyName = employeRecord.Department.Company.CompanyName;
                                    model.CompanyAddress = employeRecord.Department.Company.CompanyAddress;
                                }
                            }

                            if (employeRecord.Position != null)
                            {
                                model.PositionName = employeRecord.Position.Name;
                            }
                        }
                    }
                }


                var getResult = _mediaService.GetMediaUrlAsync(user.Id);
                model.Photo = getResult.Result;

                return model;
            }
            catch (Exception ex) {
                Logger.Error(ex, "查询用户信息时发生错误");
                return null;
            }
        }

        public Task<EmployeeRecord> HasEmployee(string mobilePhone)
        {
            //检查是否是员工
            var findEmployee= _employeeRepository.Table
                .Where(emp => emp.MobilePhone == mobilePhone)
                .FirstOrDefault();


            return Task.FromResult(findEmployee);
               
        }


        public Task ChangePasswordAsync(IUser distUser, string newPassword)
        {
            _membershipService.SetPassword(distUser,newPassword);

            _accountEventHandler.ChangedPassword(distUser);

            return Task.FromResult(default(object));
            
        }
    }
}