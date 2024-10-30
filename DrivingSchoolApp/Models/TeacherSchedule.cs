using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingSchoolApp.Models
{
    public class TeacherSchedule
    {
        public int ScheduleId { get; set; }
        public string DayOfSchedule { get; set; } = null!;
        public string Beginning { get; set; } = null!;
        public string LessonLength { get; set; } = null!;
        public string Ending { get; set; } = null!;
        public int TeacherId { get; set; }
        public TeacherSchedule() { }
       
    }
}
