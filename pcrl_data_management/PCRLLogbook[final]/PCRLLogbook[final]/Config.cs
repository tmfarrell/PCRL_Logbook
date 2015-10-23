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
        public int feeding_hour_threshold { get; set; }

        public IList<double> dispense_amount { get; set; }
        public IList<double> feeder_caloric_density { get; set; }

        public Dictionary<string, double> supp_feed_template { get; set; }

    }
}
