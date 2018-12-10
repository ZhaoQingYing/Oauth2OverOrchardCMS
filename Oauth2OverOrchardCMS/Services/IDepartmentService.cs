using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Oauth2OverOrchardCMS.Models;
using Oauth2OverOrchardCMS.DTO;

namespace Oauth2OverOrchardCMS.Services
{
    public interface IDepartmentService : Orchard.IDependency
    {
        Task<bool> AddDepartmentAsync(DepartmentDto dto);

        Task<IEnumerable<DepartmentPart>> GetDepartmentsAsync(int companyId);

        Task<DepartmentPart> GetDepartmentPartAsync(int deptId);

        void ClearCache();
    }
}
