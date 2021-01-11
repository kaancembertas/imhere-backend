// Author: Kaan Çembertaş 
// No: 200001684

using ImHere.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ImHere.DataAccess.Abstract
{
    public interface IUserLectureRepository
    {
        public Task<List<UserLecture>> GetStudentLecturesByUserId(int id);
        public Task<bool> AddUserLectures(int userId, List<string> lectureCodes);
        public Task<bool> IsUserLectureExists(int userId, string lectureCode);
    }
}
