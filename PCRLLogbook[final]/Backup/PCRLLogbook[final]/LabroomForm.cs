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
    public partial class LabroomForm : Form
    {
        public LoginForm Login = new LoginForm(); 
        public DateTime loginTime;
        public DateTime logoutTime; 
        public DateTime labroomCheckin;
        public DateTime labroomCheckout; 
        public string labMember;
        public string labmemberName; 
        public LogData[,] Data = new LogData[3,8]; 

        public LabroomForm()
        {
            InitializeComponent(); 
        } 
        
        public LabroomForm(LoginForm login)
        {
            InitializeComponent();
            
            Login = login;

            labMember = Login.user; 
            labmemberName = Login.name[0] + " " + Login.name[1]; 
            labmemberFillLabel.Text = labmemberName;
            loginTime = DateTime.Now;
            loginTimeFillLabel.Text = loginTime.ToShortTimeString() + " " + loginTime.ToLongDateString(); 
        }

        public void saveData(int labroom, LogData[] monksdata)
        {
            for(int i = 0; i < monksdata.Length; i++)  
                Data[labroom, i] = monksdata[i]; 
        } 

        private void labroomButton1_Click(object sender, EventArgs e)
        {
            labroomCheckin = DateTime.Now; 

            LogForm logForm = new LogForm(this, 1, Login);
            logForm.Show(); 
            Hide(); 
            
        }

        private void labroomButton2_Click(object sender, EventArgs e)
        {
            labroomCheckin = DateTime.Now;

            LogForm logForm = new LogForm(this, 2, Login);
            logForm.Show();
            Hide(); 
        }

        private void labroomButton3_Click(object sender, EventArgs e)
        {
            labroomCheckin = DateTime.Now;

            LogForm logForm = new LogForm(this, 3, Login);
            logForm.Show();
            Hide(); 
        }

        private void logoutButton_Click(object sender, EventArgs e)
        {
            logoutTime = DateTime.Now;

            loginLogTableAdapter.Insert(labMember, loginTime, logoutTime, 
                logoutTime.Date);
            loginLogTableAdapter.Update(pcrlLogbookDBDataSet);

            saveDataToDB(); 

            Login.Show();
            Close(); 
        }

        public void setCheckOutTime(DateTime datetime, int room)
        {
            labroomCheckout = datetime;

            labroomLogTableAdapter.Insert(labMember, room, labroomCheckin,
                labroomCheckout, DateTime.Now.Date);
            labroomLogTableAdapter.Update(pcrlLogbookDBDataSet); 
        }

        private void saveDataToDB()
        {
            foreach (LogData log in Data)
            {
                this.observationsTableAdapter.Insert(log.Mid, labMember, DateTime.Now,
                    log.Comments, log.Behavior, log.Blood, log.BloodAbnormal, log.Misbehaved,
                    log.Stool, log.StoolAbnormal);
            }

            try
            {
                this.Validate();
                this.observationsTableAdapter.Update(this.pcrlLogbookDBDataSet);
                MessageBox.Show("Save successful");
            }
            catch
            {
                MessageBox.Show("Save failed");
            }
        }

        private void reviewSaveButton_Click(object sender, EventArgs e)
        {
            ReviewForm reviewForm = new ReviewForm(this);
            reviewForm.Show(); 
        }
    }
}
