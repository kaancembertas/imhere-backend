using ImHere.DataAccess.Abstract;
using ImHere.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImHere.DataAccess.Concrete
{
    public class UserLectureRepository : IUserLectureRepository
    {
        public async Task<List<UserLecture>> GetStudentLecturesByUserId(int userId)
        {
            using (var imHereDbContext = new ImHereDbContext())
            {
                return await imHereDbContext.UserLectures.Where(ul => ul.user_id == userId).ToListAsync();
            }
        }

        public async Task<bool> AddUserLectures(int userId, List<string> lectureCodes)
        {
            using (var imHereDbContext = new ImHereDbContext())
            {
                foreach (string lectureCode in lectureCodes)
                {
                    UserLecture userLecture = new UserLecture();
                    userLecture.lecture_code = lectureCode;
                    userLecture.user_id = userId;
                    imHereDbContext.UserLectures.Add(userLecture);
                }

                int numOfObjects = await imHereDbContext.SaveChangesAsync();
                return numOfObjects > 0;
            }
        }

        public async Task<bool> IsUserLectureExists(int userId, string lectureCode)
        {
            using (var imHereDbContext = new ImHereDbContext())
            {
                List<UserLecture> list = await imHereDbContext.UserLectures
                    .Where(ul => ul.user_id == userId && ul.lecture_code == lectureCode)
                    .ToListAsync();

                return list.Count != 0;
            }
        }

    }
}
