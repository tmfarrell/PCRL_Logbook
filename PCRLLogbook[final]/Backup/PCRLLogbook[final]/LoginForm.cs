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
    public partial class LoginForm : Form
    {
        public string user;
        public string[] name = new string[2];
        string password; 

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
            
            user = usernameTextbox.Text;
            password = passwordTextbox.Text; 

            PCRLLogbookDBDataSet1.LabMemberDataTable labmembers = this.labMemberTableAdapter.GetData(); 

            var users =
                from labmember in labmembers
                where labmember.LabMember.Equals(user)
                select labmember.LabMember;

            if (!users.Any())
                MessageBox.Show("Invalid username.\nPlease proceed to create an account."); 

            var passwords = 
                from labmember in labmembers
                where labmember.LabMember.Equals(user)
                select labmember.Password;

            foreach (string pw in passwords)
            {
                if (pw.Equals(password))
                {
                    var first =
                        from labmember in labmembers
                        where labmember.LabMember.Equals(user)
                        select labmember.First;
                    var last =
                        from labmember in labmembers
                        where labmember.LabMember.Equals(user)
                        select labmember.Last;

                    foreach (string f in first)
                    {
                        foreach (string l in last)
                        {
                            name[0] = f;
                            name[1] = l;
                        }
                    }

                    LabroomForm labroomForm = new LabroomForm(this);
                    labroomForm.Show();
                    Hide(); 
                }
                else
                {
                    MessageBox.Show("\tInvalid password.");
                }
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
