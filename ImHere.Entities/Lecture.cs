// Author: Kaan Çembertaş 
// No: 200001684

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ImHere.Entities
{
    public class Lecture
    {
        [Key]
        [StringLength(6)]
        public string code { get; set; }

        [StringLength(50)]
        [Required]
        public string name { get; set; }

        [Required]
        public int instructor_id { get; set; }

        [Required]
        public DateTime start_date { get; set; }
    }
}
