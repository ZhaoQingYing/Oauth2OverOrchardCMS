using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Oauth2OverOrchardCMS.Models;
using Oauth2OverOrchardCMS.DTO;


namespace Oauth2OverOrchardCMS.Services
{
    public interface IEmployeeService : Orchard.IDependency
    {
        Task<IEnumerable<EmployeePart>> GetEmployePartsAsync(int deptId);

        Task<ViewEmployeeDto> GetDetailEmployeAsync(int empId);

        Task<List<EmployeeRecord>> GetEmployesAsync();


        Task AddEmployeeAsync(EmployeDto dto);


        Task<object> UpdateEmployeAsync(EmployeDto dto);
        
    }
}
