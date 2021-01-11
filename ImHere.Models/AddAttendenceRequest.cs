// Author: Kaan Çembertaş 
// No: 200001684

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
        public string image_url { get; set; }
    }
}
