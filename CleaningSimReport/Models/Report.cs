using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleaningSimReport.Models
{
    public class Report
    {
        public string Report_id { get; set; }
        public string User_id { get; set; }
        public string Time_seconds { get; set; }
        public string Mistakes_count { get; set; }
        public string Level_name { get; set; }
        public string Creation_datetime { get; set; }
    }
}