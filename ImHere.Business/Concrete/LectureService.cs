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
    }
}
