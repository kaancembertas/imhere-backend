using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImHere.Business.Abstract;
using ImHere.Entities;
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

        public FaceInfoController(
            IFaceInfoService faceInfoService,
            IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _faceInfoService = faceInfoService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<List<FaceInfo>> GetFaceInfos()
        {
            return await _faceInfoService.GetFaceInfos();
        }
    }
}
