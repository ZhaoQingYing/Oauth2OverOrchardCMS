using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oauth2OverOrchardCMS.Models;

namespace Oauth2OverOrchardCMS.DTO
{
    public class PositionDto
    {
        public int PositionId { get; set; }

        public  string Name { get; set; }

        public  DateTime? EntryTime { get; set; }

        public  WorkingState WorkingState { get; set; }


        public  string LeaveReason { get; set; }


        public  DateTime? LeaveTime { get; set; }



        public  DateTime? ContractStart { get; set; }

       
        public  DateTime? ContractEnd { get; set; }
    }
}