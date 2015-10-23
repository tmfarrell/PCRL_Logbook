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
    public partial class LoginForm : Form
    {
        string password;
        public string username;
        public string[] name = new string[2];
        Dictionary<string, string[]> username_pwd = new Dictionary<string, string[]>();
        

        public LoginForm()
        {
            InitializeComponent();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            if (usernameTextbox.Text.Equals(string.Empty)) { 
                MessageBox.Show("Please fill in username and password fields."); 
                return; 
            } 
            
            // get username and password from form 
            username = usernameTextbox.Text;
            password = passwordTextbox.Text;

            // get all labmember data from db
            SQLiteConnection m_dbConnection = new SQLiteConnection("DataSource=PCRL_phizer_study.db;Version=3");
            m_dbConnection.Open(); 
            string sql = "SELECT * FROM labmember";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
                username_pwd[reader["lab_member"].ToString()] = new string[3] {reader["password"].ToString(), reader["first"].ToString(), reader["last"].ToString()};
            m_dbConnection.Close(); 

            // match labmember to un and pwd 
            var user =
                from labmember in username_pwd
                where labmember.Key.Equals(username) && labmember.Value[0].Equals(password)
                select labmember;

            // if no match 
            if (!user.Any())
                MessageBox.Show("Invalid username or password.\nPlease proceed to create an account.");
            else
            {   // store name and open labroom form 
                name[0] = username_pwd.First().Value[1];
                name[1] = username_pwd.First().Value[2];  
                LabroomForm labroomForm = new LabroomForm(this);
                labroomForm.Show();
                Hide();
            } 
        }

        private void createAcctButton_Click(object sender, EventArgs e)
        {
            CreateAccountForm createAcct = new CreateAccountForm(this);
            Hide();
            createAcct.Show(); 
        }
    }
}
