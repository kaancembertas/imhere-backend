// Author: Kaan Çembertaş 
// No: 200001684

using ImHere.Business.Abstract;
using ImHere.DataAccess.Abstract;
using ImHere.Entities;
using ImHere.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ImHere.Business.Concrete
{
    public class LectureService : ILectureService
    {
        private IUserLectureRepository _userLectureRepository;
        private ILectureRepository _lectureRepository;
        private IUserRepository _userRepository;

        public LectureService(
            IUserLectureRepository userLectureRepository,
            ILectureRepository lectureRepository,
            IUserRepository userRepository)
        {
            _userLectureRepository = userLectureRepository;
            _lectureRepository = lectureRepository;
            _userRepository = userRepository;
        }

        public async Task<List<LectureInfoDto>> GetStudentLectures(int userId)
        {
            List<LectureInfoDto> lectureInfos = new List<LectureInfoDto>();
            List<UserLecture> userLectures = await _userLectureRepository.GetStudentLecturesByUserId(userId);

            foreach (UserLecture userLecture in userLectures)
            {
                Lecture lecture = await _lectureRepository.GetLectureByCode(userLecture.lecture_code);
                User instructor = await _userRepository.GetUserById(lecture.instructor_id);
                if (lecture == null || instructor == null) continue;
                lectureInfos.Add(new LectureInfoDto(lecture, instructor));
            }

            return lectureInfos;
        }

        public async Task<List<LectureInfoDto>> GetInstructorLectures(int userId)
        {
            List<LectureInfoDto> lectureInfos = new List<LectureInfoDto>();
            List<Lecture> lectures = await _lectureRepository.GetInstructorLecturesById(userId);
            User instructor = await _userRepository.GetUserById(userId);

            foreach (Lecture lecture in lectures)
            {
                lectureInfos.Add(new LectureInfoDto(lecture, instructor));
            }

            return lectureInfos;
        }

        public async Task<bool> IsLectureExists(string lectureCode)
        {
            Lecture lecture = await _lectureRepository.GetLectureByCode(lectureCode);
            return lecture != null;
        }

        public async Task<List<UserInfoDto>> GetStudentsByLecture(string lectureCode)
        {
            return await _lectureRepository.GetStudentsByLecture(lectureCode);

        }

        public async Task<bool> SelectLectures(int userId, List<string> lectureCodes)
        {
            bool isSuccess = await _userLectureRepository.AddUserLectures(userId, lectureCodes);

            if (isSuccess)
            {
                await _userRepository.SetIsSelectedLectures(userId, isSuccess);
            }

            return isSuccess;
        }

        public async Task<List<LectureInfoDto>> GetAllLectures()
        {
            List<LectureInfoDto> lectureInfos = new List<LectureInfoDto>();
            List<Lecture> allLectures = await _lectureRepository.GetAllLectures();

            foreach (Lecture lecture in allLectures)
            {
                User instructor = await _userRepository.GetUserById(lecture.instructor_id);
                lectureInfos.Add(new LectureInfoDto(lecture, instructor));
            }

            return lectureInfos;
        }

        public async Task<bool> IsStudentTakesLecture(int userId, string lectureCode)
        {
            return await _userLectureRepository.IsUserLectureExists(userId, lectureCode);
        }

        public async Task<bool> IsInstructorGivesLecture(int userId, string lectureCode)
        {
            Lecture lecture = await _lectureRepository.GetLectureByCode(lectureCode);
            return lecture.instructor_id == userId;
        }
    }
}
