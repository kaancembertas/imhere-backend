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
    public class AttendenceService : IAttendenceService
    {
        private IAttendenceRepository _attendenceRepository;
        public AttendenceService(IAttendenceRepository attendenceRepository)
        {
            _attendenceRepository = attendenceRepository;
        }

        public async Task<List<AttendenceInfoDto>> GetAttendencesInfo(int userId, string lectureCode)
        {
            int lastAttendenceWeek;
            List<AttendenceInfoDto> attendenceInfoList = new List<AttendenceInfoDto>();
            List<Attendence> attendences = await _attendenceRepository.GetAttendencesInfo(userId, lectureCode);

            foreach (Attendence attendence in attendences)
            {
                attendenceInfoList.Add(new AttendenceInfoDto(attendence));
            }

            if (attendences.Count != 0)
            {
                lastAttendenceWeek = attendences[attendences.Count - 1].week;
            }
            else
            {
                lastAttendenceWeek = 0;
            }

            for (int i = lastAttendenceWeek; i < 14; i++)
            {
                Attendence missingAttendence = new Attendence();
                missingAttendence.week = i + 1;
                missingAttendence.status = AttendenceConstants.NOT_PROCESSED;
                attendenceInfoList.Add(new AttendenceInfoDto(missingAttendence));
            }

            return attendenceInfoList;
        }

        public async Task<List<AttendenceInfoDto>> GetAttendenceInfoForInstructor(string lectureCode)
        {
            List<int> completedWeeks = await _attendenceRepository.GetCompletedAttendenceWeekInfo(lectureCode);
            List<AttendenceInfoDto> attendenceInfos = new List<AttendenceInfoDto>();

            for (int i = 1; i <= 14; i++)
            {
                Attendence attendence = new Attendence();
                attendence.week = i;
                attendence.status = completedWeeks.Contains(i) ?
                    AttendenceConstants.COMPLETED :
                    AttendenceConstants.NOT_COMPLETED;
                attendenceInfos.Add(new AttendenceInfoDto(attendence));
            }

            return attendenceInfos;
        }
    }
}
