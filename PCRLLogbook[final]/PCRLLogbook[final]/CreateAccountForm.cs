using System;
using System.IO; 
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using Newtonsoft.Json;
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
            //if textboxes not filled properly
            if (labMemberTextBox.Text.Equals(string.Empty)
                || firstTextBox.Text.Equals(string.Empty)
                || lastTextBox.Text.Equals(string.Empty))
            {
                MessageBox.Show("Please fill in user, first and last name fields.");
                return; 
            }
            else if (!passwordTextBox.Text.Equals(confirmPasswordTextBox.Text))
            {
                MessageBox.Show("Passwords do not match.");
                return; 
            }
            else
            {   // otherwise init fields
                username = labMemberTextBox.Text;
                name[0] = firstTextBox.Text;
                name[1] = lastTextBox.Text;
                password = passwordTextBox.Text;
                cellNum = (cellNumberTextBox.Text.Equals(string.Empty)) ? 
                            string.Empty : cellNumberTextBox.Text;
            }

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
                // add labmember to config data 
                Login.config.labmember_data[username] = new Dictionary<string, string>(){ 
                    {"first", name[0]}, 
                    {"last", name[1]}, 
                    {"password", password}, 
                    {"cellnum", cellNum}
                }; 

                // write config to file 
                string json = JsonConvert.SerializeObject(Login.config, Formatting.Indented);
                using (StreamWriter outputFile = new StreamWriter(Login.config_path))
                {
                    outputFile.WriteLine(json);
                }

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
