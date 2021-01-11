// Author: Kaan Çembertaş 
// No: 200001684

using ImHere.DataAccess.Abstract;
using ImHere.Entities;
using ImHere.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImHere.DataAccess.Concrete
{
    public class LectureRepository : ILectureRepository
    {
        public async Task<Lecture> GetLectureByCode(string code)
        {
            using (var imHereDbContext = new ImHereDbContext())
            {
                return await imHereDbContext.Lectures.FirstOrDefaultAsync(l => l.code == code);
            }
        }

        public async Task<List<Lecture>> GetInstructorLecturesById(int userId)
        {
            using (var imHereDbContext = new ImHereDbContext())
            {
                return await imHereDbContext.Lectures.Where(l => l.instructor_id == userId).ToListAsync();
            }
        }

        public async Task<List<UserInfoDto>> GetStudentsByLecture(string lectureCode)
        {
            using (var imHereDbContext = new ImHereDbContext())
            {
                var students = await (from userLectures in imHereDbContext.UserLectures
                                      join user in imHereDbContext.Users
                                      on userLectures.user_id equals user.id
                                      where userLectures.lecture_code == lectureCode
                                      select new UserInfoDto(
                                          user.id,
                                          user.no,
                                          user.email,
                                          user.name,
                                          user.surname,
                                          user.role,
                                          user.image_url,
                                          user.isSelectedLecture)).ToListAsync();
                return students;
            }
        }

        public async Task<List<Lecture>> GetAllLectures()
        {
            using (var imHereDbContext = new ImHereDbContext())
            {
                return await imHereDbContext.Lectures.ToListAsync();
            }
        }

    }
}
