using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Data.SQLite; 
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;

namespace PCRLLogbook
{
    public partial class LogForm : Form
    {
        int numMonks;                            //number of monkeys in labroom
        string labroom;
        LoginForm Login = new LoginForm();               
        LogData[] monksData = new LogData[8];    //array of data structs
        LabroomForm Labroom = new LabroomForm();
        Dictionary<string, string[]> monkeys = new Dictionary<string, string[]>(); 

        public LogForm() { } 
        
        public LogForm(LabroomForm labroomForm, string labroom, LoginForm login)
        {
            InitializeComponent();

            this.Login = login;
            this.Labroom = labroomForm;
            this.labroom = labroom; 

            labmemberFillLabel.Text = Login.name[0] + " " + Login.name[1];
            checkinFillLabel.Text = this.labroom + ", " + DateTime.Now.ToShortTimeString()
                                                 +  " " + DateTime.Now.ToLongDateString();

            // get all monkey data from db
            SQLiteConnection m_dbConnection = new SQLiteConnection("DataSource=PCRL_phizer_study.db;Version=3");
            m_dbConnection.Open();
            string sql = "SELECT * FROM monkey";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
                monkeys[reader["mid"].ToString()] = new string[2] {reader["station"].ToString(), reader["labroom"].ToString()}; 
            m_dbConnection.Close();

            // get monkeys for this labroom
            var monks =
                from monkey in monkeys
                where monkey.Value[1].Equals(this.labroom)
                select monkey;

            int index = 0;
            numMonks = monks.ToArray().Length;
            int half = numMonks / 2; 

            foreach (var m in monks)
            {
                LogBox monkLogbox = new LogBox(m.Key, m.Value[0], this.labroom); 
                
                //add half to left, half to right 
                if (index < half)
                    this.leftLayoutPanel.Controls.Add(monkLogbox);
                else
                    this.rightLayoutPanel.Controls.Add(monkLogbox);
                index++;
            }
        }

        //returns array of data structs
        public void printData()
        {
            string monksdata_str = ""; 
            foreach (LogData log in monksData)
                monksdata_str += log.toString();   
            DialogResult result = MessageBox.Show(monksdata_str);  
        } 

        private void backButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show( 
                "Are you sure you want to go back?\n" +
                "This will result in unsaved data.",
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
            //printData();  
            Labroom.setCheckOutTime(DateTime.Now, labroom);
            Labroom.storeData(labroom, monksData); 
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
            //printData(); 
        }
    } 
}
