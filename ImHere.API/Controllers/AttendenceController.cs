using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImHere.Business.Abstract;
using ImHere.Entities;
using ImHere.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImHere.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendenceController : BaseController
    {
        private IAttendenceService _attendenceService;
        private ILectureService _lectureService;
        private IUserService _userService;

        public AttendenceController
            (
                IHttpContextAccessor httpContextAccessor,
                IAttendenceService attendenceService,
                ILectureService lectureService,
                IUserService userService

            ) : base(httpContextAccessor)
        {
            _attendenceService = attendenceService;
            _lectureService = lectureService;
            _userService = userService;
        }

        [Authorize]
        [HttpGet("{lectureCode}")]
        [ProducesResponseType(typeof(List<AttendenceInfoDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Attendence(string lectureCode)
        {
            bool isLectureExists = await _lectureService.IsLectureExists(lectureCode);
            if (!isLectureExists)
                return NotFound(new ApiResponse("Lecture could not be found!"));

            int userId = AuthenticatedUser.Id;
            User user = await _userService.GetUserById(userId);

            if (user.role == UserConstants.INSTRUCTOR)
            {
                var info = await _attendenceService.GetAttendenceInfoForInstructor(lectureCode);
                return Ok(info);
            }

            List<AttendenceInfoDto> attendenceInfos = await _attendenceService.GetAttendencesInfo(userId, lectureCode);
            return Ok(attendenceInfos);
        }

        [Authorize]
        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AttendenceInfoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AttendenceByUser([FromBody] AttendenceByUserRequest request)
        {
            User user = await _userService.GetUserById(AuthenticatedUser.Id);
            if (user.role != UserConstants.INSTRUCTOR) return Unauthorized();

            bool isUserExists = await _userService.IsUserExists(request.userId);
            if (!isUserExists)
            {
                return NotFound(new ApiResponse("Student could not found!"));
            }

            bool isLectureExists = await _lectureService.IsLectureExists(request.lectureCode);
            if (!isLectureExists)
            {
                return NotFound(new ApiResponse("Lecture could not be found!"));
            }

            List<AttendenceInfoDto> attendenceInfos = await _attendenceService.GetAttendencesInfo(request.userId, request.lectureCode);
            return Ok(attendenceInfos);
        }
    }
}
