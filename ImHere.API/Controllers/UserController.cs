using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImHere.Business.Abstract;
using ImHere.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImHere.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService userService;
        public UserController(IUserService _userService)
        {
            userService = _userService;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            await userService.CreateUser(user);
            return Ok();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await userService.GetAllUsers();
            return Ok(users);
        }
    }
}
