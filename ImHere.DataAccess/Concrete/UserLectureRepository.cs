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
        public async Task<List<UserLecture>> GetUserLecturesByUserId(int id)
        {
            using (var imHereDbContext = new ImHereDbContext())
            {
                return await imHereDbContext.UserLectures.Where(ul => ul.user_id == id).ToListAsync();
            }
        }

    }
}
