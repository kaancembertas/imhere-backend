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
        [ProducesResponseType(typeof(List<AttendenceInfoDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Attendence(string lectureCode)
        {
            bool isLectureExists = await _lectureService.IsLectureExists(lectureCode);
            if (!isLectureExists)
                return NotFound(new ApiResponse("Lecture could not be found!"));

            int userId = AutenticatedUser.Id;
            User user = await _userService.GetUserById(userId);

            if (user.role == UserConstants.INSTRUCTOR)
            {
                var info = await _attendenceService.GetAttendenceInfoForInstructor(lectureCode);
                return Ok(info);
            }

            List<AttendenceInfoDto> attendenceInfos = await _attendenceService.GetAttendencesInfo(userId, lectureCode);
            return Ok(attendenceInfos);
        }
    }
}
