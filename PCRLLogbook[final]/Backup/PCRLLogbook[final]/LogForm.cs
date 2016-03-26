using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PCRLLogbook
{
    public partial class LogForm : Form
    {
        /* Fields */
        int labroomNum;
        int numMonks;   //number of monks in labroom
        LogData[] monksData = new LogData[8]; 
        LoginForm Login = new LoginForm();
        LabroomForm Labroom = new LabroomForm();

        /* Constructors */

        public LogForm() { } 
        
        public LogForm(LabroomForm labroom, int roomNum, LoginForm login)
        {
            InitializeComponent();

            Login = login;
            Labroom = labroom;
            labroomNum = roomNum; 

            labmemberFillLabel.Text = Login.name[0] + " " + Login.name[1];
            checkinFillLabel.Text = "Labroom " + labroomNum.ToString()
                + ", " + DateTime.Now.ToShortTimeString()
                + " " + DateTime.Now.ToLongDateString();

            PCRLLogbookDBDataSet1.MonkeyDataTable monkeys = this.monkeyTableAdapter.GetData();

            var monks =
                from monkey in monkeys
                where monkey.LabRoom.Equals(labroomNum)
                select monkey;

            int index = 0;
            numMonks = monks.ToArray().Length;
            int half = numMonks / 2; 

            foreach (var m in monks)
            {
                LogBox monkLogbox = new LogBox(m.MID, Convert.ToInt16(m.Station), labroomNum); 
                
                //add half to left, half to right 
                if (index < half)
                    this.leftLayoutPanel.Controls.Add(monkLogbox);
                else
                    this.rightLayoutPanel.Controls.Add(monkLogbox);
                index++;
            }
        }

        public LogData[] getData()
        {
            return monksData; 
        } 

        private void backButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show( "Are you sure you want to go back?\nThis will result in unsaved data.",
                "Warning", MessageBoxButtons.YesNo); 
            if (result == DialogResult.Yes) { 
                Labroom.Show();
                Close();
            } else { 
                return; 
            } 
        }

        private void saveCheckoutButton_Click(object sender, EventArgs e)
        {
            saveData(); 
            //saveDataToDB();
            Labroom.setCheckOutTime(DateTime.Now, labroomNum);
            Labroom.saveData(labroomNum, monksData); 
            Labroom.Show();
            Close(); 
        }

        private void saveData()
        {
            monksData = new LogData[numMonks];

            int index = 0;
            foreach (LogBox logBox in this.leftLayoutPanel.Controls)
                monksData[index++] = logBox.GetData();

            foreach (LogBox logBox in this.rightLayoutPanel.Controls)
                monksData[index++] = logBox.GetData();
        } 

        private void saveButton_Click(object sender, EventArgs e)
        {
            saveData(); 
        } 
    } 
}
