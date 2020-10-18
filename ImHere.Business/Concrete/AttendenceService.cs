using ImHere.Business.Abstract;
using ImHere.DataAccess.Abstract;
using ImHere.DataAccess.Concrete;
using ImHere.Entities;
using ImHere.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ImHere.Business.Concrete
{
    public class AttendenceService :IAttendenceService
    {
        private IAttendenceRepository _attendenceRepository;
        public AttendenceService(IAttendenceRepository attendenceRepository)
        {
            _attendenceRepository = attendenceRepository;
        }

        public async Task<List<AttendenceInfoDto>> GetAttendencesInfo(int userId, string lectureCode)
        {
            List<AttendenceInfoDto> attendenceInfoList = new List<AttendenceInfoDto>();
            List<Attendence> attendences = await _attendenceRepository.GetAttendencesInfo(userId, lectureCode);

            foreach (Attendence attendence in attendences)
            {
                attendenceInfoList.Add(new AttendenceInfoDto(attendence));
            }

            for(int i = 0; i < 14 - attendences.Count; i++)
            {
                Attendence missingAttendence = new Attendence();
                missingAttendence.week = attendences.Count + 1 + i;
                missingAttendence.status = 0;
                attendenceInfoList.Add(new AttendenceInfoDto(missingAttendence));
            }

            return attendenceInfoList;
        }
    }
}
