// Author: Kaan Çembertaş 
// No: 200001684

using ImHere.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImHere.Models
{
    public class LectureInfoDto
    {
        public LectureInfoDto(Lecture lecture,User instructor)
        {
            lectureCode = lecture.code;
            lectureName = lecture.name;
            lectureStartDate = lecture.start_date;
            instructorName = instructor.name;
            instructorSurname = instructor.surname;
        }

        public string lectureCode { get; set; }
        public string lectureName { get; set; }
        public DateTime lectureStartDate { get; set; }
        public string instructorName { get; set; }
        public string instructorSurname { get; set; }
    }
}
