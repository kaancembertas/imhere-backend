using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ImHere.Models
{
    public class AttendenceByUserRequest
    {
        [Required]
        public string lectureCode { get; set; }

        [Required]
        public int userId { get; set; }
    }
}
