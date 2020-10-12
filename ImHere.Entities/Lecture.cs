using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ImHere.Entities
{
    class Lecture
    {
        [Key]
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
