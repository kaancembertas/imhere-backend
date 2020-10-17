using System;
using System.Collections.Generic;
using System.Text;

namespace ImHere.Models
{
    public class RegisterRequest
    {
        public string no { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string image_url { get; set; }
    }

}
