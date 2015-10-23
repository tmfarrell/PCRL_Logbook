using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PCRLLogbook
{
    public struct LogData
    {
        public string Mid;
        public int Station;
        public int Labroom; 
        public bool Misbehaved;
        public string Behavior;
        public bool StoolAbnormal;
        public string Stool;
        public bool BloodAbnormal;
        public string Blood;
        public bool EquipMalfunc;
        public string Equip;
        public string Comments;

        public LogData(string mid, int station, int labroom)
        {
            Mid = mid;
            Station = station;
            Labroom = labroom; 
            Misbehaved = false;
            Behavior = "";
            StoolAbnormal = false;
            Stool = "";
            BloodAbnormal = false;
            Blood = "";
            EquipMalfunc = false;
            Equip = "";
            Comments = "";
        }
        public void behaviorChecked(string behavior)
        {
            Misbehaved = true;
            Behavior = behavior;
        }
        public void stoolChecked(string stool)
        {
            StoolAbnormal = true;
            Stool = stool;
        }
        public void bloodChecked(string blood)
        {
            BloodAbnormal = true;
            Blood = blood;
        }
        public void equipChecked(string equip)
        {
            EquipMalfunc = true;
            Equip = equip;
        }
        public void recordComment(string comments)
        {
            Comments = comments;
        }

        public string toString()
        {
            string str = "Data for " + Mid + ":\n\n";

            if (Misbehaved)
                str += "Misbehaved: Yes.\nBehavior: " + Behavior + ".\n";
            else
                str += "Misbehaved: No.\n\n";

            if (BloodAbnormal)
                str += "Blood Abnormal: Yes.\nBlood: " + Blood + ".\n";
            else
                str += "Blood Abnormal: No.\n\n";

            if (StoolAbnormal)
                str += "Stool Abnormal: Yes.\nStool: " + Stool + ".\n";
            else
                str += "Stool Abnormal: No.\n\n";

            if (EquipMalfunc)
                str += "Equipment Malfunction: Yes.\nEquipment: " + Equip + ".\n";
            else
                str += "Equipment Malfunction: No.\n\n";

            return str;
        }
    }
}
