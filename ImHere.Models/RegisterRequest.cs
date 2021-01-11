// Author: Kaan Çembertaş 
// No: 200001684

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ImHere.Models
{
    public class RegisterRequest
    {
        [Required]
        public string no { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string surname { get; set; }
        [Required]
        public string image_url { get; set; }
        [Required]
        public string face_encoding { get; set; }
    }
}
