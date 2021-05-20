using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleaningSimReport.Models
{
    public class Report
    {
        public string report_id { get; set; }
        public string user_id { get; set; }
        public string time_seconds { get; set; }
        public string mistakes_count { get; set; }
        public string level_name { get; set; }
    }
}