using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Oauth2OverOrchardCMS.DTO
{
    public class EmployeDto
    {
        public int EmpId { get; set; }

        public string WorkId { get; set; }

        public string FullName { get; set; }

        public string MobilePhone { get; set; }

        public  int Sex { get; set; }

        public  string IdNumber { get; set; }

        public  DateTime? BirthDate { get; set; }

        public  string FixedPhone { get; set; }

        public  int? MarriageState { get; set; }

        public int EduId { get; set; }

        public int DeptId { get; set; }

        public int PositionId { get; set; }


    }
}