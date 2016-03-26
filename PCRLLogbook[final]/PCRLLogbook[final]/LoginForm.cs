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
    public partial class LoginForm : Form
    {
        string password;
        public Config config;
        public string db_dir;
        public string base_dir;  
        public string username;
        public string config_path; 
        public string[] name = new string[2];
        Dictionary<string, string[]> username_pwd = new Dictionary<string, string[]>();
        

        public LoginForm()
        {
            InitializeComponent();

            // init directory paths 
            base_dir = ConfigurationSettings.AppSettings["BaseDirectory"];
            //db_dir = base_dir + "PCRL_phizer_study.db";
            config_path = base_dir + "config.json";
			
			// load config on opening
            string json = "";
            try {
                using (StreamReader sr = new StreamReader(config_path)) {
                    json = sr.ReadToEnd();
                }
            } catch (Exception exp) {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(exp.Message);
            }
            config = JsonConvert.DeserializeObject<Config>(json); 
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            // reload config upon login, so to update any changes
            string json = "";
            try {
                using (StreamReader sr = new StreamReader(config_path)) {
                    json = sr.ReadToEnd();
                }
            } catch (Exception exp) {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(exp.Message);
            }
            config = JsonConvert.DeserializeObject<Config>(json); 

            //check login
            if (usernameTextbox.Text.Equals(string.Empty)) { 
                MessageBox.Show("Please fill in username and password fields."); 
                return; 
            } 
            
			//set db directory 
			db_dir = config.db_file; 
			
            username = usernameTextbox.Text;
            password = passwordTextbox.Text;

            Dictionary<string, Dictionary<string, string>> labmembers = config.labmember_data; 

            var user =
                from labmember in labmembers
                where labmember.Key.Equals(username) && labmember.Value["password"].Equals(password)
                select labmember;

            if (!user.Any())    
                MessageBox.Show("Invalid username or password.\nPlease proceed to create an account.");
            else
            {   
                name[0] = user.First().Value["first"]; 
                name[1] = user.First().Value["last"];  
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

        private void generateReports_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "C:\\Python27\\python.exe";
            startInfo.Arguments = base_dir + "GenerateDailyReports.py";
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            process.StartInfo = startInfo;
            process.Start();
        }
    }
}
