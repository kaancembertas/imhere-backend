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
    public class FaceInfoController : BaseController
    {
        IFaceInfoService _faceInfoService;
        IUserService _userService;
        ILectureService _lectureService;

        public FaceInfoController(
            IFaceInfoService faceInfoService,
            IUserService userService,
            ILectureService lectureService,
            IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _faceInfoService = faceInfoService;
            _userService = userService;
            _lectureService = lectureService;
        }

        [HttpGet("{lectureCode}")]
        [ProducesResponseType(typeof(FaceInfo), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> GetFaceInfos(string lectureCode)
        {

            
            User user = await _userService.GetUserById(AuthenticatedUser.Id);
            if (user.role != UserConstants.INSTRUCTOR)
            {
                return Unauthorized();
            }
            

            bool isLectureExists = await _lectureService.IsLectureExists(lectureCode);
            if (!isLectureExists)
                return NotFound(new ApiResponse("Lecture could not be found!"));

            
            bool isInstructorGivesLecture = await _lectureService.IsInstructorGivesLecture(user.id, lectureCode);
            if (!isInstructorGivesLecture)
            {
                return Unauthorized();
            }
            

            var faceInfos = await _faceInfoService.GetFaceInfos(lectureCode);
            return Ok(faceInfos);
        }
    }
}
