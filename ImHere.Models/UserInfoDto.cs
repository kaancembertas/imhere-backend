using ImHere.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImHere.Models
{
    public class UserInfoDto
    {

        public int id { get; set; }
        public string no { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public int role { get; set; }
        public string image_url { get; set; }

        public UserInfoDto(User user)
        {
            id = user.id;
            no = user.no;
            email = user.email;
            name = user.name;
            surname = user.surname;
            role = user.role;
            image_url = user.image_url;
        }
    }
}