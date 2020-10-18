using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImHere.Business.Abstract;
using ImHere.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImHere.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : BaseController
    {
        IAuthenticationService _authenticationService;

        public AuthenticationController(
            IHttpContextAccessor httpContextAccessor,
            IAuthenticationService authenticationService) : base(httpContextAccessor)
        {
            _authenticationService = authenticationService;
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(typeof(AuthenticationResponse), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<IActionResult> Authenticate(AuthenticationRequest authenticationModel)
        {
            AuthenticationResponse response = await _authenticationService.Authenticate(authenticationModel);

            if (response == null)
            {
                var error = new ApiResponse("Incorrect Email or Password");
                return BadRequest(error);
            }

            return Ok(response);
        }
    }
}
