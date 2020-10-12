using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ImHere.Entities
{
    public class Attendence
    {
        [Key]
        public int user_id { get; set; }

        [Required]
        public string lecture_code { get; set; }

        // 0: Not Processed, 1: Joined, 2: Not Joined
        [Required]
        public int status { get; set; }
    }
}
