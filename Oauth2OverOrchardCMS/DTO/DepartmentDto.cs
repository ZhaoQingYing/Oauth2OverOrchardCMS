using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Oauth2OverOrchardCMS.DTO
{
    [Serializable]
    public class DepartmentDto
    {
        public int DeptId { get; set; }

        public int CompanyId { get; set; }

        public string Name { get; set; }

        public int Parent { get; set; }

        public string Desc { get; set; }
    }
}