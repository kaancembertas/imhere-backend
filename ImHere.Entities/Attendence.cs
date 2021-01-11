// Author: Kaan Çembertaş 
// No: 200001684

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ImHere.Entities
{
    public class Attendence
    {
        public int user_id { get; set; }

        [Required]
        [StringLength(6)]
        public string lecture_code { get; set; }

        [Required]
        public int week { get; set; }

        // 0: Not Processed, 1: Joined, 2: Not Joined
        [Required]
        public int status { get; set; }
    }
}
