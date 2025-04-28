using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingSchoolApp.Models
{
    public class Lesson
    {
        public int LessonId { get; set; }
        public DateTime DateOfLesson { get; set; }  
        public int StudentId { get; set; }
        public int TeacherId { get; set; }
        public string PickUpLoc { get; set; } = null!;
        public string DropOffLoc { get; set; } = null!;
        public int StatusId { get; set; }
        public Student? Student { get; set; }
        public Lesson() { }
      public string Details
        {
            get
            {
                return "השיעור שלך בתאריך " + DateOfLesson;
            }
        }
    }

    public class LessonStatuses
    {
        public int StatusId { get; set; }

        public string StatusDescription { get; set; } = null!;
    }
}
