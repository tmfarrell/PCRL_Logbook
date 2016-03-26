using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace PCRLLogbook
{
    public class Config
    {
        public int days { get; set; }
        public string data_dir {get; set;}
        public string report_dir { get; set; }
        public bool report_custom { get; set; }
        public int feeding_hour_threshold { get; set; }

        public string[] behavior_list { get; set; }
        public string[] equipment_list { get; set; }
        public string[] training_list { get; set; }
        public string[] stool_list { get; set; }

        public Dictionary<string, string> custom_dates { get; set; }
      
        public Dictionary<string, Dictionary<string, double>> feeders { get; set; }
        public Dictionary<string, Dictionary<string, string>> supplemental_feed_data { get; set; }
        public Dictionary<string, Dictionary<string, double>> supplemental_feed_templates { get; set; }
        public Dictionary<string, Dictionary<string, string>> monkey_data { get; set; }
        public Dictionary<string, Dictionary<string, string>> labmember_data { get; set; }

    }
}
