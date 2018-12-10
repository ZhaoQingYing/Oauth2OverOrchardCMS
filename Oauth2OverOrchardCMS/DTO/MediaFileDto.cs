using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Orchard.Security;

namespace Oauth2OverOrchardCMS.DTO
{
    [Serializable]
    public class MediaFileDto
    {
        public int UserId { get; set; }

        public string UserName { get;set; }

        /// <summary>
        /// 业务类型
        /// </summary>
        public string BusinessType { get; set; }

        public string FileName { get; set; }
        public Stream Stream { get; set; }
    }
}