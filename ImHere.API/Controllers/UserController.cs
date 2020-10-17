﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ImHere.Business.Abstract;
using ImHere.Entities;
using ImHere.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImHere.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController : BaseController
    {
        private IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(
            IUserService userService,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
            : base(httpContextAccessor)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ApiResponse),400)]
        public async Task<IActionResult> Register([FromBody] RegisterRequest user)
        {
            bool IsEMailExists = await _userService.IsEmailExists(user.email);
            bool IsNoExists = await _userService.IsNoExists(user.no);

            if (IsEMailExists)
            {
                var err = new ApiResponse("Already registered with this Email!");
                return BadRequest(err);
            }

            if (IsNoExists)
            {
                var err = new ApiResponse("Already registered with this No!");
                return BadRequest(err);
            }


            User _user = _mapper.Map<User>(user);
            await _userService.CreateUser(_user);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(AuthenticationResponse), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<IActionResult> Authenticate(AuthenticationRequest authenticationModel)
        {
            AuthenticationResponse response = await _userService.Authenticate(authenticationModel);

            if (response == null)
            {
                var error = new ApiResponse("Incorrect Email or Password");
                return BadRequest(error);
            }

            return Ok(response);
        }

        [Authorize]
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(UserInfoDto), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Info()
        {
            var user = await _userService.GetUserInfoById(AutenticatedUser.Id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [Authorize]
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(401)]
        [ProducesResponseType(typeof(List<LectureInfoDto>), 200)]
        public async Task<IActionResult> Lectures()
        {
            int userId = AutenticatedUser.Id;
            var userLectures = await _userService.GetUserLectures(userId);
            return Ok(userLectures);
        }
    }
}
