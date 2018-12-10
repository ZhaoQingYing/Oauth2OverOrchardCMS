using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Oauth2OverOrchardCMS.DTO
{
    [Serializable]
    public class VehicleDto
    {
        public VehicleDto() {
            IsActive = true;
        }

        public  int VehicleId { get; set; }

        /// <summary>
        /// 司机
        /// </summary>
        public  string Driver { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public  string VehicleLicence { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsActive { get; set; }
    }
}