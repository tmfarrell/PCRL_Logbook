using System;
using System.Linq;
using System.Text;
using System.Windows.Forms; 
using System.Collections.Generic; 
using System.Text.RegularExpressions; 

namespace PCRLLogbook
{
    public struct LogData
    { 
        public string Mid;
        public bool Blood;
		public bool SuppFed;
        public string Stool;
        public double Weight;
        public string[] Equip;
        public string Station;
        public string Labroom; 
        public bool Misbehaved;
        public string Behavior;
        public string Training;
        public string Comments;
        public bool EquipMalfunc;
        public bool StoolAbnormal;
        public bool TrainingChecked;
		//public string SuppFeedTemplate; 
        public Dictionary<string, double> SuppFeed; 

        public LogData(string mid, string station, string labroom)
        {
            Mid = mid;
            Station = station;
            Labroom = labroom; 
            Misbehaved = false;
            Behavior = "''";
            TrainingChecked = false;
            Training = "''"; 
            StoolAbnormal = false;
            Stool = "''";
            Blood = false; 
            EquipMalfunc = false;
            Equip = new string[11];
            Weight = 0; 
            Comments = "''";
            SuppFed = false;
            //SuppFeedTemplate = "";
			SuppFeed = null; 
        }
        public bool notEmpty()
        {
            return (!this.toString().Equals("")); 
        }

        public void behaviorChecked(string behavior)
        {
            Misbehaved = true;
            Behavior = "'" + behavior + "'";
        }

        public void trainingChecked(string training)
        {
            TrainingChecked = true;
            Training = "'" + training + "'"; 
        }
        public void stoolChecked(string stool)
        {
            StoolAbnormal = true;
            Stool = "'" + stool + "'";
        }
        public void bloodChecked()
        {
            Blood = true;
        }
        public void equipChecked(string[] equip)
        {
            EquipMalfunc = true;
            Equip = equip;
        }
        public void recordSuppFed(Dictionary<string, double> suppfood) {
            if (suppfood != null)
            {
                SuppFed = true;
                SuppFeed = suppfood;
            } 
        }
        public void weightChecked(double weight)
        {
            Weight = weight; 
        }

        public string getEquip()
        {
            string equip = ""; string s; 
            for (int i = 0; i < Equip.Length; i++)
            {
                s = Equip[i];
                if (s != null && !s.Equals(""))
                {
                    if (i == 0) equip += s;
                    else equip += " + " + s;
                }
            }
            return "'" + equip + "'"; 
        }

        public string toString()
        {
            string str = ""; 

            if (Misbehaved)
                str += "Behavior: " + Behavior + ";\n";

            if (TrainingChecked)
                str += "Training: " + Training + ";\n"; 

            if (StoolAbnormal)
                str += "Stool: " + Stool + ";\n";
            
            if (Blood)
                str += "Blood: present;\n";

            if (Weight != 0)
                str += "Weight: " + Weight.ToString() + ";\n"; 

            if (Comments != "")
                str += "Comments: " + Comments + ";\n";

            if (SuppFed)
            {
                str += "Supplemental Food: "; //+ SuppFeedTemplate;
                foreach (KeyValuePair<string, double> entry in SuppFeed)
                {
                    str += "(" + entry.Key + ", " + Convert.ToString(entry.Value) + "), "; 
                }
                str = str.Substring(0, str.Length-2) + ";\n";
            } 
			
			string s; 
            if (EquipMalfunc)
            {
                str += "Equipment: ";
                for (int i = 0; i < Equip.Length; i++)
                {
                    s = Equip[i]; 
                    if (s != null && !s.Equals("")) {
                        if (i == 0) str += s;
                        else        str += ", " + s; 
                    }
                }
                str += ";\n"; 
            }
                
            return str;
        }

        public void updateFromString(string logDataStr)
        {
            char[] delim = { ':', ';' };
            string[] strs = logDataStr.Split(delim);

            //string str = "";
            //foreach (string st in strs) 
              //  str += st + " "; 
            //MessageBox.Show(str);

            string s; 
            for (int i = 0; i < strs.Length; )
            {
                s = Regex.Replace(strs[i++], @"\n", "");
                //MessageBox.Show(s); 
                switch (s)
                {
                    case "Behavior":
                        Behavior = strs[i++];
                        break; 
                    case "Training":
                        Training = strs[i++];
                        break;
                    case "Weight":
                        Weight = Convert.ToDouble(strs[i++]);
                        break;
                    case "Blood":
                        Blood = true;
                        i++; 
                        break; 
                    case "Comments":
                        Comments = strs[i++].Trim();
                        break; 
                    case "Equipment":
                        string[] es = strs[i++].Split(new char[] { ',' }); 
                        int j = 0; 
                        while (j < es.Length)
                            Equip[j] = es[j++].Trim();
                        break;
                    case "Supplemental Food":
                        //SuppFeedTemplate = strs[i++].Trim(); 
                        string[] sf = strs[i++].Split(new char[] {','});
                        int k = 0; int l = 0; 
                        string s_; string s__;
                        //MessageBox.Show(sf.Length.ToString()); 
                        while (k < sf.Length)
                        {
                            s_ = Regex.Replace(sf[k].Replace('(', ' '), @"\s+", "");
                            s__ = Regex.Replace(sf[k + 1].Replace(')', ' '), @"\s+", ""); 
                            SuppFeed[s_] = Convert.ToDouble(s__);
                            //MessageBox.Show(SuppFeed[k].toString()); 
                            //MessageBox.Show(s_ + s__); 
                            k = k + 2; 
                        }  
                        break; 
                    default:
                        i++;
                        break; 
                }
            }
            //MessageBox.Show(this.toString()); 
        }
    }
}
