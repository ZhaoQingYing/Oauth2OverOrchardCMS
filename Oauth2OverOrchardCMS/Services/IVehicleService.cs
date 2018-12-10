using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Oauth2OverOrchardCMS.Models;
using Oauth2OverOrchardCMS.DTO;

namespace Oauth2OverOrchardCMS.Services
{
    public interface IVehicleService : Orchard.IDependency
    {
        /// <summary>
        /// 添加一辆车信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task AddVehicleAsync(VehicleDto dto);

        /// <summary>
        /// 获取所有车辆
        /// </summary>
        /// <param name="available">默认可用</param>
        /// <returns></returns>
        Task<List<VehicleDto>> GetVehiclesAsync(bool available = true);

        /// <summary>
        /// 更新一辆车信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(VehicleDto dto);
    }
}
