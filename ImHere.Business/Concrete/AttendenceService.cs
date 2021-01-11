// Author: Kaan Çembertaş 
// No: 200001684

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
        private ILectureRepository _lectureRepository;
        private IAttendenceImageRepository _attendenceImageRepository;

        public AttendenceService(
            IAttendenceRepository attendenceRepository,
            ILectureRepository lectureRepository,
            IAttendenceImageRepository attendenceImageRepository)
        {
            _attendenceRepository = attendenceRepository;
            _lectureRepository = lectureRepository;
            _attendenceImageRepository = attendenceImageRepository;
        }

        public async Task<List<AttendenceInfoDto>> GetAttendencesInfo(int userId, string lectureCode)
        {
            int lastAttendenceWeek;
            AttendenceImage attendenceImage;
            string image_url;

            List<AttendenceInfoDto> attendenceInfoList = new List<AttendenceInfoDto>();
            List<Attendence> attendences = await _attendenceRepository.GetAttendencesInfo(userId, lectureCode);

            foreach (Attendence attendence in attendences)
            {
                attendenceImage = await _attendenceImageRepository.GetAttendenceImage(lectureCode, attendence.week);
                image_url = attendenceImage != null ? attendenceImage.image_url : null;

                attendenceInfoList.Add(new AttendenceInfoDto(attendence, image_url));
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
                attendenceImage = await _attendenceImageRepository.GetAttendenceImage(lectureCode,missingAttendence.week);
                image_url = attendenceImage != null ? attendenceImage.image_url : null;

                attendenceInfoList.Add(new AttendenceInfoDto(missingAttendence,image_url));
            }

            return attendenceInfoList;
        }

        public async Task<List<AttendenceInfoDto>> GetAttendenceInfoForInstructor(string lectureCode)
        {
            List<int> completedWeeks = await _attendenceRepository.GetCompletedAttendenceWeekInfo(lectureCode);
            List<AttendenceInfoDto> attendenceInfos = new List<AttendenceInfoDto>();
            AttendenceImage attendenceImage;
            string image_url;

            for (int i = 1; i <= 14; i++)
            {
                Attendence attendence = new Attendence();
                attendence.week = i;
                attendence.status = completedWeeks.Contains(i) ?
                    AttendenceConstants.COMPLETED :
                    AttendenceConstants.NOT_COMPLETED;


                attendenceImage = await _attendenceImageRepository.GetAttendenceImage(lectureCode, attendence.week);
                image_url = attendenceImage != null ? attendenceImage.image_url : null;
                attendenceInfos.Add(new AttendenceInfoDto(attendence,image_url));
            }

            return attendenceInfos;
        }

        public async Task<bool> IsAttendenceCompleted(string lectureCode,int week)
        {
            List<int> completedWeeks = await _attendenceRepository.GetCompletedAttendenceWeekInfo(lectureCode);
            return completedWeeks.Contains(week);
        }

        public async Task<bool> AddAttendence(string lectureCode, List<int> joinedUserIds, int week)
        {
            List<UserInfoDto> AllUsersInLecture = await _lectureRepository.GetStudentsByLecture(lectureCode);
            List<Attendence> AttendenceList = new List<Attendence>();

            foreach (UserInfoDto user in AllUsersInLecture)
            {
                Attendence attendence = new Attendence();
                attendence.week = week;
                attendence.user_id = user.id;
                attendence.lecture_code = lectureCode;
                attendence.status = joinedUserIds.Contains(user.id) ? AttendenceConstants.JOINED : AttendenceConstants.NOT_JOINED;
                AttendenceList.Add(attendence);
            }

            return await _attendenceRepository.AddAttendence(AttendenceList);
        }

        public async Task AddAttendenceImage(string lectureCode, int week, string image_url)
        {
            AttendenceImage attendenceImage = new AttendenceImage();
            attendenceImage.lectureCode = lectureCode;
            attendenceImage.week = week;
            attendenceImage.image_url = image_url;

            await _attendenceImageRepository.AddAttendenceImage(attendenceImage);
        }
    }
}
