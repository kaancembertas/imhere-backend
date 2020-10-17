using ImHere.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImHere.API.Controllers
{
    public class BaseController : ControllerBase
    {
        public AuthenticatedUserData AutenticatedUser;

        public BaseController(IHttpContextAccessor httpContextAccessor)
        {
            var UserId = httpContextAccessor.HttpContext.Items["UserId"];
            if (UserId != null)
                AutenticatedUser = new AuthenticatedUserData((int)UserId);
        }
    }
}
