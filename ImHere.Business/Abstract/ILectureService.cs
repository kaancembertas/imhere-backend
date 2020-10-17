using ImHere.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ImHere.Business.Abstract
{
    public interface ILectureService
    {
        public Task<List<LectureInfoDto>> GetUserLectures(int id);
        public Task<bool> IsLectureExists(string lectureCode);
    }
}
