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
        private ILectureService _lectureService;
        private IAttendenceService _attendenceService;
        private readonly IMapper _mapper;

        public UserController(
            IUserService userService,
            ILectureService lectureService,
            IAttendenceService attendenceService,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
            : base(httpContextAccessor)
        {
            _userService = userService;
            _lectureService = lectureService;
            _attendenceService = attendenceService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
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
        [ProducesResponseType(typeof(List<LectureInfoDto>), 200)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Lectures()
        {
            int userId = AutenticatedUser.Id;
            var userLectures = await _lectureService.GetUserLectures(userId);
            return Ok(userLectures);
        }

        [Authorize]
        [HttpGet]
        [Route("[action]/{lectureCode}")]
        [ProducesResponseType(typeof(List<AttendenceInfoDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Attendence(string lectureCode)
        {
            bool isLectureExists = await _lectureService.IsLectureExists(lectureCode);
            if (!isLectureExists)
                return NotFound(new ApiResponse("Lecture could not be found!"));

            int userId = AutenticatedUser.Id;
            List<AttendenceInfoDto> attendenceInfos = await _attendenceService.GetAttendencesInfo(userId, lectureCode);
            return Ok(attendenceInfos);

        }
    }
}
