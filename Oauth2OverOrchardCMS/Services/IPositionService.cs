using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Oauth2OverOrchardCMS.Models;
using Oauth2OverOrchardCMS.DTO;

namespace Oauth2OverOrchardCMS.Services
{
    public interface IPositionService : Orchard.IDependency
    {
        Task<List<PositionRecord>> GetPositionsAsync();

        Task AddPositionAsync(PositionDto dto);


        void ClearCache();
    }
}
