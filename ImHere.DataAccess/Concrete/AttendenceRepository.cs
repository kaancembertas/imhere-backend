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
    public class AttendenceRepository : IAttendenceRepository
    {
        public async Task<List<Attendence>> GetAttendencesInfo(int userId, string lectureCode)
        {
            using (var imHereDbContext = new ImHereDbContext())
            {
                return await imHereDbContext.Attendences.Where(a =>
                a.user_id == userId &&
                a.lecture_code == lectureCode)
                    .OrderBy(a => a.week)
                    .ToListAsync();
            }
        }

        public async Task<List<int>> GetCompletedAttendenceWeekInfo(string lectureCode)
        {
            using (var imHereDbContext = new ImHereDbContext())
            {
                var weeks = await imHereDbContext.Attendences
                    .Where(a => a.lecture_code == lectureCode)
                    .Select(a => a.week)
                    .Distinct()
                    .ToListAsync();

                return weeks;
            }
        }

        public async Task<bool> AddAttendence(List<Attendence> attendences)
        {
            using(var imHereDbContext = new ImHereDbContext())
            {
                await imHereDbContext.AddRangeAsync(attendences);
                int numOfChanges = await imHereDbContext.SaveChangesAsync();
                return numOfChanges > 0;
            }
        }
    }
}
