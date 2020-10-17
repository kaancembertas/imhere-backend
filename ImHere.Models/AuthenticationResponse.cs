using ImHere.Entities;
using System;

namespace ImHere.Models
{
    public class AuthenticationResponse
    {
        public int id { get; set; }
        public string no { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public int role { get; set; }
        public string image_url { get; set; }
        public string token { get; set; }

        public AuthenticationResponse(User user, string token)
        {
            id = user.id;
            no = user.no;
            email = user.email;
            name = user.name;
            surname = user.surname;
            role = user.role;
            image_url = user.image_url;
            this.token = token;
        }
    }
}
