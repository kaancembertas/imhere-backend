using ImHere.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImHere.Models
{
    public class AttendenceInfoDto
    {
        public AttendenceInfoDto(Attendence attendence)
        {
            week = attendence.week;
            status = attendence.status;
        }

        public int week { get; set; }
        public int status { get; set; }
        
    }
}
