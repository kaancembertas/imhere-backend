// Author: Kaan Çembertaş 
// No: 200001684

using ImHere.Entities;
using System;

namespace ImHere.Models
{
    public class AuthenticationResponse
    {
        public string token { get; set; }
        public DateTime expireDate { get; set; }

        public AuthenticationResponse(string token,DateTime expireDate)
        { 
            this.token = token;
            this.expireDate = expireDate;
        }
    }
}
