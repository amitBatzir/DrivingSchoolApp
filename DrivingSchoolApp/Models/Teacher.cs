using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingSchoolApp.Models
{
    public class Teacher
    {
        public int UserTeacherId { get; set; }
        public string SchoolName { get; set; } = null!;
        public string TeacherEmail { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string TeacherPass { get; set; } = null!;
        public int TeacherStatus { get; set; }
        public string TeacherId { get; set; } = null!;
        public string WayToPay { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string ProfilePic { get; set; } = null!;
        public string DrivingTechnic { get; set; } = null!;
        public int ManagerId { get; set; }

        public Teacher() { }
       

    }
}
