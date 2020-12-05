using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ImHere.Entities
{
    public class AttendenceImage
    {
        [Required]
        [StringLength(6)]
        public string lectureCode { get; set; }

        [Required]
        public int week { get; set; }

        [Required]
        public string image_url { get; set; }
    }
}
