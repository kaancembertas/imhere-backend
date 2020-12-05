using ImHere.DataAccess.Abstract;
using ImHere.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ImHere.DataAccess.Concrete
{
    public class AttendenceImageRepository : IAttendenceImageRepository
    {
        public async Task AddAttendenceImage(AttendenceImage attendenceImage)
        {
            using (var imHereDbContext = new ImHereDbContext())
            {
                await imHereDbContext.AddAsync(attendenceImage);
                await imHereDbContext.SaveChangesAsync();
            }
        }

        public async Task<AttendenceImage> GetAttendenceImage(string lectureCode, int week)
        {
            using (var imHereDbContext = new ImHereDbContext())
            {
                return await imHereDbContext
                    .AttendenceImages
                    .FirstOrDefaultAsync(ai => ai.lectureCode == lectureCode && ai.week == week);
            }
        }

    }
}
