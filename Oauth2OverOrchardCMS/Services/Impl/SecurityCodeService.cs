using Orchard.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Logging;
using Orchard.Validation;
using Oauth2OverOrchardCMS.Models;
using Oauth2OverOrchardCMS.Providers;
using System.Threading.Tasks;

namespace Oauth2OverOrchardCMS.Services
{
    public class SecurityCodeService:ISecurityCodeService
    {
        private readonly IRepository<SecurityCodeRecord> _repository;
        private readonly IEnumerable<ISecurityCodeProvider> _securityCodeProviders;
        private readonly ISMSChannel _smsChannel;

        public SecurityCodeService(
            IRepository<SecurityCodeRecord> repository,
            IEnumerable<ISecurityCodeProvider> securityCodeProviders,
            ISMSChannel smsChannel)
        {
            _repository = repository;
            _securityCodeProviders = securityCodeProviders;
            _smsChannel = smsChannel;
            Logger = NullLogger.Instance;
        }

        public ILogger Logger { get; set; }

        public Task<string> RequiredCode(string channel, string type)
        {
            var provider = GetSecurityCodeProvider(type);

            var record = GetCurrentCode(channel, type);
            if (record == null)
            {
                //不存在，生成
                record = new SecurityCodeRecord();
                record.Channel = channel;
                record.Code = GenerateCodeHelper.GetRandomDigital(provider.GetLenth());
                record.CodeType = type;
                record.CreateOn = DateTime.Now;
                record.ExpireTime = provider.GetExpireTime();
                record.ServiceProvider = _smsChannel.GetSMSProvider();
                record.IsValid = false;
                _repository.CreateRecord(record);
                _repository.Flush();
            }
            else
            {
                //存在，更新过期时间
                record.ExpireTime = provider.GetExpireTime();
                _repository.UpdateRecord(record);
                _repository.Flush();
            }

            return Task.FromResult(record.Code);
        }


        /// <summary>
        /// 验证码是否正确
        /// </summary>
        /// <param name="channel">通道号码</param>
        /// <param name="type">验证类型</param>
        /// <param name="code"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public Task<bool> CheckCode(string channel, string type, string code, bool update)
        {
            var record = GetCurrentCode(channel, type);
            if (record == null)
            {
                return Task.FromResult(false);
            }

            var success = String.Equals(code, record.Code, StringComparison.InvariantCultureIgnoreCase);
            if (success && update)
            {
                record.IsValid = true;//表示已认证，验证码失效
                _repository.UpdateRecord(record);//及时更新数据库
            }

            return Task.FromResult(success);
        }

        /// <summary>
        /// 查找验证码提供程序
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private ISecurityCodeProvider GetSecurityCodeProvider(string type)
        {
            var provider = _securityCodeProviders.Where(x => x.CanHandle(type))
                .OrderByDescending(x => x.Priority)
                .FirstOrDefault();

            if (provider == null)
                Argument.ThrowIfNull(provider,provider.ToString(),"未找到验证码提供程序");

            return provider;
        }


        /// <summary>
        /// 获取指定通道验证码
        /// </summary>
        /// <param name="channel">通道号码</param>
        /// <param name="type">验证码类型</param>
        /// <returns></returns>
        private SecurityCodeRecord GetCurrentCode(string channel, string type)
        {

            Argument.ThrowIfNullOrEmpty(channel, "channel");
            Argument.ThrowIfNullOrEmpty(type, "type");

            var now = DateTime.Now;

            try
            {
                var record = _repository.Fetch(m => m.Channel == channel && m.CodeType == type)
                .Where(m => m.IsValid == false && m.ExpireTime > now)
                .OrderByDescending(m => m.ExpireTime)
                .FirstOrDefault();

                return record;
            }
            catch (Exception ex) {
                Logger.Error(ex, "查询验证码时发生错误");

                return null;
            }
            
        }
    }
}