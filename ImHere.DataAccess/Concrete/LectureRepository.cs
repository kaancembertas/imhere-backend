using ImHere.DataAccess.Abstract;
using ImHere.Entities;
using ImHere.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

    }
}
