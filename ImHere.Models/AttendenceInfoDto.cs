using ImHere.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace ImHere.Models
{
    public class AttendenceInfoDto
    {
        public AttendenceInfoDto(Attendence attendence,[AllowNull] string image_url)
        {
            week = attendence.week;
            status = attendence.status;
            this.image_url = image_url;
        }

        public int week { get; set; }
        public int status { get; set; }
        [AllowNull] public string image_url { get; set; }

    }
}
