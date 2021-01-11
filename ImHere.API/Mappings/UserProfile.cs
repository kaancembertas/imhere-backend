// Author: Kaan Çembertaş 
// No: 200001684

using AutoMapper;
using ImHere.Entities;
using ImHere.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImHere.API.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterRequest, User> ();
        }
    }
}
