using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingSchoolApp.Models
{
    public class Test
    {
        public int TestId { get; set; }
        public int StudentId { get; set; }
        public int ManagerId { get; set; }
        public DateTime DateOfTest { get; set; }
        public bool PassedOrNot { get; set; }
        public string Comments { get; set; } = null!;
        public Test() { }
       

    }
}
