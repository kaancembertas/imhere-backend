using System;
using System.Collections.Generic;
using System.Text;

namespace ImHere.Models
{
    public class AddAttendenceRequest
    {
        public string lectureCode { get; set; }
        public int week { get; set; }
        public List<int> userIds { get; set; }
    }
}
