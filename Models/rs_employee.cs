using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rsEmployee.Models
{
    public class rs_employee
    {
        public string id { get; set; }
        public string name { get; set; }
        public string dept_id { get; set; }
        public string department { get; set; }
        public string dob { get; set; }
        public string mobile { get; set; }
        public string gender { get; set; }
        public string address { get; set; }
        public string isActive { get; set; }
    }
}