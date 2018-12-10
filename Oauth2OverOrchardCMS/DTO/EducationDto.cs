using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Oauth2OverOrchardCMS.DTO
{
    [Serializable]
    public class EducationDto
    {
        public int EduId { get; set; }

        public string GraduateSchool { get; set; }

        public string Major { get; set; }

        public string EducationName { get; set; }
    }
}