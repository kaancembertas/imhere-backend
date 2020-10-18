using ImHere.Entities;
using ImHere.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ImHere.Business.Abstract
{
    public interface IAuthenticationService
    {
        public Task<AuthenticationResponse> Authenticate(AuthenticationRequest model);
    }
}
