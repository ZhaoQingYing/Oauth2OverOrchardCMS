using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Oauth2OverOrchardCMS.DTO
{
    /// <summary>
    /// 加热炉配件
    /// </summary>
    [Serializable]
    public class HeatingFurnacePartDto
    {
        public HeatingFurnacePartDto() {
            Desc = string.Empty;
            IsActive = true;
        }

        public int PartId { get; set; }

        /// <summary>
        /// ERP编号
        /// </summary>
        public string ERPNumber { get; set; }

        /// <summary>
        /// 配件名称
        /// </summary>
        public string PartName { get; set; }

        /// <summary>
        /// 配件型号
        /// </summary>
        public string ModelName { get; set; }

        /// <summary>
        /// 配件描述
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 配件数量
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// 配件是否有效
        /// </summary>
        public bool IsActive { get; set; }
       
    }
}