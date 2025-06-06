﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DrivingSchoolApp.Models
{
    public class Student
    {
        public int UserStudentId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string SchoolName { get; set; } 
        public int StudentStatus { get; set; }
        public string StudentEmail { get; set; } = null!;
        public string StudentPass { get; set; } = null!;
        public string StudentLanguage { get; set; } 
        public DateTime DateOfTheory { get; set; }
        public int LengthOfLesson { get; set; }
        public int TeacherId { get; set; }
        public string DrivingTechnic { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string StudentId { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public int CurrentLessonNum { get; set; }=0;
        public bool InternalTestDone { get; set; }
        public string StudentAddress { get; set; } = null!;
        public string ProfilePic { get; set; } = null!;
        public int PackageId { get; set; }
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }  
        public string Details
        {
            get
            {
                return "שם מלא: " + FirstName + " " + LastName + ", " + "תעודת זהות:  " + StudentId + ", " + "\nאימייל:  " + StudentEmail + "תאריך לידה:" + DateOfBirth;
            }
        }
        public Student() { }
    }
}
