using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingSchoolApp.Models
{
    public class Manager
    {
       
    
        public int UserManagerId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string ManagerEmail { get; set; } = null!;
        public string ManagerPass { get; set; } = null!;
        public int ManagerStatus { get; set; }
        public string ManagerId { get; set; } = null!;
        public string SchoolAddress { get; set; } = null!;
        public string ManagerPhone { get; set; } = null!;
        public string SchoolPhone { get; set; } = null!;
        public string Schoolname { get; set; } = null!;
        public string ProfilePic { get; set; } = null!;


        public Manager() { }
        public string Details
        {
            get
            {
                return "שם בית ספר: " + Schoolname + ", " + " שם מנהל:  " + FirstName + " " + LastName + "מספר טלפון של מנהל: " + ManagerPhone + "מספר טלפון של בית הספר: " + SchoolPhone + "כתובת בית הספר: " +SchoolAddress;
            }
        }

    }
}
