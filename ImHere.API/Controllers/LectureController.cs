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
    public class LectureController : BaseController
    {
        ILectureService _lectureService;
        IUserService _userService;

        public LectureController
            (
            ILectureService lectureService,
            IUserService userService,
            IHttpContextAccessor httpContextAccessor
            ) : base(httpContextAccessor)
        {
            _lectureService = lectureService;
            _userService = userService;
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(List<LectureInfoDto>), 200)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Lecture()
        {
            int userId = AutenticatedUser.Id;
            User user = await _userService.GetUserById(userId);

            if (user.role == UserConstants.INSTRUCTOR)
            {
                var instructorLectures = await _lectureService.GetInstructorLectures(userId);
                return Ok(instructorLectures);
            }

            var userLectures = await _lectureService.GetStudentLectures(userId);
            return Ok(userLectures);
        }
    }
}
