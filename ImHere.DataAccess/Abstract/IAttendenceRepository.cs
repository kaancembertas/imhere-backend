// Author: Kaan Çembertaş 
// No: 200001684

using ImHere.Entities;
using ImHere.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ImHere.DataAccess.Abstract
{
    public interface IAttendenceRepository
    {
        public Task<List<Attendence>> GetAttendencesInfo(int userId, string lectureCode);

        // Returns list of week that the attendence is processed for spesific lecture
        public Task<List<int>> GetCompletedAttendenceWeekInfo(string lectureCode);

        public Task<bool> AddAttendence(List<Attendence> attendences);
    }
}
