using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Oauth2OverOrchardCMS.DTO
{
    [Serializable]
    public class SimpleCompanyDto
    {
        public int CompanyId { get; set; }

        public string CompanyName { get; set; }
    }
}