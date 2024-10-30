using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingSchoolApp.Models
{
    public class Status
    {
        public int StatusId { get; set; }
        public string StatusDescription { get; set; } = null!;
        public Status() { }
       
    }
}
