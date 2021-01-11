// Author: Kaan Çembertaş 
// No: 200001684

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ImHere.Models
{
    public class AuthenticationRequest
    {
        [Required]
        public string email { get; set; }

        [Required]
        public string password { get; set; }
    }
}
