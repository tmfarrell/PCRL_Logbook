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
        }
        public bool notEmpty()
        {
            return (Stool != "''" | Training != "''" | Comments != "''" | EquipMalfunc); 
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
        public void weightChecked(double weight)
        {
            Weight = weight; 
        }
        public void recordComment(string comments)
        {
            Comments = "'" + comments + "'";
        }

        public string getEquip()
        {
            string equip = "";
            foreach (string e in Equip)
                equip += e;
            return "'" + equip + "'"; 
        }

        public string toString()
        {
            string str = ""; 

            if (Misbehaved)
                str += "Behavior: " + Behavior + "; ";

            if (TrainingChecked)
                str += "Training: " + Training + "; "; 

            if (StoolAbnormal)
                str += "Stool: " + Stool + "; ";
            
            if (Blood)
                str += "Blood: present; ";

            if (Weight != 0)
                str += "Weight: " + Weight.ToString() + "; "; 

            if (Comments != "")
                str += "Comments: " + Comments + "; ";

            if (EquipMalfunc)
            {
                str += "Equipment: ";
                foreach (string e in Equip) 
                    if(e != null) str += e + "; ";   
            }
                
            return str;
        }

        public void updateFromString(string logDataStr)
        {
            char[] delim = { ':', ';' };
            string[] strs = logDataStr.Split(delim);

            string str = "";
            foreach (string st in strs) 
                str += st + " "; 
            MessageBox.Show(str);

            string s; 
            for (int i = 0; i < strs.Length; i++)
            {
                s = Regex.Replace(strs[i++], @"\s+", ""); 
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
                        Comments = strs[i++];
                        break; 
                    case "Equipment":
                        int j = 0; 
                        while (i < strs.Length)
                            Equip[j++] = strs[i++];
                        break; 
                }
            }
        }
    }
}
