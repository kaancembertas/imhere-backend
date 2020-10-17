using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ImHere.Entities
{
    public class UserLecture
    {
        public int user_id { get; set; }

        [StringLength(6)]
        public string lecture_code { get; set; }
    }
}
