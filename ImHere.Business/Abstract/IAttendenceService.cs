using ImHere.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ImHere.Business.Abstract
{
    public interface IAttendenceService
    {
        public Task<List<AttendenceInfoDto>> GetAttendencesInfo(int userId, string lectureCode);
    }
}
