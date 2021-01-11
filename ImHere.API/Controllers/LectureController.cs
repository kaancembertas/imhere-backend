// Author: Kaan Çembertaş 
// No: 200001684

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
        [ProducesResponseType(typeof(List<LectureInfoDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Lecture()
        {
            int userId = AuthenticatedUser.Id;
            User user = await _userService.GetUserById(userId);

            if (user.role == UserConstants.INSTRUCTOR)
            {
                var instructorLectures = await _lectureService.GetInstructorLectures(userId);
                return Ok(instructorLectures);
            }

            var userLectures = await _lectureService.GetStudentLectures(userId);
            return Ok(userLectures);
        }

        [Authorize]
        [HttpGet]
        [Route("[action]/{lectureCode}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(List<UserInfoDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Students(string lectureCode)
        {
            int userId = AuthenticatedUser.Id;
            User user = await _userService.GetUserById(userId);
            bool isLectureExists = await _lectureService.IsLectureExists(lectureCode);

            if (user.role != UserConstants.INSTRUCTOR)
            {
                return Unauthorized();
            }
            if (!isLectureExists)
            {
                return NotFound(new ApiResponse("Lecture could not be found!"));
            }


            var studentList = await _lectureService.GetStudentsByLecture(lectureCode);
            return Ok(studentList);
        }

        [Authorize]
        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SelectLectures(List<string> lectureCodes)
        {
            User user = await _userService.GetUserById(AuthenticatedUser.Id);

            if (user.role == UserConstants.INSTRUCTOR)
            {
                return Unauthorized(new ApiResponse("Instructors cannot select lecture!"));
            }

            if (user.isSelectedLecture)
            {
                return BadRequest(new ApiResponse("User already selected lectures!"));
            }

            if (lectureCodes.Count == 0)
            {
                return BadRequest(new ApiResponse("There must be at least one lecture code!"));
            }

            foreach (string lectureCode in lectureCodes)
            {
                bool isExists = await _lectureService.IsLectureExists(lectureCode);
                if (!isExists)
                {
                    return BadRequest(new ApiResponse("Invalid lecture in lectureCodes!"));
                }
            }

            bool response = await _lectureService.SelectLectures(user.id, lectureCodes);
            if (response)
            {
                return NoContent();
            }
            else
            {
                return StatusCode(500, "Internal Server Error. Something went wrong when inserting to database!");
            }
        }

        [HttpGet]
        [Route("[action]")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(List<LectureInfoDto>),StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var lectures = await _lectureService.GetAllLectures();
            return Ok(lectures);
        }
    }
}
