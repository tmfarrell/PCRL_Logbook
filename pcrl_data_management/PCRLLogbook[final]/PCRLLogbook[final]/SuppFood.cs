using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PCRLLogbook
{
    public class SuppFood
    {
        string name;
        double amount;

        public SuppFood() { } 

        public SuppFood(string name_, double amt)
        {
            name = name_;
            amount = amt; 
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public double Amount
        {
            get { return amount; }
            set { amount = value; }
        } 
    }
}
