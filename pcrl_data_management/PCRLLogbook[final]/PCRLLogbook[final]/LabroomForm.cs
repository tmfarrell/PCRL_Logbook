using System;
using System.IO; 
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using Newtonsoft.Json;
using System.Data.SQLite;
using System.Configuration; 
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;

namespace PCRLLogbook
{
    public partial class LabroomForm : Form
    {
        public string labMember; 
        public DateTime loginTime;
        public DateTime logoutTime; 
        public string labmemberName;
        public DateTime labroomCheckIn;
        public DateTime labroomCheckOut;
        public LoginForm Login = new LoginForm();
        public string[] LabroomNum = { "W715", "W716", "W718" };
        public Dictionary<string, LogData[]> Data = new Dictionary<string, LogData[]>();
        Dictionary<string, string> DBSchema = new Dictionary<string, string>() {
	        {"observation", "(date_time, lab_member, labroom, behavior, stool, training, " + 
                "blood, comments, equip_deficiency, weight, monkey)"},
            {"labmember", "(lab_member, first, last, password, cell_num)"}, 
            {"labroomlog", "(checkin, checkout, lab_member, labroom)"}, 
            {"loginlog", "(login, logout, lab_member)"}, 
	    };


        public LabroomForm()
        {
            InitializeComponent(); 
        } 
        
        public LabroomForm(LoginForm login)
        {
            InitializeComponent();

            // set various parameter and UI element values 
            Login = login;
            labMember = Login.username; 
            labmemberName = Login.name[0] + " " + Login.name[1]; 
            labmemberFillLabel.Text = labmemberName;
            loginTime = DateTime.Now;
            loginTimeFillLabel.Text = loginTime.ToShortTimeString() + " " + 
                                        loginTime.ToLongDateString(); 
        }

        public void storeData(string labroom, LogData[] monksdata)
        {
            Data[labroom] = new LogData[8]; 
            for (int i = 0; i < 8; i++)
            {
                Data[labroom][i] = monksdata[i]; 
            }
        } 
        private void labroomButton1_Click(object sender, EventArgs e)
        {
            labroomCheckIn = DateTime.Now; 

            //init logform for W715
            LogForm logForm = new LogForm(this, LabroomNum[0], Login);
            logForm.Show(); 
            Hide(); 
        }

        private void labroomButton2_Click(object sender, EventArgs e)
        {
            labroomCheckIn = DateTime.Now;

            //init logform for W716
            LogForm logForm = new LogForm(this, LabroomNum[1], Login);
            logForm.Show();
            Hide(); 
        }

        private void labroomButton3_Click(object sender, EventArgs e)
        {
            labroomCheckIn = DateTime.Now;

            //init logform for W718
            LogForm logForm = new LogForm(this, LabroomNum[2], Login);
            logForm.Show();
            Hide(); 
        }

        private void logoutButton_Click(object sender, EventArgs e)
        {
            logoutTime = DateTime.Now;

            // save the login and logout times to the DB's loginlog table 
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + 
                                                            Login.db_dir + ";Version=3;");
            m_dbConnection.Open();
            string sql = "INSERT INTO loginlog" + DBSchema["loginlog"] + " VALUES ('" + 
                labMember + "','" + loginTime + "','" + logoutTime + "')"; 
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            m_dbConnection.Close(); 

            // save observational data to DB
            saveDataToDB(); 

            Login.Show();
            Close(); 
        }

        public void setCheckOutTime(DateTime checkout, string room)
        {
            labroomCheckOut = checkout;

            // save the checkin and checkout times to the DB's labroomlog table 
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" +
                                                            Login.db_dir + ";Version=3;");
            m_dbConnection.Open();
            string sql = "INSERT INTO labroomlog" + DBSchema["labroomlog"] + " VALUES ('" + 
                labroomCheckIn + "','" + labroomCheckOut + "','" +
                labMember + "','" + room + "')";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            m_dbConnection.Close(); 
        }

        private void saveDataToDB()
        {
            // open DB connection and declare command 
            SQLiteConnection m_dbConnection = 
                new SQLiteConnection("Data Source=" + Login.db_dir + ";Version=3;");
            m_dbConnection.Open();

            string sql;
            SQLiteCommand command;

            foreach (string labroom in Data.Keys)
            {
                foreach (LogData log in Data[labroom])
                { 
                    if (log.notEmpty())
                    {
                        //MessageBox.Show(log.toString());
                        sql = "INSERT INTO observation" + DBSchema["observation"] + " VALUES ('" + 
                              DateTime.Now.ToString() + "','" + labMember + "','" + log.Labroom + 
                              "'," + log.Behavior + "," + log.Stool + "," + log.Training + "," + 
                              Convert.ToInt16(log.Blood) + ",'" + log.Comments + "'," + log.getEquip() + 
                              "," + log.Weight + ",'" + log.Mid + "')";
                        //MessageBox.Show(sql); 
                        command = new SQLiteCommand(sql, m_dbConnection);
                        command.ExecuteNonQuery(); 
                    }
                }
            }
            m_dbConnection.Close(); 
            MessageBox.Show("Save successful");
        }

        private void reviewSaveButton_Click(object sender, EventArgs e)
        {
            ReviewForm reviewForm = new ReviewForm(this);
            reviewForm.Show(); 
        }
    }
}
