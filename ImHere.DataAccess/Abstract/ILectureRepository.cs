using ImHere.Entities;
using ImHere.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ImHere.DataAccess.Abstract
{
    public interface ILectureRepository
    {
        public Task<Lecture> GetLectureByCode(string code);
        public Task<List<Lecture>> GetInstructorLecturesById(int userId);
        public Task<List<UserInfoDto>> GetStudentsByLecture(string lectureCode);
        public Task<List<Lecture>> GetAllLectures();
    }
}
