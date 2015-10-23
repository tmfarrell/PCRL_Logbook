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
    public partial class CreateAccountForm : Form
    {
        string password;
        public string cellNum;
        public string username;
        public LoginForm Login;
        public string[] name = new string[2];
        Dictionary<string, string[]> labmembers = new Dictionary<string, string[]>();

        public CreateAccountForm(LoginForm login)
        {
            InitializeComponent();
            Login = login; 
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            //if textboxes filled properly, init fields
            if (labMemberTextBox.Text.Equals(string.Empty)
                || firstTextBox.Text.Equals(string.Empty)
                || lastTextBox.Text.Equals(string.Empty))
            {
                MessageBox.Show("Please fill in Username, First and Last names.");
                return; 
            }
            else if (!passwordTextBox.Text.Equals(confirmPasswordTextBox.Text))
            {
                MessageBox.Show("Passwords do not match.");
                return; 
            }
            else
            {
                username = labMemberTextBox.Text;
                name[0] = firstTextBox.Text;
                name[1] = lastTextBox.Text;
                password = passwordTextBox.Text;
                cellNum = (cellNumberTextBox.Text.Equals(string.Empty)) ? string.Empty : cellNumberTextBox.Text;
            }

            // get all labmember data from db
            SQLiteConnection m_dbConnection = new SQLiteConnection("DataSource=PCRL_phizer_study.db;Version=3");
            m_dbConnection.Open();
            string sql = "SELECT * FROM labmember";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
                labmembers[reader["lab_member"].ToString()] = new string[3] { reader["password"].ToString(), reader["first"].ToString(), reader["last"].ToString() };
            

            // match labmember to username to look in username already in use
            var user =
                from labmember in labmembers 
                where labmember.Key.Equals(username)
                select labmember;

            // if match 
            if (user.Any())
            {
                MessageBox.Show("This username is already used. Please choose another.");
                return;
            }
            else { 
                //save to dataset
                sql = "INSERT INTO labmember (lab_member, first, last, password, cell_num) VALUES ('" +
                    username + "','" + name[0] + "','" + name[1] + "','" + password + "','" + cellNum + "')";
                command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery(); 
                m_dbConnection.Close();

                MessageBox.Show("Update successful");

                //back to login 
                Login.Show();
                Close(); 
            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            Login.Show();
            Close(); 
        }
    }
}
