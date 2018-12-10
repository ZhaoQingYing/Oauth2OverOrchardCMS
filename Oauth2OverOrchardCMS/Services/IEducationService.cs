using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oauth2OverOrchardCMS.DTO;
using Oauth2OverOrchardCMS.Models;

namespace Oauth2OverOrchardCMS.Services
{
    public interface IEducationService:Orchard.IDependency
    {
        Task<List<EducationRecord>> GetEducationsAsync();

        Task AddEducationAsync(EducationDto dto);

        void ClearCache();
    }
}
