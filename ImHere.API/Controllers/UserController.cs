﻿// Author: Kaan Çembertaş 
// No: 200001684

using System;
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
        private IFaceInfoService _faceInfoService;
        private readonly IMapper _mapper;

        public UserController
            (
            IUserService userService,
            IFaceInfoService faceInfoService,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper
            ) : base(httpContextAccessor)
        {
            _userService = userService;
            _faceInfoService = faceInfoService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
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
            await _faceInfoService.CreateFaceInfo(_user.id, user.face_encoding);
            return NoContent();
        }

        [Authorize]
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(UserInfoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Info()
        {
            var user = await _userService.GetUserInfoById(AuthenticatedUser.Id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }


        [AllowAnonymous]
        [ProducesResponseType(typeof(CheckExistsResponse), StatusCodes.Status200OK)]
        [HttpGet]
        [Route("check/{email}")]
        public async Task<IActionResult> checkEmail(string email)
        {
            bool isExists = await _userService.IsEmailExists(email);
            return Ok(new CheckExistsResponse(isExists));
        }
    }
}
