﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingSchoolApp.Models
{
    public class Package
    {
        public int PackageId { get; set; }
        public int ManagerId { get; set; }
        public string Title { get; set; } = null!;
        public string TheText { get; set; } = null!;

        public Package() { }
       


    }
}
