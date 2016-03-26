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
        public Dictionary<string, bool> recorded = new Dictionary<string, bool>() {
            {"W715", false}, 
            {"W716", false}, 
            {"W718", false}
        }; 
        public Dictionary<string, LogData[]> Data = new Dictionary<string, LogData[]>();
        Dictionary<string, string> DBSchema = new Dictionary<string, string>() {
	        {"observation", "(date_time, lab_member, labroom, behavior, stool, training, " + 
                "comments, equip, monkey)"},
            {"labmember", "(lab_member, first, last, password, cell_num)"}, 
            {"labroomlog", "(checkin, checkout, lab_member, labroom)"}, 
            {"loginlog", "(login, logout, lab_member)"}
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
            if (recorded[LabroomNum[0]])
            {
                MessageBox.Show("Data was already entered for " + LabroomNum[0] +
                    ".\nPlease see Review and Save to change previously recorded data.");
            }
            else
            {
                //init logform for W715
                LogForm logForm = new LogForm(this, LabroomNum[0], Login);
                logForm.Show();
                Hide();
            } 
        }

        private void labroomButton2_Click(object sender, EventArgs e)
        {
            labroomCheckIn = DateTime.Now;
            if (recorded[LabroomNum[1]])
            {
                MessageBox.Show("Data was already entered for " + LabroomNum[1] +
                    ".\nPlease see Review and Save to change previously recorded data.");
            }
            else
            {
                //init logform for W716
                LogForm logForm = new LogForm(this, LabroomNum[1], Login);
                logForm.Show();
                Hide();
            } 
        }

        private void labroomButton3_Click(object sender, EventArgs e)
        {
            labroomCheckIn = DateTime.Now;

            if (recorded[LabroomNum[2]]) {
                MessageBox.Show("Data was already entered for " + LabroomNum[2] + 
                    ".\nPlease see Review and Save to change previously recorded data."); 
            } else {
                //init logform for W718
                LogForm logForm = new LogForm(this, LabroomNum[2], Login);
                logForm.Show();
                Hide(); 
            } 
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
                    //MessageBox.Show(log.notEmpty().ToString()); 
                    //MessageBox.Show(log.toString());
                    if (log.notEmpty())
                    {
                        //MessageBox.Show(log.toString());
                        // record observations 
                        sql = "INSERT INTO observation" + DBSchema["observation"] + " VALUES ('" + 
                              DateTime.Now.ToString() + "','" + labMember + "','" + log.Labroom + 
                              "'," + log.Behavior + "," + log.Stool + "," + log.Training + ",'" + 
                              log.Comments + "'," + log.getEquip() + ",'" + log.Mid + "')";
                        //MessageBox.Show(sql); 
                        command = new SQLiteCommand(sql, m_dbConnection);
                        command.ExecuteNonQuery(); 

                        // record supplemental feed 
                        if (log.SuppFed)
                        {
                            foreach (KeyValuePair<string, double> sf in Login.config.supplemental_feed_templates[log.SuppFeedTemplate])
                            {
                                sql = "INSERT INTO supp_feed(date_time, labmember, name, amount, mid) " +
                                      "VALUES ('" + DateTime.Now.ToString() + "', '" + labMember + "', '" +
                                      sf.Key + "', " + Convert.ToString(sf.Value) + ", '" + log.Mid + "')";
                                command = new SQLiteCommand(sql, m_dbConnection);
                                command.ExecuteNonQuery(); 
                            }
                        }  
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

        private void generateReportsButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "C:\\Python27\\python.exe";
            startInfo.Arguments = Login.base_dir + "GenerateDailyReports.py";
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            process.StartInfo = startInfo;
            process.Start();
        }

        public void dataRecorded(string labroom)
        {
            recorded[labroom] = true; 
        }
    }
}
