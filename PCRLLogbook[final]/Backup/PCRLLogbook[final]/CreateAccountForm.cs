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
    public partial class CreateAccountForm : Form
    {
        public string user;
        public string[] name = new string[2];
        public string cellNum;
        public LoginForm Login; 
        string password; 

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
                user = labMemberTextBox.Text;
                name[0] = firstTextBox.Text;
                name[1] = lastTextBox.Text;
                password = passwordTextBox.Text;
                cellNum = (cellNumberTextBox.Text.Equals(string.Empty)) ? string.Empty : cellNumberTextBox.Text;
            }

            //check whether username is already taken
            PCRLLogbookDBDataSet1.LabMemberDataTable labmembers = this.labMemberTableAdapter.GetData();

            var usernames =
                from labmember in labmembers
                select labmember.LabMember.ToString();

            foreach (string u in usernames)
            {
                if (u.Equals(user))
                {
                    MessageBox.Show("This username is already used. Please choose another.");
                    return;
                }
            }

            labMemberTableAdapter.Insert(user, name[0], name[1], password, cellNum);
            
            //save to dataset
            try
            {
                this.Validate();
                labMemberTableAdapter.Update(pcrlLogbookDBDataSet.LabMember);
                MessageBox.Show("Update successful");
            }
            catch 
            {
                MessageBox.Show("Update failed");
            } 

            //back to login 
            Login.Show();
            Close(); 
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            Login.Show();
            Close(); 
        }
    }
}
