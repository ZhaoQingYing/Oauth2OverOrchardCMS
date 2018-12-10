using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Oauth2OverOrchardCMS.Models;
using Oauth2OverOrchardCMS.DTO;

namespace Oauth2OverOrchardCMS.Services
{
    public interface ICompanyService : Orchard.IDependency
    {
        Task<bool> AddCompanyAsync(CompanyDto dto);

        Task<List<SimpleCompanyDto>> GetSimpleCompanysAsync();

        Task<List<CompanyRecord>> GetCompanysAsync();

        Task<CompanyPart> GetCompanyPartAsync(int Id);

        void ClearCache();
    }
}
