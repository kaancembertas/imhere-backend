// Author: Kaan Çembertaş 
// No: 200001684

using ImHere.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ImHere.Business.Abstract
{
    public interface IAttendenceService
    {
        public Task<List<AttendenceInfoDto>> GetAttendencesInfo(int userId, string lectureCode);
        public Task<List<AttendenceInfoDto>> GetAttendenceInfoForInstructor(string lectureCode);
        public Task<bool> AddAttendence(string lectureCode, List<int> joinedUserIds, int week);
        public Task<bool> IsAttendenceCompleted(string lectureCode, int week);
        public Task AddAttendenceImage(string lectureCode, int week, string image_url);
    }
}
