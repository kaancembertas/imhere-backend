using ImHere.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ImHere.Business.Abstract
{
    public interface ILectureService
    {
        public Task<List<LectureInfoDto>> GetStudentLectures(int id);
        public Task<List<LectureInfoDto>> GetInstructorLectures(int id);
        public Task<bool> IsLectureExists(string lectureCode);
    }
}
